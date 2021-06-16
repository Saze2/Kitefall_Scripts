using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeBossEnraged : IState
{
    private SlimeBoss _slime;
    private NavMeshAgent _agent;
    private Transform _player;
    private Animator _anim;

    private float _initialSpeed;


    public SlimeBossEnraged(SlimeBoss slime, NavMeshAgent agent, Transform player, Animator anim)
    {
        _slime = slime;
        _agent = agent;
        _player = player;
        _anim = anim;

    }

    public void OnEnter()
    {
        _slime.EndState(_slime.chase, 1f);
        _slime.alreadyEnraged = true;
        _initialSpeed = _agent.speed;
        _agent.speed = 0;
        //_slime.PlayEnrageParticles();
        _anim.SetTrigger("enrage");

        _slime.SpawnAdds();
    }

    public void Tick()
    {

    }

    public void OnExit()
    {
        _agent.speed = _initialSpeed;
    }
}
