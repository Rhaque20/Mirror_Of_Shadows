using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumLib
{
    public enum AimType{rigid,freeAim,tracker,lastTracked};
    public enum Target{normal,self,party};
    public enum Elevation{underground,grounded,aerial,both};
    public enum Element{Physical, Fire, Wind, Earth, Water, Light, Dark};
    public enum HitWeight{None,Weak,Normal,Heavy,SuperHeavy};
    public enum ForceType{Knockback,Launch,Knockdown,HeavyKnockBack};
    public enum markerType{linearRect,centerRect,centerCircle};
    public enum Role{Rush,Defend,Heal,Buffer,Debuffer};
    public enum SkillType{DMGDeal,NonDMGDeal,GiveBuff,GiveDebuff,Heal};
    public enum RarityGroup{Beginner,Intermediate,Advanced};
    public enum HitImpacts{Regular};
    public enum Stats{Health,Attack,Defense,Potency,Resistance,CriticalRate,CriticalDamage,SPGain,SP};
    public enum Nation{Lucadia,Ryalos};

    public static float HitStunDuration(HitWeight weight)
    {
        float hitWeight = 0f;
        switch (weight)
        {
            case HitWeight.Normal:
                hitWeight = 0.05f;
                break;
            case HitWeight.Heavy:
                hitWeight = 0.1f;
                break;
            case HitWeight.SuperHeavy:
                hitWeight = 0.5f;
                break;

        }
        return hitWeight;
    }

    public static float KnockbackPower(int power)
    {
        switch(power)
        {
            case 1:
                return 100f;
            case 2:
                return 250f;
            case 3:
                return 500f;
        }

        return 1000f;
    }

    public static Vector2 MixedPower(ForceType typing, int power)
    {
        float strength = KnockbackPower(power);
        switch(typing)
        {
            case ForceType.Knockback:
                return new Vector2(strength,0f);
            case ForceType.Launch:
                return new Vector2(0f,strength);
            case ForceType.Knockdown:
                return new Vector2(0f,-strength);
                
        }

        return new Vector2(strength,strength);
    }
}