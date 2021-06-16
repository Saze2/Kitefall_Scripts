using UnityEngine;
using TMPro;
using System.Collections;

public class SlimeStats : CharacterStats
{
    public EnemyHealthbar healthbar;
    public GameObject damageNumberUI;
    Slime slime;
    private float _isAttackedTimer = 0;
    [SerializeField] private float threatTime = 3;

    public GameObject deathExplosion;

    private void Start()
    {
        healthbar.SetMaxHealth(maxHealth);
        slime = GetComponent<Slime>();
    }

    public void Update()
    {
        _isAttackedTimer -= Time.deltaTime;
        if (_isAttackedTimer < 0)
        {
            slime.isAttacked = false;
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UpdateHealthbar();

        slime.isAttacked = true;
        _isAttackedTimer = threatTime;

        if (damageNumberUI)
        {
            ShowDamageNumber();
        }
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject, 0.1f);
        //add death animation 
        //add loot

        Instantiate(deathExplosion, transform.position, transform.rotation);
    }
    public override void UpdateHealthbar()
    {
        healthbar.SetHealth(currentHealth);
    }


    public void ShowDamageNumber()
    {
        var inst = Instantiate(damageNumberUI, transform.position, Quaternion.identity, transform);
        inst.GetComponent<TextMeshPro>().text = damageNumber.ToString();
    }
}
