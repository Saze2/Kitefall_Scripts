using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterStats))]
public class EnemyCombat : CharacterCombat
{
    SlimeAnimator slimeAnim;

    private void Start()
    {
        slimeAnim = GetComponent<SlimeAnimator>();
    }

    public override void AttackAnimation()
    {
        base.AttackAnimation();
        slimeAnim.Tackle();

        StartCoroutine(AttackDelay(attackTarget));
    }

    IEnumerator AttackDelay(Transform attackTarget)
    {
        yield return new WaitForSeconds(0.4f);

        DoDamage(attackTarget);
    }
}

