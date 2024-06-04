using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    public string skillName;
    public EnumLib.HitWeight hitWeight = EnumLib.HitWeight.Normal;
    public float power,poiseDamage = 50f, motionArmor = 0.5f;
    public EnumLib.Element affinity = EnumLib.Element.Physical;
    public List<AnimationClip> anim = new List<AnimationClip>();
    public Vector2 knockbackForce;//X = knockback Y = Launch

}
