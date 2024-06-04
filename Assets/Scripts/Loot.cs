using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using TMPro;
using UnityEngine.UI;

public class Loot : MonoBehaviour,IInteractable
{
    [SerializeField]private LootVFXParameter[] presets = new LootVFXParameter[5];
    [SerializeField]private VisualEffect lootVFX;
    [SerializeField]private Transform nameTag;
    [SerializeField]private TrailRenderer trailRender;
    [SerializeField]private GameObject armor,weapon;
    MeshFilter weaponMeshFilter;
    MeshRenderer weaponMeshRenderer;

    private Item loot;
    private Camera cam;
    private Image backDrop,selected;
    private TMP_Text itemName;
    private bool claimed = false, landed = false;
    private Rigidbody rigidBody;


    // Give each instance of the loot acess to the mian camera and set up the variables
    // It's set to a custom function to control function call order.
    public void Initialize(Camera cam)
    {
        selected = nameTag.GetChild(0).GetComponent<Image>();
        backDrop = nameTag.GetChild(1).GetComponent<Image>();
        itemName = nameTag.GetChild(2).GetComponent<TMP_Text>();
        rigidBody = GetComponent<Rigidbody>();
        this.cam = cam;
        weaponMeshFilter = weapon.GetComponent<MeshFilter>();
        weaponMeshRenderer = weapon.GetComponent<MeshRenderer>();
    }

    // Inherited from IInteractable, will be called when player interaction presses interact key.
    public void Interaction()
    {
        // Invoke a func to append the loot to inventory but check if inventory is full;
        claimed = (bool)(Inventory.appendToInventory)?.Invoke(loot);

        // If not full, return current object to pool
        if (claimed)
        {
            (LootManager.returnToPool)?.Invoke(this.gameObject);
        }
        else
            Debug.Log("Inventory full");
    }

    // Make nametag highlight to indicate in grabbing range
    public void InRange(bool inRange)
    {
        selected.gameObject.SetActive(inRange);
    }

    // Upon the loot hitting the ground, activate the name tag and loot VFX
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            landed = true;
            nameTag.gameObject.SetActive(true);
            lootVFX.gameObject.SetActive(true);
        }
    }

    // When player's interaction collider is within the object's interaction collider
    void OnTriggerStay(Collider entity)
    {
        if (entity.gameObject.CompareTag("Player"))
        {
            Debug.Log("In range of player!");
            InRange(true);
        }
    }

    // Remove highlight if player is no longer in range.
    void OnTriggerExit(Collider entity)
    {
        if (entity.gameObject.CompareTag("Player"))
        {
            Debug.Log("Out range of player!");
            InRange(false);
        }
    }

    // Set up the VFX parameters based on the fed in presets.
    void SetUpVFX(int rarity)
    {
        lootVFX.SetVector4("RarityColor",presets[rarity].rarityColor);
        lootVFX.SetInt("ParticleRate",presets[rarity].particleRate);
        lootVFX.SetInt("CircleMeshRate",presets[rarity].circleMeshRate);
        lootVFX.SetInt("FlareRate",presets[rarity].flareRate);
        lootVFX.SetFloat("CircleMeshSize",presets[rarity].circleMeshSize);
        lootVFX.SetFloat("CircleMeshUpSpeed",presets[rarity].circleMeshUpSpeed);
        lootVFX.SetFloat("GlowGroundSize",presets[rarity].glowGroundSize);
        lootVFX.SetVector2("FlareSize",presets[rarity].flareSize);
    }

    void SwapMesh(Item item)
    {
        bool isWeapon = item is Weapon;

        if (isWeapon)
        {
            armor.SetActive(false);
            weapon.SetActive(true);
            Debug.Log("Weapon: "+item.itemName);
            Weapon lootWeapon = (Weapon)item;
            if (lootWeapon.weaponMesh != null)
            {
                Debug.Log("Weapon mesh of loot is named "+lootWeapon.weaponMesh.name);
            }
            weaponMeshFilter.sharedMesh = lootWeapon.weaponMesh.GetComponent<MeshFilter>().sharedMesh;
            weaponMeshRenderer.sharedMaterial = lootWeapon.weaponMesh.GetComponent<MeshRenderer>().sharedMaterial;
        }
        else
        {
            armor.SetActive(true);
            weapon.SetActive(false);
        }


        
    }

    // Set up the loot drop before launching it in the air
    public void SetUpLoot(Item item)
    {
        claimed = false;
        loot = item;
        itemName.SetText(item.itemName);
        itemName.color = EquipmentSprites.instance.rarityColors[item.rarity];
        trailRender.startColor = EquipmentSprites.instance.rarityColors[item.rarity];
        SetUpVFX(item.rarity);
        SwapMesh(item);
        rigidBody.AddForce(new Vector3(-5,300,15));
        nameTag.gameObject.SetActive(false);
        lootVFX.gameObject.SetActive(false);
    }

    // Rotate the name tag to have it always face the camera.
    void Update()
    {
        nameTag.forward = cam.transform.forward;
    }
}
