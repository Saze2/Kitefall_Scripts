using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.WSA;

public class SlimeDash : IState
{
    private Slime _slime;
    private NavMeshAgent _agent;
    private Transform _player;
    private Animator _anim;
    private float _initialAcceleration;
    private Vector3 _targetVec;
    private float speed = 15f;

    public SlimeDash(Slime slime, NavMeshAgent agent, Transform player, Animator anim)
    {
        _slime = slime;
        _agent = agent;
        _player = player;
        _anim = anim;
        
    }

    public void OnEnter()
    {
        _slime.canDash = false;
        _slime.didDamage = false;
        _slime.DashCompleted();
        
        _targetVec = (_player.position - _slime.transform.position).normalized;

        _initialAcceleration = _agent.acceleration;
        _agent.acceleration = 50;

        _agent.updateRotation = false;
        _slime.transform.LookAt(_player.position);
       

        //dash animation start
    }

    public void Tick()
    {
        _agent.Move(_targetVec * speed * Time.deltaTime);
    }

    public void OnExit()
    {
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
