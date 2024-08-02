using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private ParticleSystem fireEffect;
    [SerializeField] private float cooldown = 10f;

    private float _nextFireTime;

    public override void StartFiring()
    {
        fireEffect.Play();
    }
    
    public override void Fire()
    {
        if (Time.time < _nextFireTime) return;
        _nextFireTime = Time.time + cooldown;
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.Init(transform.forward);
    }

    public override void StopFiring()
    {
        fireEffect.Stop();
    }
}
