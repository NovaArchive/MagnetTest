using System;
using System.Collections;
using System.Collections.Generic;
using Duy.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private List<Weapon> weaponList;
    
    private Camera _camera;
    
    private int _currentIndex;
    private Weapon _currentWeapon;

    public UnityEvent<Weapon> OnWeaponChange;

    // Update is called once per frame
    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        EquipWeapon(weaponList[0]);
        _currentIndex = 0;
        GameManager.Instance.onPhaseChanged.AddListener(OnPhaseChanged);
    }

    private void EquipWeapon(Weapon weapon)
    {
        if (_currentWeapon) _currentWeapon.gameObject.SetActive(false);
        _currentWeapon = weapon;
        _currentWeapon.gameObject.SetActive(true);
        OnWeaponChange.Invoke(weapon);
    }

    private void OnPhaseChanged(GameManager.GamePhase phase)
    {
        if (phase != GameManager.GamePhase.InGame)
        {
            _currentWeapon.StopFiring();
        }
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentPhase != GameManager.GamePhase.InGame) return;
        if (PauseManager.Instance.GameIsPaused) return;

        if (Input.GetMouseButtonDown(0))
        {
            _currentWeapon.StartFiring();
        }
        
        if (Input.GetMouseButton(0))
        {
            _currentWeapon.Fire();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            _currentWeapon.StopFiring();
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (_currentIndex == 0)
            {
                _currentIndex = weaponList.Count - 1;
            }
            else
            {
                _currentIndex -= 1;
            }
            
            EquipWeapon(weaponList[_currentIndex]);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (_currentIndex == weaponList.Count - 1)
            {
                _currentIndex = 0;
            }
            else
            {
                _currentIndex += 1;
            }
            
            EquipWeapon(weaponList[_currentIndex]);
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
