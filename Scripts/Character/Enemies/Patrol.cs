using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Patrol : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 2f)
        {
            
            GotoNextPoint();
        }
            
    }

    void TurnTowardsNextPoint(Vector3 point)
    {
        
    }


    void GotoNextPoint()
    {
        if (points.Length == 0) return;
        
        Vector3 point = points[destPoint].position;
        TurnTowardsNextPoint(point);
        agent.destination = point;
        destPoint = (destPoint + 1) % points.Length;
    }
}
