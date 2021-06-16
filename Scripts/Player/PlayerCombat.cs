using System.Collections;
using UnityEngine;

public class PlayerCombat : CharacterCombat
{
    public Transform arrowPrefab;
    public Transform arrowSpawnPoint;
    private PlayerController cont;

    //arrowCone ability
    public GameObject arrowConePrefab;
    public GameObject singleArrowPrefab;
    private Vector3 hitPoint;

    private int speedBurstMod = 100;
    public GameObject moveSpeedBurstPrefab;
    public Transform backInstantionPoint;

    private void Start()
    {
        cont = GetComponent<PlayerController>();
    }

    public override void Attack(Transform target)
    {
        if (target == null) return;
        attackTarget = target;

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= attackRange)
        {
            if (attackCooldown >= 0f) return;
            AttackAnimation();
            attackCooldown = 1f / attackSpeed;
        }
        else
        {
            cont.playerControllerActive = false;
            movingIntoRange = true;
        }
    }

    public void SetHitPoint(Vector3 hitpoint)
    {
        //for ArrowCone
        hitPoint = hitpoint;
    }

    public void ShootMissile()
    {
        MissileInstantiationEffects();
        Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);      
        
    }

    public void MissileInstantiationEffects()
    {
        speedBurstMod = characterStats.speedBurst.GetValue();
        float x = (float) speedBurstMod / 100;
        motor.MovementSpeedBurst(x);
        
        if (speedBurstMod >= 130)
        {
            Instantiate(moveSpeedBurstPrefab, backInstantionPoint.position, backInstantionPoint.rotation, backInstantionPoint);
        }
    }

    public void ShootArrowCone()
    {
        Instantiate(arrowConePrefab, arrowSpawnPoint.position, Quaternion.LookRotation((hitPoint - transform.position).normalized));
    }

    public void ShootSingleArrow()
    {
        Instantiate(singleArrowPrefab, arrowSpawnPoint.position, Quaternion.LookRotation((hitPoint - transform.position).normalized));
    }

    public override void AttackAnimation()
    {
        animator.AttackClick();
    }
}


