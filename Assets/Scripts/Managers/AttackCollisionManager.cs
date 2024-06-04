using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollisionManager : MonoBehaviour
{
    [SerializeField]protected WeaponCollision[] weaponColliders = new WeaponCollision[0];
    [SerializeField]protected Transform hurtBoxCollection;
    private Collider[] hurtBoxes;
    public LayerMask enemyLayers;
    public bool m_Started = false;
    [SerializeField]EntityCore coreMechanic;

    public void DisableCollision()
    {
        foreach(WeaponCollision weaponCol in weaponColliders)
        {
            weaponCol.ActivateCollider(false);
        }
    }

    void Start()
    {
        coreMechanic = GetComponent<EntityCore>();

        foreach(WeaponCollision wepCol in weaponColliders)
        {
            wepCol.Initialize(GetComponent<AttackCollisionManager>());
            wepCol.ActivateCollider(false);
        }
        int n = hurtBoxCollection.childCount;
        hurtBoxes = new Collider[n];

        for (int i = 0; i < n; i++)
        {
            hurtBoxes[i] = hurtBoxCollection.GetChild(i).GetComponent<Collider>();
            hurtBoxCollection.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void AttackScan(int i)
    {
        BoxCollider col = (BoxCollider)hurtBoxes[i];
        col.gameObject.SetActive(true);
        //Collider[] entitiesHit = Physics.OverlapBox(col.transform.position, col.transform.localScale / 2, Quaternion.identity,enemyLayers);
        Collider[] entitiesHit = Physics.OverlapBox(col.transform.position, col.size,col.transform.localRotation,enemyLayers);

        GameObject g;
        if (entitiesHit.Length > 0)
        {
            foreach(Collider target in entitiesHit)
            {
                if (target.CompareTag("Enemy") || target.CompareTag("Player"))
                {
                    coreMechanic.InflictDamage(target.gameObject);
                    g = (HitImpactPool.createHit)?.Invoke(0);
                    g.transform.position = target.transform.position;
                }
            }
        }
        else
        {
            Debug.Log("Whiffed");
        }

        col.gameObject.SetActive(false);
    }

    public void WeaponCollideData(GameObject target)
    {
        if (target.CompareTag("Enemy") || target.CompareTag("Player"))
        {
            coreMechanic.InflictDamage(target);
        }
    }

    public void ActivateWeaponCollider(int i)
    {
        weaponColliders[i].ActivateCollider(true);
    }

    public void DeactivateWeaponCollider(int i)
    {
        weaponColliders[i].ActivateCollider(false);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_Started)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(hurtBoxes[0].transform.position, ((BoxCollider)hurtBoxes[0]).size);
    }
}
