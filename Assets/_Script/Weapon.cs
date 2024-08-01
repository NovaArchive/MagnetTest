using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private ParticleSystem fireEffect;
    [SerializeField] private float cooldown = 10f;

    private float _nextFireTime;

    public void StartFiring()
    {
        fireEffect.Play();
    }
    
    public void Fire()
    {
        if (Time.time < _nextFireTime) return;
        _nextFireTime = Time.time + cooldown;
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.Init(transform.forward);
    }

    public void StopFiring()
    {
        fireEffect.Stop();
    }
}
