using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float visualizationRate = 0.1f;
    [SerializeField] private float fullLength = 1.5f;
    [SerializeField] private ParticleSystem hitEffect;
    
    [Header("Settings")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float lifetime = 1f;
    
    [Header("Hit Settings")]
    [SerializeField] private int maxPenetration = 3;
    [SerializeField] private int maxBounce = 3;
    // Start is called before the first frame update

    private int _currentPenetration;
    private int _currentBounce;
    private Vector3 _direction;
    
    private void Start()
    {
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }

    public void Init(Vector3 direction)
    {
        _direction = direction;
        transform.forward = direction;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += _direction * (speed * Time.deltaTime);
        ExtendLine();
    }

    private void ExtendLine()
    {
        Vector3 endpointPos = lineRenderer.GetPosition(1);
        if (Mathf.Abs(endpointPos.z) < fullLength)
        {
            lineRenderer.SetPosition(1, new Vector3(0, 0, endpointPos.z - speed * visualizationRate * Time.deltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject hitObject = other.gameObject;
        BulletHitRegister register = hitObject.TryGetComponent(out BulletHitRegister hit) ? hit : hitObject.AddComponent<BulletHitRegister>();
        
        if (register.CanBeHit(this))
        {
            _currentPenetration += 1;
            if (_currentPenetration == maxPenetration)
            {
                UseHitEffect(transform.position);
                Destroy(gameObject);
                return;
            }
            
            register.Register(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject hitObject = other.gameObject;

        if (!hitObject.TryGetComponent(out BulletHitRegister register)) return;
        register.Deregister(this);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_currentBounce == maxBounce)
        {
            UseHitEffect(transform.position);
            Destroy(gameObject);
            return;
        }

        ContactPoint contactPoint = other.GetContact(0);
        Vector3 newVelocity = Vector3.Reflect(_direction.normalized, contactPoint.normal);
        
        _direction = newVelocity;
        transform.forward = newVelocity;
        _currentBounce += 1;
        
        UseHitEffect(contactPoint.point);
    }

    private void UseHitEffect(Vector3 position)
    {
        ParticleSystem hitFX = Instantiate(hitEffect, position, Quaternion.identity);
        hitFX.Play();
    }
}
