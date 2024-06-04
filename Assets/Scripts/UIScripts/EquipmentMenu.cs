using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class EquipmentMenu : MonoBehaviour
{
    //PlayerHUD phud;
    //PlayerParty pp;
    const int STAT = 0,SINGLE = 1,MULTI = 2;
    public enum EquipState{Stats,SingleEquip,MultiEquip};
    EquipState currentState = EquipState.Stats;
    GearCard gc;
    public static Action returntoMain;
    //DisplayGear dg;
    //MiniGearList mgl;
    PlayerStats playerStat;
    CharacterEquips activeEquip;
    [SerializeField]private GameObject mainStatBlock, equipmentBlock;
    [SerializeField]private Sprite[] defaultIcons = new Sprite[6];
    GameObject[] equipMenus = new GameObject[3];
    int selectedSlot = 0;


    // Start is called before the first frame update
    public void Initialize()
    {
        Debug.Log("Starting Equipment Menu");
        for (int i = 0; i < transform.childCount;i++)
        {
            equipMenus[i] = transform.GetChild(i).gameObject;
            if (i != 0)
                equipMenus[i].SetActive(false);
        }
        mainStatBlock = equipMenus[0].transform.GetChild(0).gameObject;
        //equipmentBlock = transform.Find("EquipmentBlock").gameObject;
        equipmentBlock = equipMenus[0].transform.GetChild(1).gameObject;
        gc = equipMenus[0].transform.GetChild(2).GetComponent<GearCard>();
        ToggleCard(false);

        returntoMain = ReturntoMain;
        // dg = equipmentBlock.GetComponent<DisplayGear>();
        //gc.gameObject.SetActive(false);
        // mgl = transform.Find("SingleEquipBlock").GetComponent<MiniGearList>();
    }

    public void ReturntoMain()
    {
        int i = (int)currentState;
        equipMenus[i].SetActive(false);
        equipMenus[STAT].SetActive(true);
        currentState = EquipState.Stats;
        UpdateStats();
        UpdateGearSlots();
        ToggleCard(false);

    }

    public void SingleEquip(Equipment.Piece slot)
    {
        currentState = EquipState.SingleEquip;
        equipMenus[STAT].SetActive(false);
        equipMenus[SINGLE].SetActive(true);
        equipMenus[SINGLE].GetComponent<SingleEquipMenu>().InitializeMenu(slot);
    }

    public void SetPlayerStat(PlayerStats playerStat, CharacterEquips activeEquip)
    {
        this.playerStat = playerStat;
        this.activeEquip = activeEquip;
        UpdateStats();
        UpdateGearSlots();
    }

    string StatValue(int i)
    {
        
        switch(i)
        {
            case 0:
                return (Mathf.Floor(playerStat.totalHP)).ToString();
            case 1:
                return (Mathf.Floor(playerStat.TotalATK)).ToString();
            case 2:
                return (Mathf.Floor(playerStat.TotalDEF)).ToString();
            case 3:
                return (playerStat.TotalPotency * 100f).ToString("F1")+"%";
            case 4:
                return (playerStat.TotalResistance * 100f).ToString("F1")+"%";
            case 5:
                return (playerStat.TotalCritRate * 100f).ToString("F1")+"%";
            case 6:
                return (playerStat.TotalCritDMG * 100f).ToString("F1")+"%";
            case 7:
                return (playerStat.TotalMPGain*100f).ToString("F1")+"%";
        }

        return "Banana";
    }

    public void OpenSingleEquip()
    {
        SingleEquip((Equipment.Piece)selectedSlot);
    }

    public void ActivateGearCard(int i)
    {
        if (i == 6)
        {
            Weapon w = activeEquip.a_Weapon;
        }
        else
        {
            Armor a = activeEquip.EquipPiece(i);// Get Armor from the current equip
            selectedSlot = i;// Cache the slot index

            if (a != null)// If there is an armor piece in the slot
            {
                ToggleCard(true);// Active and update the card
                gc.UpdateCard((Item)a);
                // Set current selected slot in single equip to the cached index
                // This is to allow for the implemented unequip button to work outside of the
                // single equip menu
                equipMenus[SINGLE].GetComponent<SingleEquipMenu>().SetCurrentListing(selectedSlot);
                
            }
            else
            {
                ToggleCard(false);
                OpenSingleEquip();
            }
        }
        
    }

    public void ToggleCard(bool toggle)
    {
        gc.gameObject.SetActive(toggle);
    }

    // Create delegate to update stat when equipment changes
    public void UpdateStats()
    {
        GameObject stat;
        for (int i = 0; i < mainStatBlock.transform.childCount; i++)
        {
            stat = mainStatBlock.transform.GetChild(i).GetChild(2).gameObject;
            stat.GetComponent<TMP_Text>().SetText(StatValue(i));
        }

        playerStat.ReshuffleHP();

        (PlayerHealthBar.updatePlayerHealth)?.Invoke(playerStat.curHP,playerStat.totalHP);
    }

    public void UpdateGearSlots()
    {
        Transform itemSlot;
        Armor armor;
        Image itemSprite;
        for (int i = 0; i < 6; i++)
        {
            itemSlot = equipmentBlock.transform.GetChild(i);
            itemSprite = itemSlot.GetChild(1).GetComponent<Image>();
            armor = activeEquip.EquipPiece(i);

            if (armor == null)
            {
                itemSprite.sprite = defaultIcons[i];
                itemSlot.GetChild(0).GetComponent<Image>().color = Color.white;
            }
            else
            {
                itemSlot.GetChild(0).GetComponent<Image>().color = EquipmentSprites.instance.rarityColors[armor.rarity];
                itemSprite.sprite =  armor.icon;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked out of Bounds");
                ToggleCard(false);
            }
        }
    }
}
