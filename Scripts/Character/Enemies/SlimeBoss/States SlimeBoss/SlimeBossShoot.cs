using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeBossShoot : IState
{
    private SlimeBoss _slime;
    private NavMeshAgent _agent;
    private Transform _player;
    private Animator _anim;

    private float _initialSpeed;

    public SlimeBossShoot(SlimeBoss slime, NavMeshAgent agent, Transform player, Animator anim)
    {
        _slime = slime;
        _agent = agent;
        _player = player;
        _anim = anim;
        

    }

    public void OnEnter()
    {        
        _initialSpeed = _agent.speed;
        _agent.speed = 0;
        _agent.updateRotation = false;
        _slime.transform.LookAt(_player.position);

        _slime.ShootBullet();

        _anim.ResetTrigger("shoot");
        _anim.SetTrigger("shoot");


        if (_slime.bulletCounter < _slime.bulletMax)
        {
            _slime.bulletCounter += 1;
            _slime.InvokeRepeatState(_slime.shoot, 0.3f);

        }
        else
        {
            _slime.EndState(_slime.prepareJump, 1f);
            _slime.bulletCounter = 0;

        }
               
    }

    public void Tick()
    {


    }

    public void OnExit()
    {
        _agent.updateRotation = true;
        _agent.speed = _initialSpeed;
    }
}

