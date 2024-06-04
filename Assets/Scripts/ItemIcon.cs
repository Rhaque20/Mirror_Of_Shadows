using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemIcon : MonoBehaviour
{
    private Image backDrop, icon;
    private TMP_Text itemCount;

    public Item item;
    public int index;
    public InventoryDisplay inventoryDisplay;
    // Start is called before the first frame update
    void Start()
    {
        backDrop = transform.GetChild(0).GetComponent<Image>();
        icon = transform.GetChild(1).GetComponent<Image>();
        itemCount = transform.GetChild(2).GetComponent<TMP_Text>();
    }

    public void ChangeIconDisplay(Item mat, int i)
    {
        if (backDrop == null)
            Start();

        index = i;

        item = mat;

        if (mat == null)
        {
            //backDrop.sprite = es.rarityBar[0];
            Debug.Log("Item is null");
            backDrop.color = Color.white;
            icon.gameObject.SetActive(false);
            itemCount.gameObject.SetActive(false);

            return;

        }

        backDrop.sprite = EquipmentSprites.instance.rarityFrame;
        backDrop.color = EquipmentSprites.instance.rarityColors[(int)item.grade];
        icon.gameObject.SetActive(true);
        itemCount.gameObject.SetActive(true);
        icon.sprite = item.icon;
        itemCount.gameObject.SetActive(false);
        //transform.GetChild(1).gameObject.SetActive(false);
        if (item is Armor)
        {
            
        }
        else if (item is RPGMaterial)
        {
            
            RPGMaterial rp = (RPGMaterial)item;
            itemCount.gameObject.SetActive(true);
            if (rp.stackCount > 1)
                itemCount.SetText(inventoryDisplay.ReturnStackCount(rp).ToString());
        }
    }

    public void CallDisplay()
    {
        Debug.Log("Called Display");
        inventoryDisplay.DisplayItem(item);
    }
}
