using UnityEngine;
using UnityEngine.AI;

public class SlimeBossIdle : IState
{
    private SlimeBoss _slime;
    private NavMeshAgent _agent;
    private Vector3 _spawnPos;
    private SlimeBossStats _stats;

    public SlimeBossIdle(SlimeBoss slime, NavMeshAgent agent, Vector3 spawnPos, SlimeBossStats stats)
    {
        _slime = slime;
        _agent = agent;
        _spawnPos = spawnPos;
        _stats = stats;
    }

    public void OnEnter()
    {
        _stats.HealthbarSetInactive();
        _agent.destination = _spawnPos;
        _stats.healthReg *= _slime.healingMultiplicator;
    }

    public void Tick()
    {
        if ( Vector3.Distance(_slime.transform.position, _spawnPos) < 2f)
        {
            _agent.isStopped = true;

        }

    }

    public void OnExit()
    {
        _stats.healthReg = _stats.healthRegen.GetValue();
        _agent.isStopped = false;

        _slime.StartSlimeBossFightEvent();
    }
}
