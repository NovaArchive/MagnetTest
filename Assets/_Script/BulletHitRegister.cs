using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitRegister : MonoBehaviour
{
    public List<Bullet> damageSources = new();

    public bool CanBeHit(Bullet bullet)
    {
        //True if damageSource does not contain the bullet
        return !damageSources.Contains(bullet);
    }

    public void Register(Bullet bullet)
    {
        if (damageSources.Contains(bullet)) return;
        
        Debug.Log("Register " + bullet.name);
        damageSources.Add(bullet);
    }

    public void Deregister(Bullet bullet)
    {
        if (!damageSources.Contains(bullet)) return;

        Debug.Log("Deregister " + bullet.name);
        damageSources.Remove(bullet);
    }
}
