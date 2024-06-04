using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GearCard : MonoBehaviour
{
    // Start is called before the first frame update
    //public enum imagetype{background,upgradeBackDrop,raritybar,icon,bottomdivider};
    //public enum textfields{upgradeLevel,title,piece,mainstatName,mainStatVal};
    [SerializeField]Image background, upgradeBackDrop, rarityBar, icon, topdivider,bottomdivider;
    [SerializeField]TMP_Text upgradeLevel, title, piece,mainstatName, mainStatVal;
    //Image[] gearImages = new Image[8];
    //TMP_Text[] textFields = new TMP_Text[]
    //GameObject[] substats = new GameObject[4];
    [SerializeField]GameObject substat;
    [SerializeField]GameObject seteffect;
    //[SerializeField]Color[] es.rarityColors = new Color[6];


    public void WriteSetEffect(int set)
    {
        SetBonus sb = EquipmentSprites.instance.bonusDesc[set];
        TMP_Text tmp = seteffect.GetComponent<TMP_Text>();

        tmp.text = "<b>2-Piece Set</b><br>"+sb.twoPiece+"<br><br>";
        tmp.text += "<b>4-Piece Set</b><br>"+sb.fourPiece;
    }

    public void UpdateCard(Item invenItem)
    {
        // Set the rarity color of the item;
        Color32 rareColor = EquipmentSprites.instance.rarityColors[(int)invenItem.grade];
        background.color = rareColor;
        rarityBar.color = rareColor;
        title.color = rareColor;
        topdivider.color = rareColor;
        bottomdivider.color = rareColor;
        title.text = invenItem.itemName;
        icon.sprite = invenItem.icon;
        // If the item is armor update the card based on the armor protocol
        Debug.Log("Adding in "+invenItem.itemName+" and is a weapon "+(invenItem is Weapon));
        if (invenItem is Armor)
        {
            UpdateCard((Armor)invenItem);
            return;
        }
        else if (invenItem is Weapon)
        {
            UpdateCard((Weapon)invenItem);
            return;
        }

        GameObject g;
        title.text = invenItem.itemName;
        mainstatName.enabled = false;
        mainStatVal.enabled = false;
        

        icon.sprite = invenItem.icon;
        upgradeBackDrop.gameObject.SetActive(false);
        
        for(int i = 0; i < 5; i++)
        {
            g = substat.transform.GetChild(i).gameObject;
            g.SetActive(false);
        }

        piece.text = invenItem.grade.ToString();

        if (invenItem is RPGMaterial)
        {
            RPGMaterial rp = (RPGMaterial)invenItem;
            piece.text += " | "+rp.category.ToString();
        }

        TMP_Text tmp = seteffect.GetComponent<TMP_Text>();
        tmp.text = invenItem.description;
        topdivider.gameObject.SetActive(false);
        bottomdivider.gameObject.SetActive(true);

    }

    void UpdateCard(Weapon w)
    {
        Debug.Log("Updating to weapon");
        topdivider.gameObject.SetActive(true);
        mainstatName.enabled = true;
        mainStatVal.enabled = true;
        if (w.enhancelevel == 0)
            upgradeBackDrop.gameObject.SetActive(false);
        else
        {
            upgradeBackDrop.gameObject.SetActive(true);
            upgradeLevel.SetText("+"+w.enhancelevel.ToString());
        }
        piece.text = w.grade.ToString()+" | "+w.wpType.ToString();
        mainstatName.SetText("Attack");
        mainStatVal.SetText(w.baseATK.ToString());

        int startSubSlot = w.rarity < 3 ? 1 : 2;

        GameObject g;
        if (startSubSlot == 2)
        {
            TMP_Text subName, subValue, subRare;
            g = substat.transform.GetChild(1).gameObject;
            g.SetActive(true);
            subName = g.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
            subValue = g.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
            subRare = g.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>();

            subName.text = ArmorAttributes.StatName(w.subStat.statType);
            subValue.text = (w.subStat.value * 100f).ToString("#.#")+"%";
            subRare.text = w.rarity.ToString();
        }

        for(int i = startSubSlot; i < 5; i++)
        {
            g = substat.transform.GetChild(i).gameObject;
            g.SetActive(false);

        }
        bottomdivider.gameObject.SetActive(false);
        TMP_Text tmp = seteffect.GetComponent<TMP_Text>();
        tmp.SetText(w.description);

    }

    void UpdateCard(Armor a)
    {
        topdivider.gameObject.SetActive(true);
        mainstatName.enabled = true;
        mainStatVal.enabled = true;
        if (a.enhancelevel == 0)
            upgradeBackDrop.gameObject.SetActive(false);
        else
        {
            upgradeBackDrop.gameObject.SetActive(true);
            upgradeLevel.SetText("+"+a.enhancelevel.ToString());
        }
        piece.text = a.grade.ToString()+" | "+a.slot.ToString();
        mainstatName.text = ArmorAttributes.StatName(a.subStats[0].statType);

        if (a.subStats[0].value >= 1f)
            mainStatVal.text = Mathf.Round(a.subStats[0].value).ToString();
        else
            mainStatVal.text = (a.subStats[0].value * 100f).ToString("#.#")+"%";
        
        int difference = 5 - a.subStats.Count;
        GameObject g;
        TMP_Text subName, subValue, subRare;
        for(int i = 1; i < 5; i++)
        {
            g = substat.transform.GetChild(i).gameObject;
            if (i >= a.subStats.Count)
            {
                g.SetActive(false);
            }
            else
            {
                g.SetActive(true);
                subName = g.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
                subValue = g.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
                subRare = g.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>();

                subName.SetText(ArmorAttributes.StatName(a.subStats[i].statType));
                subValue.SetText((a.totalStatVal(i) * 100f).ToString("#.#")+"%");
                subRare.SetText(a.subStats[i].grade.ToString());
                //sub
            }
        }
        bottomdivider.gameObject.SetActive(true);
        if (difference == 4)
            topdivider.gameObject.SetActive(false);
        WriteSetEffect((int)a.set);

    }
}
