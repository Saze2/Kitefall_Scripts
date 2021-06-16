using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.Events;

public class SlimeBoss : Enemy
{
    public StateMachine _stateMachine;
    [SerializeField] private float lookRadius = 12f;
    private Transform player;
    [HideInInspector] public bool isAttacked = false;
    [HideInInspector] private Vector3 spawnPos;
    public float dashSpeed;
    public float jumpSpeed = 1.5f;
    public int healingMultiplicator = 50;
    private int damageMultiplicator = 2;
    [HideInInspector] public bool didDamage = true;
    [HideInInspector] public bool dashAvailable = false;
    [HideInInspector] public bool dashPrepared = false;

    private SlimeBossStats stats;
    private IState _newState;
    public IState dash;
    public IState chase;
    public IState prepareShoot;
    public IState shoot;
    public IState prepareJump;
    public IState jump;
    public IState prepareDash;
    public IState enrage;

    [HideInInspector] public bool alreadyDashed = false;
    public GameObject bullet;
    [HideInInspector]  public int bulletCounter = 0;
    public int bulletMax = 20;
    public Transform missileSpawnPoint;

    [HideInInspector] public bool reachedTarget = false;
    public GameObject jumpEndParticles;
    //public GameObject enrageParticles;
    [HideInInspector] public bool alreadyEnraged = false;

    //public Transform addSpawner;
    //public Transform addSpawner2;
    //public GameObject SlimeAdd;
    //SlimeSpawner slimeSpawner;

    [SerializeField] UnityEvent OnSlimeBossStartFight;
    [SerializeField] UnityEvent OnSlimeBossEnrage;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        var agent = GetComponent<NavMeshAgent>();
        var combat = GetComponent<CharacterCombat>();
        var animator = GetComponentInChildren<Animator>();
        stats = GetComponent<SlimeBossStats>();
        spawnPos = transform.position;

        _stateMachine = new StateMachine();

        //states
        var idle = new SlimeBossIdle(this, agent, spawnPos, stats);
        chase = new SlimeBossChase(this, agent, player, stats);
        dash = new SlimeBossDash(this, agent, player, animator);
        prepareDash = new SlimeBossPrepareDash(this, agent, player, animator);
        //prepareShoot = new SlimeBossShoot(this, agent, player);
        shoot = new SlimeBossShoot(this, agent, player, animator);
        prepareJump = new SlimeBossPrepareJump(this, agent, player, animator);
        jump = new SlimeBossJump(this, agent, player, animator);

        enrage = new SlimeBossEnraged(this, agent, player, animator);


        //transitions
        At(idle, chase, PlayerDetected());
        At(chase, idle, PlayerOutOfLookRadius());
        At(chase, prepareDash, Dash());
        At(jump, chase, JumpEnded());


        _stateMachine.SetState(idle);

        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        Func<bool> PlayerDetected() => () => (Vector3.Distance(transform.position, player.position) < lookRadius) || (isAttacked == true);
        Func<bool> PlayerOutOfLookRadius() => () => ((Vector3.Distance(transform.position, player.position) > lookRadius) && (isAttacked == false));
        Func<bool> Dash() => () => (dashAvailable == true);
        Func<bool> JumpEnded() => () => (reachedTarget == true);

        //dash
        //missile launching counter
    }


    private void Update() => _stateMachine.Tick();

    public void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (didDamage == true) return;

        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<CharacterStats>().TakeDamage(stats.damage.GetValue() * damageMultiplicator);
            didDamage = true;

            //instantiate hit effect
        }
    }

    public void ShootBullet()
    {
        Instantiate(bullet, missileSpawnPoint.position, missileSpawnPoint.rotation);
    }

    public void EndState(IState newState, float timer)
    {
        _newState = newState;
        Invoke("SetNewState", timer);
    }

    public void InvokeRepeatState(IState newState, float timer)
    {
        _newState = newState;
        Invoke("RepeatState", timer);
    }

    private void RepeatState()
    {
        _stateMachine.RepeatState(_newState);
    }

    public void SetNewState()
    {
        _stateMachine.SetState(_newState);
    }

    public void InvokeCanDash(float time)
    {
        Invoke("CanDash", time);
    }
    private void CanDash()
    {
        dashAvailable = true;
    }

    public void PlayLandParticles(Vector3 targetPoint)
    {
        Instantiate(jumpEndParticles, targetPoint, transform.rotation);
    }

    public void PlayEnrageParticles()
    {
        //Instantiate(enrageParticles, transform.position, transform.rotation);
    }

    public void SpawnAdds()
    {
        OnSlimeBossEnrage.Invoke();
        OnSlimeBossEnrage.RemoveAllListeners();
        //Instantiate(SlimeAdd, addSpawner.position, addSpawner.rotation);
        //Instantiate(SlimeAdd, addSpawner2.position, addSpawner2.rotation);
    }

    public void StartSlimeBossFightEvent()
    {
        OnSlimeBossStartFight.Invoke();
        OnSlimeBossStartFight.RemoveAllListeners();
    }

}