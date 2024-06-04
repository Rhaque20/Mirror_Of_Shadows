using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    /**
       0     1     2     3     4     5     6     7     8
    +-----+-----+-----+-----+-----+-----+-----+-----+-----+
    |  HP | ATK | DEF | POT | RES | CRI | CDMG|SPGAI| SP  |
    +-----+-----+-----+-----+-----+-----+-----+-----+-----+
    **/
    
    // These are the main stats that all entities will have
    protected const int HP = 0,ATK = 1,DEF = 2,POT = 3, RES = 4, CRI = 5, CDMG = 6;
    protected float _curHP = 0f, _baseHP = 0f;
    protected float _baseATK = 0f, _baseDEF = 0f;
    protected float _baseCritRate = 0.05f, _baseCritDMG = 0.5f;
    protected float _basePotency = 0f, _baseResistance = 0f;

    protected const int maxDamageCount = 20;
    protected float randomRange = 1f;

    // StatGrowth allows for applying scalable stats to character level
    [SerializeField]private StatGrowth statGrowth;
    [SerializeField]private GameObject damageNumber;

    //Provides a list of all the damagenumber prefab to be recycled in object pooling
    protected List<GameObject> damageNumbers = new List<GameObject>();

    public Action onDeath;

    // Will return the final Attack after factoring in equipment, buffs, and skill tree items 
    //(though the latter will be in playerStats)

    public bool isDead
    {
        get{return _curHP <= 0f;}
    }

    public void SetHP(float hp)
    {
        _curHP = hp;
    }
    public float totalHP
    {
        get{return GetFinalStat(HP);}
    }
    public float TotalATK
    {
        get{return GetFinalStat(ATK);}
    }

    public float TotalDEF
    {
        get{return GetFinalStat(DEF);}
    }

    public float TotalCritRate
    {
        get{
            return GetFinalStat(CRI);
        }
    }
    public float TotalCritDMG
    {
        get{
            return GetFinalStat(CDMG);
        }
    }

    public float TotalPotency
    {
        get{
            return GetFinalStat(POT);
        }
    }

    public float TotalResistance
    {
        get{
            return GetFinalStat(RES);
        }
    }

    public virtual float GetFinalStat(int i)
    {
        return _baseDEF;
    }

    public virtual void InitializeStats()
    {
        Debug.Log("Initialize stats for "+gameObject.name);
        _baseHP = statGrowth.baseHP;
        _curHP = _baseHP;
        _baseATK = statGrowth.baseATK;
        _baseDEF = statGrowth.baseDEF;
        _baseCritRate = statGrowth.baseCritRate;
        _baseCritDMG = statGrowth.baseCritDMG;
        _basePotency = statGrowth.basePotency;
        _baseResistance = statGrowth.baseResistance;

        GameObject temp;
        Transform damageParent = transform.GetChild(1);

        for (int i = 0; i < maxDamageCount; i++)
        {
            temp = Instantiate(damageNumber);
            temp.transform.SetParent(damageParent);
            damageNumbers.Add(damageParent.GetChild(i).gameObject);
            damageNumbers[i].SetActive(false);
            damageNumbers[i].GetComponent<DamageNumber>().Initialize();

        }
    }

    protected virtual void Start()
    {
        InitializeStats();
    }

    public virtual float TotalDMGMod(float skillMod)
    {
        return 0.0f;
    }

    public virtual int CanCrit(float critRate)
    {
        float randomVal = UnityEngine.Random.Range(0f,1f);
        Debug.Log("Random Val: "+randomVal+"against critRate of "+critRate);
        if (randomVal <= critRate)
            return 1;
        return 0;
    }

    public virtual void DisplayDamage(float damage, int crit)
    {
        Vector3 displacedPos;
        for (int i = 0; i < maxDamageCount; i++)
        {
            if (!damageNumbers[i].activeSelf)
            {
                damageNumbers[i].GetComponent<DamageNumber>().SetString(damage,crit);
                displacedPos = new Vector3(transform.position.x + UnityEngine.Random.Range(-randomRange,randomRange), transform.position.y + UnityEngine.Random.Range(-randomRange,randomRange), transform.position.z + UnityEngine.Random.Range(-randomRange,randomRange));
                damageNumbers[i].transform.position = displacedPos;
                break;
            }
        }
    }

    // crit and element needed
    public virtual float DamageCalculation(Stats attacker, float skillMod)
    {
        int landedCrit = CanCrit(attacker.TotalCritRate);
        float damage = attacker.TotalATK/(TotalDEF/300+1);
        
        if (landedCrit == 1)
        {
            Debug.Log("CRIT!");
            damage *= (1f + attacker.TotalCritDMG);
        }
        damage *= skillMod;
        Debug.Log("Display damage!");
        DisplayDamage(Mathf.Round(damage), landedCrit);
        return damage;
    }

    protected virtual void OnDeath()
    {
        
    }
}
