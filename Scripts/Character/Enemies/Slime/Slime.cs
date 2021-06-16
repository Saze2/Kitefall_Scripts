using UnityEngine;
using UnityEngine.AI;
using System;

public class Slime : Enemy
{
    private StateMachine _stateMachine;
    [SerializeField] private float lookRadius = 13f;
    private Transform player;
    [HideInInspector] public bool isAttacked = false;
    private Vector3 spawnPos;
    public Transform[] points;
    public float dashSpeed;
    public int healingMultiplicator = 50;

    [SerializeField] public bool canDash = false;
    private bool endDashState = false;
    [SerializeField] private bool doesPatrol = false;
    //[SerializeField] private bool canChase = true;

    private int damageMultiplicator = 2;
    [HideInInspector] public bool didDamage = true;
    [HideInInspector] public bool dashAvailable = false;
    [HideInInspector] public bool dashPrepared = false;

    private SlimeStats stats;
    

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        var agent = GetComponent<NavMeshAgent>();
        var combat = GetComponent<CharacterCombat>();
        var animator = GetComponentInChildren<Animator>();
        stats = GetComponent<SlimeStats>();
        spawnPos = transform.position;

        _stateMachine = new StateMachine();

        //states
        var idle = new SlimeIdle(this, agent, spawnPos, stats); ; //placeholder
        var chase = new SlimeChase(this, agent, player);
        var attack = new SlimeAttack(this, player, combat);
        var patrol = new SlimePatrol(this, agent);
        var dash = new SlimeDash(this, agent, player, animator);
        var prepareDash = new SlimePrepareDash(this, agent, player, animator);

        //transitions
        At(idle, chase, PlayerDetected());
        At(chase, idle, PlayerOutOfLookRadius());
        At(patrol, chase, PlayerDetected());
        At(idle, patrol, SlimeFullHP());

        At(chase, attack, InAttackRange());
        At(attack, chase, OutAttackRange());

        At(chase, prepareDash, Dash());
        At(prepareDash, dash, DashPrepared());
        At(dash, chase, DashExecuted());


        _stateMachine.SetState(idle);

        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        Func<bool> PlayerDetected() => () => (Vector3.Distance(transform.position, player.position) < lookRadius) || (isAttacked == true);
        Func<bool> PlayerOutOfLookRadius() => () => ((Vector3.Distance(transform.position, player.position) > lookRadius) && (isAttacked == false));
        Func<bool> InAttackRange() => () => Vector3.Distance(transform.position, player.position) < combat.attackRange;
        Func<bool> OutAttackRange() => () => Vector3.Distance(transform.position, player.position) > combat.attackRange;
        Func<bool> SlimeFullHP() => () => (stats.currentHealth == stats.maxHealth) && (points.Length != 0) && (doesPatrol == true);
        Func<bool> Dash() => () => (canDash == true) && (dashAvailable == true);
        Func<bool> DashPrepared() => () => (dashPrepared == true);
        Func<bool> DashExecuted() => () => endDashState == true;

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
            //instantiate hit effect??
        }
    }

    #region StateCD

    public void DashCompleted()
    {
        Invoke("SetEndDashState", 1.2f);
        Invoke("SetDashCooldown", UnityEngine.Random.Range(2f, 7f));
    }

    public void PrepareDash()
    {
        Invoke("SetEndDashPrepareState", 0.8f);
    }

    public void MakeDashAvailable()
    {

        Invoke("dashIsAvailable", UnityEngine.Random.Range(2f, 5f));
    }

    private void dashIsAvailable()
    {
        dashAvailable = true;
    }

    private void SetEndDashPrepareState()
    {
        dashPrepared = true;
    }

    private void SetEndDashState()
    {
        endDashState = true;
    }

    private void SetDashCooldown()
    {
        dashPrepared = false;
        endDashState = false;
        dashAvailable = true;
        canDash = true;
    }

    #endregion
}
