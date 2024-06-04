using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

// Intended to be accessed by any class to fill in data that is not serializable
public class EquipmentSprites : MonoBehaviour
{
    public Sprite rarityFrame;
    // Holds various icons of armor and weapons in a dictionary of dictionaries
    public SpriteLibraryAsset armorIcons, weaponIcons;
    public Sprite[] blankGear = new Sprite[7];
    //public List<SetBonus> bonusDesc = new List<SetBonus>();
    public SetBonus[] bonusDesc = new SetBonus[1];
    public Sprite[] statIcons = new Sprite[6];

    public Color32[] rarityColors = new Color32[6];

    [Header("Meshes")]
    public GameObject[] daggers = new GameObject[1];

    [Header("WeaponSkills")]
    public WeaponSkill[] daggerWeaponSkill = new WeaponSkill[1];

    public static EquipmentSprites instance;

    void Awake()
    {
        instance = this;
    }

    public Sprite GetArmorIcon(string set, string piece)
    {
        return armorIcons.GetSprite(set,piece);
    }

    public Sprite GetWeaponIcon(string category, string series)
    {
        return weaponIcons.GetSprite(category,series);
    }

    public GameObject GetWeaponMesh(Weapon.WeaponType category, Weapon.Series series)
    {
        GameObject mesh = null;
        switch (category)
        {
            case Weapon.WeaponType.Dagger:
                mesh = daggers[(int)series];
            break;
        }

        return mesh;
    }

    public WeaponSkill GetWeaponSkill(Weapon.WeaponType category, Weapon.Series series)
    {
        WeaponSkill wpSkill = null;
        switch (category)
        {
            case Weapon.WeaponType.Dagger:
                wpSkill = daggerWeaponSkill[(int)series];
            break;
        }

        return wpSkill;
    }
    
}