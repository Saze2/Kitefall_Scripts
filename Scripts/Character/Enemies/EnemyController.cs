using UnityEngine;

public class EnemyController : Enemy
{
    public float lookRadius = 10f;
    public float radius = 15f;
    public bool isFollowingPlayer = true;
    public bool isAttacked = false;

    Transform player;
    UnityEngine.AI.NavMeshAgent agent;
    CharacterCombat combat;

    void Start()
    {
        player = PlayerManager.instance.player.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
    }

    void Update()
    {
        if (isFollowingPlayer)
        {
            EnemyFollowPlayer();
        }

    }

    public void EnemyFollowPlayer()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        
        if ((distance <= lookRadius) || (isAttacked == true))
        {
            agent.SetDestination(player.position);
            AttackPlayer();
            isAttacked = false;
        }
    }

    void AttackPlayer()
    {
        combat.Attack(player.transform);
        FaceTarget();

        //keep attacking
    }

    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}