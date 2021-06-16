using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IState
{
    private Player _player;
    private PlayerMotor _motor;
    private Animator _animator;
    private PlayerCombat _combat;
    private PlayerStats _stats;

    public PlayerInput(Player player)
    {
        _player = player;
    }

    public void OnEnter()
    {

    }

    public void Tick()
    {

    }

    public void OnExit()
    {

    }
}
