using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectFunction : MonoBehaviour
{
    // Start is called before the first frame update
    [ColorUsage(false,true)]
    [SerializeField]private Color32[] _slashColor = new Color32[3];
    [SerializeField]private Transform[] _vfxPositions = new Transform[1];
    // void Start()
    // {
        
    // }

    public void SummonSingleSlash(int index)
    {
        VisualFXManager.instance.StartStaticParticle(VisualFXManager.VFXIndex.Slash,_vfxPositions[index],1f);
    }

    public void SummonSpinSlash(int index)
    {
        VisualFXManager.instance.StartStaticParticle(VisualFXManager.VFXIndex.SpinSlash,_vfxPositions[index],3f);
    }

    public void SummonClawSlash(int transformPos)
    {
        VisualFXManager.instance.StartStaticParticle(VisualFXManager.VFXIndex.ClawSlash,_vfxPositions[transformPos],1f);
    }

    public void SummonLungeLines(int index)
    {
        VisualFXManager.instance.StartStaticParticle(VisualFXManager.VFXIndex.LungeWaves,_vfxPositions[index],1f);
    }

    public void SummonXSlash(int index)
    {
        VisualFXManager.instance.StartStaticParticle(VisualFXManager.VFXIndex.XSlash,_vfxPositions[index],1f);
    }

    // public void StartTrackingTrail(int transformPos)
    // {

    // }

    
}
