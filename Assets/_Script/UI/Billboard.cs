using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Duy.Utility
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField] private BillboardType billboardType;

        private Camera _camera;

        private enum BillboardType
        {
            LookAtCamera,
            CameraForward
        }

        private void Awake()
        {
            _camera = Camera.main;
        }

        void LateUpdate()
        {
            switch (billboardType)
            {
                case BillboardType.LookAtCamera:
                    transform.LookAt(_camera.transform.position, Vector3.up);
                    break;
                case BillboardType.CameraForward:
                    transform.forward = _camera.transform.forward;
                    break;
                default:
                    break;
            }
        }
    }
}
