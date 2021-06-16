using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : IState
{
    private Slime _slime;
    private Transform _player;
    private CharacterCombat _combat;

    public SlimeAttack(Slime slime, Transform player, CharacterCombat combat)
    {
        _slime = slime;
        _player = player;
        _combat = combat;
    }

    public void OnEnter()
    {
        
    }

    public void Tick()
    {
        _combat.Attack(_player);
    }

    public void OnExit()
    {

    }
}
