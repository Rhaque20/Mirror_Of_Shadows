using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParty : MonoBehaviour,ISaveLoadOut
{
    [SerializeField]List<GameObject> partyMembers = new List<GameObject>();
    public int activeMember {get ;private set;}

    public PlayerStats activeStat {get;private set;}
    public CharacterEquips activeEquip{get; private set;}
    public static Action<bool> onJump;
    public static Action onLand;
    // Start is called before the first frame update
    void Awake()
    {
        int n = Mathf.Min(transform.childCount,3);
        activeMember = 0;
        for (int i = 0; i < n; i++)
        {
            if (transform.GetChild(i).gameObject.CompareTag("Player"))
            {
                partyMembers.Add(transform.GetChild(i).gameObject);
            }
        }
        activeStat = partyMembers[activeMember].GetComponent<PlayerStats>();
        activeEquip = partyMembers[activeMember].GetComponent<CharacterEquips>();
        //PlayerMovement.setMove -= partyMembers[activeMember].GetComponent<PlayerMovement>().SetMovement;
    }

    public void LoadData(PlayerPartyLoadOut data)
    {
        if (data.equips.Count == 0)
            return;
        
        if (partyMembers == null)
            Awake();

        float totalHP;
        PlayerStats temp;
        
        for (int i = 0; i < partyMembers.Count; i++)
        {
            partyMembers[i].GetComponent<CharacterEquips>().FillLoadOut(data.equips[i]);
            temp = partyMembers[i].GetComponent<PlayerStats>();
            temp.SetHP(temp.totalHP);
        }
        
        temp = partyMembers[activeMember].GetComponent<PlayerStats>();
        totalHP = temp.totalHP;
        (PlayerHealthBar.updatePlayerHealth)?.Invoke(totalHP,totalHP);

        
    }
    public void SaveData(ref PlayerPartyLoadOut data)
    {
        for (int i = 0; i < partyMembers.Count; i++)
        {
            data.AddLoadOut(partyMembers[i].GetComponent<CharacterEquips>(),i);
        }
    }

    
}
