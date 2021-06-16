using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    protected float attackCooldown = 0f;
    [HideInInspector] public float attackRange;
    [HideInInspector] public bool followEnemy = false; 
    public Transform attackTarget;
    protected PlayerAnimator animator;
    protected CharacterStats characterStats;
    protected PlayerMotor motor;
    protected UnityEngine.AI.NavMeshAgent agent;
    public bool movingIntoRange = false;

    private void Awake()
    {
        characterStats = GetComponent<CharacterStats>();
        animator = GetComponent<PlayerAnimator>();
        motor = GetComponent<PlayerMotor>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
       
        attackRange = characterStats.attackRange.GetValue();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    public virtual void Attack(Transform target)
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
    }

    public void DoDamage(Transform target)
    {
        var damage = characterStats.damage.GetValue();
        damage = Random.Range(damage + (damage / 10), damage - (damage / 10));
        target.GetComponent<CharacterStats>().TakeDamage(damage);
    }

    public virtual void AttackAnimation ()
    {    
        //execute Attack animation of the character
    }
}
