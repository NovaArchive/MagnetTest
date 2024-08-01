using System.Collections;
using System.Collections.Generic;
using Duy.Utility;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Weapon weapon;
    private Camera _camera;

    // Update is called once per frame
    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.StartFiring();
        }
        
        if (Input.GetMouseButton(0))
        {
            weapon.Fire();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            weapon.StopFiring();
        }
        
        ApplyMovement(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        ApplyRotation();
    }

    private void ApplyMovement(float xDirection, float zDirection)
    {
        Vector3 direction = new (xDirection, 0, zDirection);
        transform.position += direction * (speed * Time.deltaTime);
    }

    private void ApplyRotation()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }
}
