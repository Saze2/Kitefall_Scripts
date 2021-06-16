using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//wrapper class to trigger events
public class ShootEventHandler : MonoBehaviour
{
    Animator animator;
    public PlayerCombat playerCombat;
    Transform player;

    public Transform BowDraw;
    public Transform BowRelease;
    public Transform Footsteps;
    
    SoundEffectVariation sfxBowDraw;
    SoundEffectVariation sfxBowRelease;
    SoundEffectVariation sfxFootsteps;

    void Start()
    {
        player = PlayerManager.instance.player.transform;
        animator = GetComponent<Animator>();
        playerCombat = player.GetComponent<PlayerCombat>();

        sfxBowDraw = BowDraw.GetComponent<SoundEffectVariation>();
        sfxBowRelease = BowRelease.GetComponent<SoundEffectVariation>();
        sfxFootsteps =  Footsteps.GetComponent<SoundEffectVariation>();

    }

    public void cancelAttack()
    {
        //Debug.Log("cancelled");
        animator.SetBool("isAttacking", false);
    }

    public void CheckRange()
    {
        if (Vector3.Distance(transform.position, playerCombat.attackTarget.position) >= playerCombat.attackRange)
        {
            cancelAttack();
        }
    }

    public void startShootAnimation()
    {
        
        //Debug.Log("test");
        playerCombat.ShootMissile();
        sfxBowRelease.PlayRandom();

    }

    public void startConeMissile()
    {
        playerCombat.ShootArrowCone();
        sfxBowRelease.PlayRandom();
    }

    public void startSingleArrow()
    {
        playerCombat.ShootSingleArrow();
        sfxBowRelease.PlayRandom();
    }


    public void OnBowDraw()
    {
        sfxBowDraw.PlayRandom();
    }

    public void PlayFootstep()
    {
        sfxFootsteps.PlayRandom();
    }
}
