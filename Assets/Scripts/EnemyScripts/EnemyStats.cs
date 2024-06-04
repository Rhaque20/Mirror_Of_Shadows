using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    [SerializeField]private EnemyHealthMini _miniDisplay;
    public override float GetFinalStat(int i)
    {
        float baseStat = -1;
        switch(i)
        {
            case HP:
                baseStat = _baseHP;
            break;
            case ATK:
                baseStat = _baseATK;
            break;
            case DEF:
                baseStat = _baseDEF;
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
        }

        return baseStat;
    }

    protected override void OnDeath()
    {
        onDeath?.Invoke();
        Delegate[] onDeathList = onDeath.GetInvocationList();
        foreach(Delegate d in onDeathList)
        {
            onDeath -= (d as Action);
        }
        gameObject.SetActive(false);
    }

    public override float DamageCalculation(Stats attacker, float skillMod)
    {
        int landedCrit = CanCrit(attacker.TotalCritRate);
        float damage = attacker.TotalATK/(TotalDEF/300+1);
        
        if (landedCrit == 1)
        {
            Debug.Log("CRIT!");
            damage *= (1f + attacker.TotalCritDMG);
        }
        damage *= skillMod;
        _curHP -= damage;
        if (_curHP <= 0f)
        {
            _curHP = 0f;
            OnDeath();
            return 0f;
        }
        Debug.Log("Display damage!");
        DisplayDamage(Mathf.Round(damage), landedCrit);
        if (_miniDisplay != null)
        {
            _miniDisplay.UpdateHealth(_curHP,_baseHP);
        }
        return 0f;
    }

}
