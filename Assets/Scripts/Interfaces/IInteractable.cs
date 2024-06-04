using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public abstract void Interaction();

    public abstract void InRange(bool inRange);
    
}
