using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour,IInteractable
{
    public enum ChestType{Common,Silver, Gold, Strongbox};
    [SerializeField]private ChestType chestGrade = ChestType.Common;
    [SerializeField]public SetBonus setDrop;
    [SerializeField]List<Item> additionalLoot = new List<Item>();
    [SerializeField]EnumLib.RarityGroup rg = EnumLib.RarityGroup.Beginner;
    [SerializeField]GameObject prompt;

    public int chestLevel = 1;
    
    public void Interaction()
    {
        OpenChest();
        gameObject.SetActive(false);
    }

    public void InRange(bool inRange)
    {
        prompt.SetActive(inRange);
    }

    void OnTriggerStay(Collider entity)
    {
        if (entity.gameObject.CompareTag("Player"))
        {
            Debug.Log("In range of player!");
            InRange(true);
        }
    }

    void OnTriggerExit(Collider entity)
    {
        if (entity.gameObject.CompareTag("Player"))
        {
            Debug.Log("Out range of player!");
            InRange(false);
        }
    }

    void CommonChestLoot()
    {
        int count = Random.Range(1,4);
        Armor a;

        for (int i = 0; i < count; i++)
        {
            a = ArmorAttributes.GenerateArmor(chestLevel,setDrop.setType);
            (LootManager.generateLoot)?.Invoke(transform.position,(Item)a);
        }

        // count = Random.Range(1,4);
        // for (int i = 0; i < count; i++)
        // {
        //     landingPos = new Vector2(Random.Range(x - bux.size.x,x + bux.size.x),Random.Range(y - bux.size.y/2f,y + bux.size.y/2f));
        //     lm.GenerateItem(new RPGMaterial(expOre),landingPos, transform.localPosition);
        // }

    }

    public void OpenChest()
    {
        switch(chestGrade)
        {
            case ChestType.Common:
                CommonChestLoot();
            break;
        }

        if (additionalLoot.Count > 0)
        {
            foreach(Item i in additionalLoot)
            {
                // landingPos = new Vector2(Random.Range(x - bux.size.x,x + bux.size.x),Random.Range(y - bux.size.y/2f,y + bux.size.y/2f));
                if (i is Weapon)
                {
                    Weapon w = (Weapon)i;
                    (LootManager.generateLoot)?.Invoke(transform.position,new Weapon(w));
                }
                    
            }
        }
    }

    void Update()
    {
        prompt.transform.forward = LootManager.cam.transform.forward;
    }
}
