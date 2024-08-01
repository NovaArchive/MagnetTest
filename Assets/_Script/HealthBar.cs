using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void OnHealthChanged(float currentHealth, float maxHealth)
    {
        text.text = $"{currentHealth} / {maxHealth}";
    }
}
