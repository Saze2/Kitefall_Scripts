using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttack : IState
{
    private Player _player;
    private PlayerMotor _motor;
    private Animator _animator;
    private PlayerCombat _combat;
    private PlayerStats _stats;
    private NavMeshAgent _agent;

    public PlayerAttack(Player player, PlayerCombat combat, PlayerMotor motor, NavMeshAgent agent)
    {
        _player = player;
        _combat = combat;
        _motor = motor;
        _agent = agent;
    }

    public void OnEnter()
    {
        _combat.Attack(_combat.attackTarget);
        _motor.StopMoving();
        _player.transform.LookAt(_combat.attackTarget.position);
    }

    public void Tick()
    {


    }

    public void OnExit()
    {

    }
}
