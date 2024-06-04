using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponSkill", menuName = "WeaponSkill")]
public class WeaponSkill : ScriptableObject
{
    public Weapon.WeaponType weaponType;
    public Weapon.Series weaponSeries;
    public string weaponName;
    [TextArea(5,10)]
    public string weaponSkillDesc;
    [TextArea(5,10)]
    public string weaponLore;
}
