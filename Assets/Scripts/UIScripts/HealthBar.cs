using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]protected Image _healthBar, _healthDrain;
    [SerializeField]protected TMP_Text _healthValue;
    protected Transform _statusBar;

    [SerializeField]protected float _drainRate = 1f;

    protected Coroutine _healthDrainTimer;

    protected virtual void Start()
    {
        _healthDrain = transform.GetChild(0).GetChild(1).GetComponent<Image>();
        _healthBar = transform.GetChild(0).GetChild(2).GetComponent<Image>();
        _healthValue = transform.GetChild(0).GetChild(3).GetComponent<TMP_Text>();
    }
    protected IEnumerator HealthDrain()
    {
        Debug.Log("Starting drain");
        yield return new WaitForSeconds(1f);

        while (_healthDrain.fillAmount > _healthBar.fillAmount)
        {
            _healthDrain.fillAmount -= Time.deltaTime * _drainRate;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        _healthDrainTimer = null;
    }

    public virtual void UpdateHealth(float curHealth, float maxHealth)
    {
        float healthRatio = curHealth/maxHealth;

        if (_healthDrainTimer != null)
        {
            StopCoroutine(_healthDrainTimer);
        }

        if (healthRatio < _healthBar.fillAmount)
        {   
            _healthDrainTimer = StartCoroutine(HealthDrain());
        }
        else
        {
            _healthDrain.fillAmount = healthRatio;
        }

        _healthBar.fillAmount = healthRatio;

        _healthValue.SetText(Mathf.RoundToInt(curHealth)+"/"+maxHealth);
    }
    

}
