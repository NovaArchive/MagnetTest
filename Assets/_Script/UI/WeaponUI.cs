using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    
    public void OnWeaponChange(Weapon weapon)
    {
        text.text = weapon.weaponName;
    }
}
