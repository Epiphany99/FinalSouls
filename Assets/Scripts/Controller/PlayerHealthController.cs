using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : Singleton<PlayerHealthController>
{
    public float MaxHealth = 100.0f;

    [SerializeField]
    private float currentHealth;

    // Use this for initialization
    void Start()
    {
        currentHealth = MaxHealth;
    }

    public void ReceiveDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void RegainHealth(float health)
    {
        currentHealth += health;
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
    }

    protected void Die()
    {
        Debug.Log(" Player DEAD");
        GameController.Instance.Quit();
    }
}
