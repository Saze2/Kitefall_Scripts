using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlimeBossStats : EnemyStats
{
    public GameObject healthbarCanvas;
    public bool bossEnraged = false;

    [SerializeField] UnityEvent OnSlimeBossDeath;

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (currentHealth <= maxHealth / 3) bossEnraged = true; ;
    }

    public void HealthbarSetActive()
    {
        if (healthbarCanvas.activeSelf == false) healthbarCanvas.SetActive(true);
    }

    public void HealthbarSetInactive()
    {
        if (healthbarCanvas.activeSelf == true) healthbarCanvas.SetActive(false);
    }

    public override void Die()
    {
        Destroy(gameObject, 0.1f);
        OnSlimeBossDeath.Invoke();
        OnSlimeBossDeath.RemoveAllListeners();
    }

}
