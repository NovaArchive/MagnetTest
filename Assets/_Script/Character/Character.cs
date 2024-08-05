using System;
using System.Collections;
using System.Collections.Generic;
using Duy.Core;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour, IDamageable
{
    public float maxHealth;

    private float _currentHealth;
    public UnityEvent<float, float> OnHealthChanged;
    public UnityEvent<float> OnDamage;
    public UnityEvent OnDeath;

    public float CurrentHealth
    {
        get => _currentHealth;
        private set
        {
            _currentHealth = value;
            OnHealthChanged.Invoke(_currentHealth, maxHealth);
        }
    }

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void Damage(float amount)
    {
        CurrentHealth -= amount;
        
        if (CurrentHealth <= 0)
        {
            OnDeath.Invoke();
            return;
        }
        
        OnDamage.Invoke(amount);
    }
}
