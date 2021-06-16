using UnityEngine;
using UnityEngine.AI;

public class SlimePatrol : IState
{
    private Slime _slime;
    private NavMeshAgent _agent;

    
    private int destPoint = 0;

    public SlimePatrol(Slime slime, NavMeshAgent agent)
    {
        _slime = slime;
        _agent = agent;
    }

    public void OnEnter()
    {
        GotoNextPoint();
    }

    public void Tick()
    {
        if (!_agent.pathPending && _agent.remainingDistance < 2f)
        {
            GotoNextPoint();
        }
    }

    public void OnExit()
    {

    }

    void GotoNextPoint()
    {
        if (_slime.points.Length == 0) return;

        Vector3 point = _slime.points[destPoint].position;
        //TurnTowardsNextPoint(point);
        _agent.destination = point;
        destPoint = (destPoint + 1) % _slime.points.Length;
    }
}
