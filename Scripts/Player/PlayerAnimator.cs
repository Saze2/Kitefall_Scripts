using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    UnityEngine.AI.NavMeshAgent agent;
    const float locomotionAnimationSmoothTime = 0.15f;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        SetSpeedPercent();   
    }

    public void SetSpeedPercent()
    {
        if (agent.speed != 0)
        {
            float speedPercent = agent.velocity.magnitude / agent.speed;
            animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
        }
    }

    public void AttackClick()
    {
        animator.ResetTrigger("attack");
        animator.SetTrigger("attack");
        animator.SetBool("isAttacking", true);
    }

    public void AbilityW()
    {
        animator.ResetTrigger("abilityW");
        animator.SetTrigger("abilityW");
        animator.SetBool("isAttacking", true);
    }

    public void shootSingleArrow()
    {
        animator.ResetTrigger("singleArrow");
        animator.SetTrigger("singleArrow");
        animator.SetBool("isAttacking", true);
    }

    public void cancelAttack() 
    {
        animator.SetBool("isAttacking", false);
    }

    public void Die()
    {
        animator.ResetTrigger("dead");
        animator.SetTrigger("dead");
    }
}
