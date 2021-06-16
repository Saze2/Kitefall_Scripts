using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimator : MonoBehaviour
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
                animator.SetFloat("speed", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
            }
    }

    public void Tackle()
    {
        animator.ResetTrigger("attack");
        animator.SetTrigger("attack");
    }
}
