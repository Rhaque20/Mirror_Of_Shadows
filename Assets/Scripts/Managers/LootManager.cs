using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    Queue<GameObject> lootPool = new Queue<GameObject>();
    [SerializeField]GameObject lootPrefab;
    [SerializeField]int poolCount = 50;
    public static Action<GameObject> returnToPool{get;private set;}
    public static Action<Vector3,Item> generateLoot{get;private set;}
    public static Camera cam;
    // Start is called before the first frame update
    public void Initialize()
    {
        returnToPool = ReturnToPool;
        generateLoot = GenerateLoot;
        cam = Camera.main;
        GameObject g;
        for(int i = 0; i < poolCount; i++)
        {
            g = Instantiate(lootPrefab);
            g.transform.parent = this.transform;
            g.GetComponent<Loot>().Initialize(cam);
            g.name += " "+i.ToString();
            g.SetActive(false);
            lootPool.Enqueue(g);
        }
    }
    
    void Awake()
    {
        if (cam == null)
        {
            Initialize();
        }
    }

    public void GenerateLoot(Vector3 lootStartPos,Item loot)
    {
        GameObject temp = lootPool.Dequeue();
        temp.SetActive(true);
        temp.transform.position = lootStartPos;
        temp.GetComponent<Loot>().SetUpLoot(loot);
    }

    public void ReturnToPool(GameObject lootObject)
    {
        lootObject.SetActive(false);
        lootPool.Enqueue(lootObject);
    }
}
