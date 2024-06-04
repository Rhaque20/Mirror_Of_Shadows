using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField]private GameObject inventoryIcon, equipmentCard, gridParent;
    private Inventory inven;
    List<GameObject> invenSlots = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        LoadUp();
    }

    public int ReturnStackCount(RPGMaterial r)
    {
        return inven.ReturnStackCount(r);
    }

    public void LoadUp()
    {
        GameObject g = null;
        Item item = null;
        int i = 0;
        inven = GetComponent<Inventory>();

        for (i = 0; i < inven.maxSize; i++)
        {
            g = Instantiate(inventoryIcon);
            //g.SetActive(true);
            g.transform.SetParent(gridParent.transform,false);
            g.GetComponent<ItemIcon>().inventoryDisplay = GetComponent<InventoryDisplay>();
            g.GetComponent<ItemIcon>().ChangeIconDisplay(null,i);
            g.transform.localScale = new Vector2(1f,1f);
            invenSlots.Add(g);
        }

        i = 0;
        foreach(KeyValuePair<string, Item> kvp in inven.stock)
        {
            item = kvp.Value;
            invenSlots[i].GetComponent<ItemIcon>().ChangeIconDisplay(item, i);
            
            i++;
            
        }
        equipmentCard.SetActive(false);
    }

    public void Reload()
    {
        //GameObject g;
        Item item;
        ItemIcon itemIcon = null;
        int i = 0;
        //inventoryIcon.SetActive(false);
        foreach(KeyValuePair<string, Item> kvp in inven.stock)
        {
            itemIcon = invenSlots[i].GetComponent<ItemIcon>();
            itemIcon.ChangeIconDisplay(kvp.Value, i);
            i++;
            
        }
        if (i < inven.maxSize)
        {
            
            for (int j = i; j < inven.maxSize; j++)
            {
                itemIcon = invenSlots[j].GetComponent<ItemIcon>();
                itemIcon.ChangeIconDisplay(null, j);
            }
        }
    }
    public void DisplayItem(Item mat)
    {
        if(mat != null)
        {
            equipmentCard.SetActive(true);
            equipmentCard.GetComponent<GearCard>().UpdateCard(mat);
        }
        else
            equipmentCard.SetActive(false);
    }
}
