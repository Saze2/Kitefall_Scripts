using UnityEngine;
using UnityEngine.PlayerLoop;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; protected set; }
    protected float damageNumber; //for UI

    [HideInInspector] public int healthReg;

    public Stat damage;
    public Stat armor;
    public Stat attackRange;
    public Stat attackSpeed;
    public Stat healthRegen;
    public Stat speedBurst;

    private bool isAlive = true;


    void Awake()
    {
        SetHealthOnSpawn();
        
        healthReg = healthRegen.GetValue();

        InvokeRepeating("Regenerate", 0f, 1f);       
    }


    public virtual void TakeDamage (int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue); //keep the value positive

        currentHealth -= damage;

        //UnityEngine.Debug.Log(transform.name + "takes " + damage + " damage");
        damageNumber = damage;

        if ((currentHealth <= 0) && (isAlive == true))
        {
            isAlive = false;
            Die();
        }
    }
    public virtual void Die()
    {
        //UnityEngine.Debug.Log(transform.name + "died");
    }

    private void Regenerate()
    {
        if (currentHealth >= maxHealth) return;
        currentHealth += (1 * healthReg);

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthbar();
    }

    public virtual void SetHealthOnSpawn()
    {
        currentHealth = maxHealth;
        isAlive = true;

        UpdateHealthbar();
    }

    public virtual void UpdateHealthbar()
    {

    }

}
