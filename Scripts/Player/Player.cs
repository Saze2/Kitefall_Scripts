using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private StateMachine _stateMachine;


    private void Awake()
    {
        var cont = GetComponent<PlayerController>();
        var motor = GetComponent<PlayerMotor>();
        var animator = GetComponent<PlayerAnimator>();
        var combat = GetComponent<PlayerCombat>();
        var stats = GetComponent<PlayerStats>();
        var agent = GetComponent<NavMeshAgent>();

        _stateMachine = new StateMachine();

        var input = new PlayerInput(this);
        var playerAttack = new PlayerAttack(this, combat, motor, agent);
        var moveToAttack = new PlayerMoveIntoAttackRange(this, motor, combat, agent);

        //transitions
        At(input, moveToAttack, MoveIntoRange());
        At(moveToAttack, input, PlayerDoesInput());
        At(moveToAttack, playerAttack, PlayerInRange());
        //At(playerAttack, playerAttack, PlayerKeepsAttacking());
        At(playerAttack, input, AttackFinished());

        _stateMachine.SetState(input);

        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        Func<bool> PlayerDoesInput() => () => cont.playerControllerActive == true;
        Func<bool> MoveIntoRange() => () => combat.movingIntoRange == true && combat.attackTarget != null && cont.playerControllerActive == false;
        Func<bool> PlayerInRange() => () => Vector3.Distance(transform.position, combat.attackTarget.position) < combat.attackRange && cont.playerControllerActive == false;
        Func<bool> AttackFinished() => () =>   cont.playerControllerActive == true;
    }

    private void Update()
    {
        _stateMachine.Tick();
    }


}
