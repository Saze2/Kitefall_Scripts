using UnityEngine;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    Transform target;
    bool chasing = false;
    public Vector3 point;
    public bool isTurning;

    private float _initialSpeed;
    
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _initialSpeed = agent.speed;
    }


    void Update ()
    {
        if (target != null)
        {
            FaceTarget();

            if (chasing == true)
            {
                agent.SetDestination(target.position); 
            }
        }

        if (point != null)
        {
            if(isTurning == true)
            {
                FacePoint();
            }
        }
    }

    public void MoveToPoint (Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget (Interactable newTarget)
    {
        chasing = true;
        agent.stoppingDistance = newTarget.radius * 0.8f;
        agent.updateRotation = false;
        target = newTarget.interactionTransform;
    }

    public void SetTarget(Transform newTarget)
    {
        chasing = false;
        agent.updateRotation = false;
        target = newTarget;
    }

    public void StopFollowingTarget()
    {
        chasing = false;
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }

    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);

    }

    public void FacePoint()
    {
        Vector3 direction = (point - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 100);

    }

    public void StopMoving()
    {
        chasing = false;
        StopFollowingTarget(); 
        agent.SetDestination(agent.transform.position);
    }

    public void MovementSpeedBurst(float speedMod)
    {
        agent.speed *= speedMod;
        Invoke("ResetSpeed", 1.5f);

        //faster speed particles
    }



    #region SetSpeed
    public void SpeedZero()
    {
        agent.speed = 0;
    }

    public void ResetSpeed()
    {
        agent.speed = _initialSpeed;
    }

    #endregion
}
