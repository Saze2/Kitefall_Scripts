using UnityEngine;
using UnityEngine.AI;

public class SlimeIdle : IState
{
    private Slime _slime;
    private NavMeshAgent _agent;
    private Vector3 _spawnPos;
    private SlimeStats _stats;

    public SlimeIdle(Slime slime, NavMeshAgent agent, Vector3 spawnPos, SlimeStats stats)
    {
        _slime = slime;
        _agent = agent;
        _spawnPos = spawnPos;
        _stats = stats;
    }

    public void OnEnter()
    {
        _agent.destination = _spawnPos;
        _stats.healthReg *= _slime.healingMultiplicator;
    }

    public void Tick()
    {
        if ( Vector3.Distance(_slime.transform.position, _spawnPos) < 2f)
        {
            _agent.isStopped = true;

            //heal?
        }

    }

    public void OnExit()
    {
        _stats.healthReg = _stats.healthRegen.GetValue();
        _agent.isStopped = false;
    }
}
