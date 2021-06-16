using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.WSA;

public class SlimeBossDash : IState
{
    private SlimeBoss _slime;
    private NavMeshAgent _agent;
    private Transform _player;
    private Animator _anim;
    private float _initialAcceleration;
    private Vector3 _targetVec;
    private float _speed = 15f;

    public SlimeBossDash(SlimeBoss slime, NavMeshAgent agent, Transform player, Animator anim)
    {
        _slime = slime;
        _agent = agent;
        _player = player;
        _anim = anim;
        
    }

    public void OnEnter()
    {
        _speed = _slime.dashSpeed;
        _slime.didDamage = false;
        
        _targetVec = (_player.position - _slime.transform.position).normalized;

        _initialAcceleration = _agent.acceleration;
        _agent.acceleration = 50;

        _agent.updateRotation = false;
        _slime.transform.LookAt(_player.position);

        _slime.dashAvailable = false;


        if (_slime.alreadyDashed == false) _slime.alreadyDashed = (Random.value > 0.5f);            
        if (_slime.alreadyDashed == true)
        {
            _slime.EndState(_slime.shoot, 0.9f);
            _slime.alreadyDashed = false;
        }
        else
        {
            _slime.alreadyDashed = true;
            _slime.InvokeRepeatState(_slime.prepareDash, 1.1f);
        }
        


        //dash animation start
    }

    public void Tick()
    {
        _agent.Move(_targetVec * _speed * Time.deltaTime);
    }

    public void OnExit()
    {
        _slime.dashAvailable = false;
        _agent.Move(Vector3.zero);
        //FacePoint();
        _slime.transform.LookAt(_player.position);
        _agent.updateRotation = true;
        _agent.acceleration = _initialAcceleration;
        _slime.didDamage = true;
    }

    public void FacePoint()
    {
        Vector3 direction = (_player.position - _slime.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        _slime.transform.rotation = Quaternion.Slerp(_slime.transform.rotation, lookRotation, Time.deltaTime * 100);

    }
}
