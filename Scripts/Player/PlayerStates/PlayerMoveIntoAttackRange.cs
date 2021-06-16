using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMoveIntoAttackRange : IState
{
    private Player _player;  
    private PlayerMotor _motor;
    private Animator _animator;
    private PlayerCombat _combat;
    private PlayerStats _stats;
    private NavMeshAgent _agent;

    private float _initialStoppingDistance;

    public PlayerMoveIntoAttackRange(Player player, PlayerMotor motor, PlayerCombat combat, NavMeshAgent agent)
    {
        _player = player;
        _motor = motor;
        _combat = combat;
        _agent = agent;
    }

    public void OnEnter()
    {
        //distance check so it only works if enemy is closeby
        /*
        float distance2 = Vector3.Distance(transform.position, target.position);
        if (attackRange >= distance2) return;
        if (distance2 >= 40f) return;
        */   
        _initialStoppingDistance = _agent.stoppingDistance;

        //placeholder because of very big objects
        _agent.stoppingDistance = (_combat.attackRange - 2);
    }

    public void Tick()
    {
        _motor.MoveToPoint(_combat.attackTarget.position);
    }

    public void OnExit()
    {
        _agent.stoppingDistance = _initialStoppingDistance;
    }
}
