using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.WSA;

public class SlimeBossPrepareJump : IState
{
    private SlimeBoss _slime;
    private NavMeshAgent _agent;
    private Transform _player;
    private Animator _anim;

    private float _initialSpeed;

    public SlimeBossPrepareJump(SlimeBoss slime, NavMeshAgent agent, Transform player, Animator anim)
    {
        _slime = slime;
        _agent = agent;
        _player = player;
        _anim = anim;
        
    }

    public void OnEnter()
    {
        _slime.EndState(_slime.jump, 1f);
        _initialSpeed = _agent.speed;
        _agent.speed = 0;
        _agent.updateRotation = false;
        //dash animation start

        _anim.ResetTrigger("prepJump");
        _anim.SetTrigger("prepJump");
    }

    public void Tick()
    {
        FacePoint();
    }

    public void OnExit()
    {
        _agent.speed = _initialSpeed;
        _agent.updateRotation = true;
    }

    public void FacePoint()
    {
        Vector3 direction = (_player.position - _slime.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        _slime.transform.rotation = Quaternion.Slerp(_slime.transform.rotation, lookRotation, Time.deltaTime * 100);

    }
}
