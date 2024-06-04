using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    private const int MPGAIN = 7;
    private float _baseMP = 300f, _curMP = 300f;
    private float _mpGainOnHit = 5f, _baseMPRegen = 1f;
    private float _baseMPGain = 1f;

    [SerializeField]Transform _focusTarget;

    [SerializeField]CharacterEquips currentEquip;

    public float curHP
    {
        get { return _curHP;}
    }

    public float TotalMPGain
    {
        get{return GetFinalStat(MPGAIN);}
    }

    // public override void InitializeStats()
    // {
    //     base.InitializeStats();

    // }
    public void ReshuffleHP()
    {
        if (_curHP > totalHP)
        {
            _curHP = totalHP;
        }
    }
    protected override void Start()
    {
        InitializeStats();
        currentEquip = GetComponent<CharacterEquips>();
    }

    public override void DisplayDamage(float damage, int crit)
    {
        Vector3 displacedPos;
        for (int i = 0; i < maxDamageCount; i++)
        {
            if (!damageNumbers[i].activeSelf)
            {
                damageNumbers[i].GetComponent<DamageNumber>().SetString(damage,crit);

                displacedPos = new Vector3(
                _focusTarget.position.x + Random.Range(-randomRange,randomRange), 
                _focusTarget.position.y + Random.Range(-randomRange,randomRange), 
                _focusTarget.position.z + Random.Range(-randomRange,randomRange)
                );

                damageNumbers[i].transform.position = displacedPos;
                break;
            }
        }
    }

    public override float DamageCalculation(Stats attacker, float skillMod)
    {
        float damage = base.DamageCalculation(attacker, skillMod);
        _curHP -= damage;
        (PlayerHealthBar.updatePlayerHealth)?.Invoke(_curHP,totalHP);

        return damage;
    }
    

    

    public override float GetFinalStat(int i)
    {
        float statVal = 0f;
        float baseStat = -1;
        switch(i)
        {
            case HP:
                baseStat = _baseHP;
                statVal = currentEquip.flatHP;
            break;
            case ATK:
                baseStat = _baseATK;
                statVal = currentEquip.flatATK;
            break;
            case DEF:
                baseStat = _baseDEF;
                statVal = currentEquip.flatDEF;
            break;
            case POT:
                baseStat = _basePotency;
            break;
            case RES:
                baseStat = _baseResistance;
            break;
            case CRI:
                baseStat = _baseCritRate;
            break;
            case CDMG:
                baseStat = _baseCritDMG;
            break;
            case MPGAIN:
                baseStat = _baseMPGain;
            break;
        }

        if (i < POT)
        {
            Debug.Log("access i for array size of "+currentEquip.totalStats.Length);
            statVal += baseStat + (baseStat * currentEquip.totalStats[i]);
            if (i == HP)
            {
                statVal = Mathf.RoundToInt(statVal);
            }
        }
        else
        {
            statVal = baseStat + currentEquip.totalStats[i];
        }

        return statVal;
    }
    
}
