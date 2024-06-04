using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageNumber : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    TMP_Text numberDisplay;
    Camera cam;
    public void Initialize()
    {
        anim = GetComponent<Animator>();
        numberDisplay = transform.GetChild(0).GetComponent<TMP_Text>();
        cam = Camera.main;
    }

    public void SetString(float value, int type)
    {
        this.gameObject.SetActive(true);
        numberDisplay.SetText(value.ToString());
        switch(type)
        {
            case 0:
                anim.Play("Normal");
                break;
            case 1:
                anim.Play("Critical");
                break;
        }
    }

    public void HideNumber()
    {
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        transform.forward = cam.transform.forward;
    }

}
