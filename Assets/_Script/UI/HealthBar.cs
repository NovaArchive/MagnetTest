using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TMP_Text healthBarText;
    [SerializeField] private TMP_Text damageIndicatorTextObject;
    [SerializeField] private float damageIndicatorDuration = 0.2f;

    public void OnHealthChanged(float currentHealth, float maxHealth)
    {
        healthBarText.text = $"{currentHealth} / {maxHealth}";
    }

    public void OnTakeDamage(float damage)
    {
        TMP_Text textObject = Instantiate(damageIndicatorTextObject, transform);
        textObject.text = $"- {damage}";

        textObject.gameObject.GetComponent<RectTransform>().DOAnchorPosY(50, damageIndicatorDuration)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => Destroy(textObject.gameObject));;
    }
}
