using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ParticleDuration : MonoBehaviour
{
    // Start is called before the first frame update
    VisualEffect _vfx;
    public EnumLib.HitImpacts impactType;
    private float offSet = 0f;
    public void Initialize(EnumLib.HitImpacts impactType)
    {
        _vfx = transform.GetChild(0).GetComponent<VisualEffect>();
        this.impactType = impactType;
    }

    public void StartCountdown(float duration)
    {
        offSet = duration;
    }

    void Update()
    {
        if (_vfx.aliveParticleCount < 1 && offSet <= 0f)
        {
            Debug.Log("Out of particles");
            offSet = 0f;
            (HitImpactPool.returnToPool)?.Invoke(GetComponent<ParticleDuration>());
        }
        else if (offSet > 0f)
            offSet -= Time.deltaTime;
        // Debug.Log("VFX particles remaining"+_vfx.aliveParticleCount);
    }
}
