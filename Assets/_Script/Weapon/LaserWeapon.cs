using System;
using System.Collections;
using System.Collections.Generic;
using Duy.Core;
using UnityEngine;
using UnityEngine.Serialization;

public class LaserWeapon : Weapon
{
    [Header("Visuals")]
    [SerializeField] private LineRenderer line;
    [SerializeField] private ParticleSystem fireFX;
    
    [Header("Settings")]
    [SerializeField] private float maxLength;
    [SerializeField] private int maxBounce = 3;
    [SerializeField] private int maxPenetration = 3;
    
    [Header("Firing Settings")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private float damage = 10;
    [SerializeField] private float cooldown = 0.5f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask enemyLayer;
    
    private int _currentCharacterCount;
    private float _nextTimeDamage;
    private bool _canDamage;

    private void Awake()
    {
        line.enabled = false;
    }
    
    public override void StartFiring()
    {
        fireFX.Play();
        line.enabled = true;
        _nextTimeDamage = 0;
    }

    public override void Fire()
    {
        if (Time.time > _nextTimeDamage)
        {
            _nextTimeDamage = Time.time + cooldown;
            _canDamage = true;
        }
        else
        {
            _canDamage = false;
        }
        
        _currentCharacterCount = 0;
        line.SetPosition(0, firePoint.position);
        line.positionCount = 1;
        ProjectLaser(1, new Ray(firePoint.position, firePoint.forward));
    }

    private void ProjectLaser(int index, Ray startingRay)
    {
        Ray ray = startingRay;
        
        //Loop to max bounce
        for (int bounce = index; bounce <= maxBounce; bounce++)
        {
            //Increment the amount of lines in renderer
            line.positionCount += 1;
            
            Vector3 endpoint; //End point of current line
            RaycastHit? lastHit = null; //Last object hit, can be null
            bool canBounce = false; //Check if the ray can be bounce
            
            //First we check if there is a wall in our path:
            if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit obstacleHit, maxLength, obstacleLayer))
            {
                //If there is, set last hit as the obstacle
                endpoint = obstacleHit.point;
                lastHit = obstacleHit;
                canBounce = true;
            }
            else
            {
                //Else, project until the end of the length
                endpoint = ray.origin + ray.direction * maxLength;
            }
        
            //Check if there is any enemy in between the current ray
            RaycastHit[] enemyHits = Physics.RaycastAll(
                ray.origin, ray.direction,
                Vector3.Distance(ray.origin, endpoint),
                enemyLayer);
        
            foreach (RaycastHit hit in enemyHits)
            {
                GameObject other = hit.collider.gameObject;
                _currentCharacterCount += 1;

                if (_canDamage)
                {
                    if (other.TryGetComponent(out IDamageable damageable))
                    {
                        damageable.Damage(damage);
                    }
                }
            
                //If the max amount of character hit is reached, set endpoint at the last enemy & end the current line
                if (_currentCharacterCount == maxPenetration)
                {
                    endpoint = hit.point;
                    lastHit = hit;
                    canBounce = false;
                    break;
                }
            }
        
            Debug.DrawLine(ray.origin, endpoint, Color.red);
            line.SetPosition(bounce, endpoint);
            
            //If there is a last hit, and can be bounced proceed to create a new ray. Else finish this ray and end the laser.
            if (lastHit != null && canBounce)
            {
                ray = new Ray(endpoint, Vector3.Reflect(ray.direction.normalized, lastHit.Value.normal));
            }
            else
            {
                return;
            }
        }
    }

    public override void StopFiring()
    {
        fireFX.Stop();
        line.enabled = false;
    }
}
