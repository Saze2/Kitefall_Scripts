using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.WSA;

public class SlimeBossJump : IState
{
    private SlimeBoss _slime;
    private NavMeshAgent _agent;
    private Transform _player;
    private Animator _anim;

    private float _time = 0;
    private float _speed = 1;
    private Vector3 offset = new Vector3(0, 10, 0);
    private Vector3 _startPoint;
    private Vector3 _targetPoint;
    private Vector3 _middlePoint;
    public GameObject particle;
    private Vector3 _point1;
    private Vector3 _point2;

    public SlimeBossJump(SlimeBoss slime, NavMeshAgent agent, Transform player, Animator animator)
    {
        _slime = slime;
        _agent = agent;
        _player = player;
        _anim = animator;
    }

    public void OnEnter()
    {
        _speed = _slime.jumpSpeed;
        Transform slimeTransform = _slime.transform;

        _slime.didDamage = false;    
        _agent.updateRotation = false;
        slimeTransform.LookAt(_player.position); 
        _agent.enabled = false;

        _startPoint = slimeTransform.position;
        _targetPoint = _player.position + (new Vector3(0, slimeTransform.localScale.y, 0) / 2);
        _middlePoint = (_startPoint + _startPoint + _targetPoint) / 3 + offset;

        _point1 = _startPoint;
        _point2 = _middlePoint;
        _time = 0;

    }

    public void Tick()
    {
        _time += Time.deltaTime * _speed;
        _point1 = Vector3.Lerp(_startPoint, _middlePoint, _time);
        _point2 = Vector3.Lerp(_middlePoint, _targetPoint, _time);
        if (_slime.reachedTarget == false) _slime.transform.position = Vector3.Lerp(_point1, _point2, _time);

        if (_point1 == _middlePoint && _point2 == _targetPoint)
        {
            _slime.reachedTarget = true;
        }
    }

    public void OnExit()
    {
        _anim.ResetTrigger("land");
        _anim.SetTrigger("land");

        _agent.enabled = true;
        _slime.transform.LookAt(_player.position);
        _agent.updateRotation = true;
        _slime.didDamage = true;
        _slime.reachedTarget = false;

        _slime.PlayLandParticles(_point2);
    }



}
