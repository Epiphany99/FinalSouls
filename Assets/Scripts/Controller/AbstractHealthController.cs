using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractHealthController : MonoBehaviour
{
    public float MaxHealth = 100.0f;
    [SerializeField]
    protected float currentHealth;


    public abstract void ReceiveDamage(float damage);
    public abstract void RegainHealth(float health);
    protected abstract void Die();
}
