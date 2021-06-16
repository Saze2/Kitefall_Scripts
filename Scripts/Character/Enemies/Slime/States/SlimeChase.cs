using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SlimeChase : IState
{
    private Slime _slime;
    private NavMeshAgent _agent;
    private Transform _player;

    public SlimeChase(Slime slime, NavMeshAgent agent, Transform player)
    {
        _slime = slime;
        _agent = agent;
        _player = player;
    }

    public void OnEnter()
    {
        _slime.MakeDashAvailable();   
    }

    public void Tick()
    {
        _agent.SetDestination(_player.position);
    }

    public void OnExit()
    {

    }
}
