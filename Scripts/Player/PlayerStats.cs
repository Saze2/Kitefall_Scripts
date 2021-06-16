using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    PlayerAnimator animator;
    public EnemyHealthbar healthbar;
    public Image damageScreen;
    
    
    //red damage screen
    Color damagedColor = new Color(178f, 34f, 34f, 0.2f); //firebrick rgb, alpha is last float%
    float smoothColor = 5f;
    bool damaged = false;
    public GameObject _bloodSplatter;

    public int SetcurrentHealth { get; private set; }

    private void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += onEquipmentChanged;
        animator = GetComponent<PlayerAnimator>();
        healthbar.SetMaxHealth(maxHealth);
        damaged = false;

        InitialzeEquipment();
    }

    private void Update()
    {
        if (damaged)
        {
            damageScreen.color = damagedColor;
        }
        else
        {
            damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, smoothColor * Time.deltaTime);

        }
        damaged = false;
    }

    private void InitialzeEquipment()
    {
        for(int i = 0; i < EquipmentManager.instance.currentEquipment.Length; i++)
        {
            Equipment newItem = EquipmentManager.instance.currentEquipment[i];
            onEquipmentChanged(newItem, null);
            newItem = null;
        }
    }
    

    //change stats when changing equipment
    void onEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
            //attackRange
            //attackSpeed

            //health regen           
            speedBurst.AddModifier(newItem.speedBurstModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
            speedBurst.RemoveModifier(oldItem.speedBurstModifier);
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UpdateHealthbar();
        damaged = true;
        Instantiate(_bloodSplatter, PlayerManager.instance.player.transform.position, PlayerManager.instance.player.transform.rotation);
    }

    public override void Die()
    {
        base.Die();
        animator.Die();
        damageScreen.color = damagedColor;
        PlayerManager.instance.KillPlayer();
    }


    public void Heal(int healthGain)
    {
        if (currentHealth >= maxHealth) return;
        currentHealth += healthGain;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthbar();
    }

    public override void UpdateHealthbar()
    {
        healthbar.SetHealth(currentHealth);
    }
}
