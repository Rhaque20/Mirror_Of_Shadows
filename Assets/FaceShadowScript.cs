using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class FaceShadowScript : MonoBehaviour
{
    public Material faceMaterial;
    private void SetHeadDirection()
    {
        if (faceMaterial != null)
        {
           faceMaterial.SetVector("_HeadForward",this.transform.forward);
           faceMaterial.SetVector("_HeadRight",this.transform.right);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetHeadDirection();
    }
}
