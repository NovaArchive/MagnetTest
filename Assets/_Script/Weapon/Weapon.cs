using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public abstract void StartFiring();
    public abstract void Fire();
    public abstract void StopFiring();
}
