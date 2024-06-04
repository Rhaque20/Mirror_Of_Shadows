using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class PlayerHealthBar : HealthBar
{
    private Image _manaBar;
    private TMP_Text _manaValue;

    public static Action<float,float> updatePlayerHealth,updatePlayerMana;
    protected override void Start()
    {
        base.Start();
        _manaBar = transform.GetChild(1).GetChild(2).GetComponent<Image>();
        _manaValue = transform.GetChild(1).GetChild(3).GetComponent<TMP_Text>();

        updatePlayerHealth = UpdateHealth;
        updatePlayerMana = UpdateManaBar;
    }

    public void UpdateManaBar(float curMana, float maxMana)
    {
        float manaRatio = curMana/maxMana;

        _manaBar.fillAmount = manaRatio;
        _manaValue.SetText(curMana+"/"+maxMana);
    }
}