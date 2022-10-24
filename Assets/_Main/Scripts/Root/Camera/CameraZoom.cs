using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = System.Object;

namespace _Main.Scripts.youtubo.Camera
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private CinemachineFreeLook freeLook;
        [SerializeField] private float zoomSpeed = 2f;
        [SerializeField] private float zoomAcceleration = 2f;
        [SerializeField] private float zoomInnerRange = 3f;
        [SerializeField] private float zoomOutRange = 15f;

        private float currentMiddleRadius;
        private float newMiddleRadius;
        private InputAction mouseYAction;

        private void Start()
        {
            freeLook = GetComponent<CinemachineFreeLook>();
            mouseYAction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>().actions["MouseY"];
            currentMiddleRadius = freeLook.m_Orbits[1].m_Radius;
        }

        private void FixedUpdate()
        {
            AdjustCameraZoomIndex(mouseYAction.ReadValue<float>());
            UpdateZoomLevel();
        }

        private void UpdateZoomLevel()
        {
            if (currentMiddleRadius == newMiddleRadius) return;

            currentMiddleRadius = Mathf.Lerp(currentMiddleRadius, newMiddleRadius, zoomAcceleration * Time.deltaTime);
            currentMiddleRadius = Mathf.Clamp(currentMiddleRadius, zoomInnerRange, zoomOutRange);

            freeLook.m_Orbits[1].m_Radius = currentMiddleRadius;
            freeLook.m_Orbits[0].m_Radius = freeLook.m_Orbits[1].m_Radius * 0.75f;
            freeLook.m_Orbits[2].m_Radius = freeLook.m_Orbits[1].m_Radius * 0.5f;
        }

        private void AdjustCameraZoomIndex(float f)
        {
            if (f == 0)
            {
                return;
            }
            else if (f < 0)
            {
                newMiddleRadius = currentMiddleRadius + zoomSpeed;
            }
            else if (f > 0)
            {
                newMiddleRadius = currentMiddleRadius - zoomSpeed;
            }
        }
    }
}