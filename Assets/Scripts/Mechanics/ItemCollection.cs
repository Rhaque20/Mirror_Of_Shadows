using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Interactable") && Input.GetKeyUp("m"))
        {
            col.gameObject.GetComponent<IInteractable>().Interaction();
        }
    }
}
