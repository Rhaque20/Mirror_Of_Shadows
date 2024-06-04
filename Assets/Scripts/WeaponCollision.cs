using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    private AttackCollisionManager attackCollision;
    private Collider attackBox;

    // Start is called before the first frame update

    public void Initialize(AttackCollisionManager attackCol)
    {
        attackCollision = attackCol;
        attackBox = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit!");
            attackCollision.WeaponCollideData(col.gameObject);
            GameObject g = (HitImpactPool.createHit)?.Invoke(0);
            g.transform.position = transform.position;
        }
    }

    public void ActivateCollider(bool activate)
    {
        attackBox.enabled = activate;
    }

}
