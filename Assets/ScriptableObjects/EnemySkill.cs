using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemySkill", menuName = "EnemySkill")]
public class EnemySkill : Skill
{
    public float rangeReq = 0f, heightReq = 2f;
    public float cooldown = 30f,curCooldown = 0f, startCooldown = 0f;
    public int priority = 99;
    public float actionInterval = 3f;
    public bool canRotate = false;


}
