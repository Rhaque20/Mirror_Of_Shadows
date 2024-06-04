using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VisualFXManager : MonoBehaviour
{
    public enum VFXIndex{Slash,SpinSlash,ClawSlash,LungeWaves,XSlash}
    [SerializeField]List<GameObject> _visualEffects = new List<GameObject>();
    [SerializeField]int _vfxCount = 20;
    Dictionary<VFXIndex,Queue<GameObject>> _vfxPool = new ();
    public static VisualFXManager instance;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        // slashEffects = new VisualEffect[slashObjects.Length];
        // for (int i = 0; i < slashEffects.Length; i++)
        // {
        //     slashEffects[i] = slashObjects[i].GetChild(i).GetComponent<VisualEffect>();
        //     //slashObjects[i].gameObject.SetActive(false);
        // }
        int i = 0;
        foreach(GameObject vfxPrefab in _visualEffects)
        {
            Queue<GameObject> vfxPool = new();
            
            for(int j = 0; j < _vfxCount; j++)
            {
                GameObject temp = Instantiate(vfxPrefab);
                temp.SetActive(false);
                temp.transform.SetParent(this.transform);
                vfxPool.Enqueue(temp);
            }
            _vfxPool.Add((VFXIndex)i,vfxPool);
            i++;
        }
    }

    private IEnumerator VFXTimer(float lifeTime, VFXIndex index, GameObject vfx)
    {
        yield return new WaitForSeconds(lifeTime);
        vfx.SetActive(false);
        _vfxPool[index].Enqueue(vfx);
    }

    private IEnumerator VFXFollowTimer(float lifeTime, VFXIndex index, GameObject vfx, Transform position)
    {
        while(lifeTime > 0f)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            lifeTime -= Time.deltaTime;
        }
    }

    // private IEnumerator TrailFollowTimer(bool startTracking, ParticleSystem trailParticle)
    // {
    //     if (startTracking)
    //     {

    //     }
    // }

    public void StartTrackingTrail(bool startTracking, Transform position)
    {
        if (startTracking)
        {

        }
        else
        {

        }
    }

    public void StartStaticParticle(VFXIndex index, Transform slashPos, float lifeTime)
    {
        GameObject temp = _vfxPool[index].Dequeue();
        temp.SetActive(true);
        temp.transform.position = slashPos.position;
        temp.transform.rotation = slashPos.rotation;
        temp.transform.localScale = slashPos.localScale;
        SetLifeTime(temp,lifeTime,index);
    }


    public void SetLifeTime(GameObject vfx, float time, VFXIndex vfxIndex)
    {
        VisualEffect visual =  vfx.GetComponent<VisualEffect>();
        
        if (visual.HasFloat("LifeTime"))
            visual.SetFloat("Lifetime",time);
        
        StartCoroutine(VFXTimer(time,vfxIndex,vfx));
    }
}
