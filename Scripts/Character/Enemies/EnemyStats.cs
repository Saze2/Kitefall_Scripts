using UnityEngine;
using TMPro;
using System.Collections;

public class EnemyStats : CharacterStats
{
    public EnemyHealthbar healthbar;
    public GameObject damageNumberUI;


    private void Start()
    {
        healthbar.SetMaxHealth(maxHealth);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        healthbar.SetHealth(currentHealth);
        
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
