using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGearCard : MonoBehaviour
{
    // Start is called before the first frame update
    Image armorBackDrop, armorIcon, mainStatIcon;
    Image[] subStatIcons = new Image[4];

    TMP_Text mainStatName, mainStatVal;
    TMP_Text[] subStatVal = new TMP_Text[4];
    Armor armorRef = null;
    string armorID;

    public void GetPieces()
    {
        GameObject temp;
        armorBackDrop = transform.GetChild(1).GetComponent<Image>();
        armorIcon = armorBackDrop.transform.GetChild(0).GetComponent<Image>();
        temp = transform.GetChild(2).gameObject;
        mainStatIcon = temp.transform.GetChild(0).GetComponent<Image>();
        mainStatName = temp.transform.GetChild(1).GetComponent<TMP_Text>();
        mainStatVal = temp.transform.GetChild(2).GetComponent<TMP_Text>();

        temp = transform.GetChild(3).gameObject;

        for(int i = 0; i < temp.transform.childCount; i++)
        {
            subStatIcons[i] = temp.transform.GetChild(i).GetChild(0).GetComponent<Image>();
            subStatVal[i] = temp.transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>();
        }
    }

    public void Initialize(Armor a, string armorID)
    {
        
        this.armorID = armorID;
        armorRef = a;
        if (armorRef == null)
        {
            this.gameObject.SetActive(false);
            return;
        }
        else
        {
            this.gameObject.SetActive(true);
        }

        armorBackDrop.color = EquipmentSprites.instance.rarityColors[a.rarity];
        armorIcon.sprite = EquipmentSprites.instance.GetArmorIcon(a.set.ToString(),a.slot.ToString());

        mainStatIcon.sprite = EquipmentSprites.instance.statIcons[(int)a.subStats[0].statType];
        mainStatName.text = ArmorAttributes.StatName(a.subStats[0].statType);
        
        if (a.subStats[0].value < 1f && a.subStats[0].value > 0f)
            mainStatVal.text = (a.subStats[0].value * 100f).ToString("F1")+"%";
        else
            mainStatVal.text = Mathf.Floor(a.subStats[0].value).ToString();

        for (int i = 0 ; i < 4; i++)
        {
            if (i >= a.subStats.Count - 1)
            {
                subStatIcons[i].gameObject.SetActive(false);
                subStatVal[i].gameObject.SetActive(false);
            }
            else
            {
                subStatIcons[i].gameObject.SetActive(true);
                subStatVal[i].gameObject.SetActive(true);

                subStatIcons[i].sprite = EquipmentSprites.instance.statIcons[(int)a.subStats[i+1].statType];
                subStatVal[i].text = (a.subStats[i+1].value * 100f).ToString("F1")+"%";
            }
        }
    }

    public void SelectArmor()
    {
        (SingleEquipMenu.displayArmor)?.Invoke(armorRef,armorID);
    }

}
