using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitImpactPool : MonoBehaviour
{
    private Dictionary<EnumLib.HitImpacts,Queue<GameObject>> _hitImpacts = new Dictionary<EnumLib.HitImpacts, Queue<GameObject>>();
    [SerializeField]private GameObject[] _hitPrefab = new GameObject[1];
    [SerializeField]private int _poolCount = 20;
    public static Action<ParticleDuration> returnToPool;
    public static Func<int,GameObject> createHit;

    void AddChild(GameObject g)
    {
        g.transform.SetParent(this.transform,false);
    }

    void ReturnToPool(ParticleDuration particle)
    {
        EnumLib.HitImpacts index = particle.impactType;
        particle.gameObject.SetActive(false);
        _hitImpacts[index].Enqueue(particle.gameObject);
    }

    GameObject CreateHitEffect(int index)
    {
        Debug.Log("Created hit");
        GameObject g = _hitImpacts[(EnumLib.HitImpacts)index].Dequeue();
        g.SetActive(true);
        g.GetComponent<ParticleDuration>().StartCountdown(0.1f);
        return g;
    }

    void Start()
    {
        Array types = Enum.GetValues(typeof(EnumLib.HitImpacts));
        int n = Mathf.Min(types.Length,_hitPrefab.Length);
        Queue<GameObject> temp;
        GameObject g;

        createHit = CreateHitEffect;
        returnToPool = ReturnToPool;

        for(int i = 0; i < n; i++)
        {
            temp = new Queue<GameObject>();
            for (int j = 0; j < _poolCount; j++)
            {
                g = Instantiate(_hitPrefab[i]);
                g.GetComponent<ParticleDuration>().Initialize((EnumLib.HitImpacts)i);
                g.SetActive(false);
                AddChild(g);
                temp.Enqueue(g);
            }

            _hitImpacts.Add((EnumLib.HitImpacts)i,temp);
        }
    }
}
