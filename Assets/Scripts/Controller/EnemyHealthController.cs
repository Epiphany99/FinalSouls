using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : AbstractHealthController
{

    // Use this for initialization
    void Start ()
    {
        currentHealth = MaxHealth;
	}

    public override void ReceiveDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public override void RegainHealth(float health)
    {
        currentHealth += health;
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
    }
}
