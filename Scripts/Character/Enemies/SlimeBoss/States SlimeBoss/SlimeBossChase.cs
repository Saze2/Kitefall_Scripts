using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SlimeBossChase : IState
{
    private SlimeBoss _slime;
    private NavMeshAgent _agent;
    private Transform _player;
    private SlimeBossStats _stats;

    public SlimeBossChase(SlimeBoss slime, NavMeshAgent agent, Transform player, SlimeBossStats stats)
    {
        _slime = slime;
        _agent = agent;
        _player = player;
        _stats = stats;
    }

    public void OnEnter()
    {
        _stats.HealthbarSetActive();
        _slime.dashAvailable = false;
        _slime.InvokeCanDash(1.8f);

        if ((_stats.bossEnraged == true) && (_slime.alreadyEnraged == false)) _slime.EndState(_slime.enrage, 1.5f);

    }

    public void Tick()
    {
        _agent.SetDestination(_player.position);
    }

    public void OnExit()
    {
       
        
    }
}
