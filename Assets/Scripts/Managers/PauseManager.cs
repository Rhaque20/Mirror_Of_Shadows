using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    enum MenuState{Main, Items,Equipment};
    private MenuState curState = MenuState.Main;
    [SerializeField]private GameObject[] menuItems;
    bool isPause = false, canPause = true;
    GameObject pauseElement;

    PlayerParty playerParty;
    
    public static CharacterEquips activeEquip{get;private set;}
    public static PlayerStats activeStat{get;private set;}
    public static Inventory inventory{get;private set;}

    [SerializeField]private GameObject mainTab, buttons;
    private Image icon;
    private TMP_Text title;

    //Menu scripts
    [SerializeField]private EquipmentMenu equipMenu;

    const int offset = 1;// To offset from the first category being essentially in being active
    const int centralPauseIndex = 0;// Index of the child object that has the central pause menu
    // Start is called before the first frame update
    void Awake()
    {
        int n = transform.childCount;
        menuItems = new GameObject[n-offset];
        pauseElement = transform.GetChild(0).gameObject;
        pauseElement.SetActive(false);
        for (int i = offset; i < n; i++)
        {
            menuItems[i-offset] = transform.GetChild(i).gameObject;
            switch(i)
            {
                case 2:
                    inventory = menuItems[i-offset].GetComponent<Inventory>();
                break;
                case 3:
                    equipMenu = menuItems[i-offset].GetComponent<EquipmentMenu>();
                    equipMenu.Initialize();
                    menuItems[i-offset].SetActive(false);
                break;
            }
        }

        if (inventory == null)
            Debug.Log("Can't get inventory");
        else
            Debug.Log("Inventory obtained");

        title = mainTab.transform.GetChild(1).GetComponent<TMP_Text>();
        icon = mainTab.transform.GetChild(2).GetComponent<Image>();
        mainTab.SetActive(false);
        playerParty = GameObject.Find("Party").GetComponent<PlayerParty>();
    }

    public void ActivateTitle(bool activate)
    {
        title.text = curState.ToString();

        switch(curState)
        {
            case MenuState.Items:

            break;
            case MenuState.Equipment:
                activeStat = playerParty.activeStat;
                activeEquip = playerParty.activeEquip;
                equipMenu.SetPlayerStat(activeStat,activeEquip);
            break;
        }

        mainTab.SetActive(activate);
        buttons.SetActive(!activate);
    }

    public void ActivateMenu(int i)
    {
        int stateInt = (int)curState;

        if (i == centralPauseIndex)
        {
            menuItems[stateInt].SetActive(false);
            menuItems[centralPauseIndex].SetActive(true);
            curState = MenuState.Main;
            ActivateTitle(false);
        }
        else
        {
            menuItems[stateInt].SetActive(false);
            curState = (MenuState)i;
            menuItems[i].SetActive(true);
            ActivateTitle(true);
        }
    }

    public void SetMenu(bool isActive)
    {
        menuItems[(int)curState].SetActive(isPause);
        pauseElement.SetActive(isPause);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("escape"))
        {
            if (isPause)
            {
                isPause = false;
            }
            else
            {
                isPause = true;
            }
            (TimeManager.invokePause)?.Invoke(isPause);
            SetMenu(isPause);
        }
    }
}
