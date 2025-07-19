using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Sway : MonoBehaviour
    {
        [SerializeField] private Transform m_WeaponSwayTransform;
        [SerializeField] private Movement m_PlayerMovement;
        [SerializeField] private PlayerCamera m_PlayerCamera;

        private float m_IdleSwayRandomX;
        private float m_IdleSwayRandomY;
        private float m_IdleSwayTimer = 0.0f;

        private WeaponSwayDS m_WeaponSwayDS = null;

        private void Update()
        {
            if (m_WeaponSwayDS == null) return;
            SwayWeapon(m_PlayerCamera.MouseDelta, Time.deltaTime);
        }

        public void SwitchWeapon(WeaponSwayDS data)
        {
            m_WeaponSwayDS = data;
        }

        public void SwayWeapon(Vector2 mouseInput, float delta)
        {
            if (!m_PlayerMovement.IsMoving)
            {
                float sway_random = Mathf.PerlinNoise(0.0f, 0.0f);
                sway_random = (sway_random - 0.5f) * 2;

                float sway_random_adjusted = sway_random * m_WeaponSwayDS._IdleSwayAdjustment;

                m_IdleSwayTimer += delta * (m_WeaponSwayDS._IdleSwaySpeed + sway_random);
                m_IdleSwayRandomX = Mathf.Sin(m_IdleSwayTimer * 1.5f + sway_random_adjusted) / m_WeaponSwayDS._IdleRandomSwayAmount;
                m_IdleSwayRandomY = Mathf.Sin(m_IdleSwayTimer - sway_random_adjusted) / m_WeaponSwayDS._IdleRandomSwayAmount;
            }
            else
            {
                m_IdleSwayRandomX = 0.0f;
                m_IdleSwayRandomY = 0.0f;
            }

            mouseInput = ClampVector2(mouseInput, m_WeaponSwayDS._SwayMin, m_WeaponSwayDS._SwayMax);

            float posX = Mathf.Lerp(m_WeaponSwayTransform.localPosition.x
                , m_WeaponSwayDS._Position.x - (mouseInput.x * m_WeaponSwayDS._SwayPositionAmount + m_IdleSwayRandomX) * delta, m_WeaponSwayDS._SwayPositionSpeed);
            float posY = Mathf.Lerp(m_WeaponSwayTransform.localPosition.y
                , m_WeaponSwayDS._Position.y - (mouseInput.y * m_WeaponSwayDS._SwayPositionAmount + m_IdleSwayRandomY) * delta, m_WeaponSwayDS._SwayPositionSpeed);
            m_WeaponSwayTransform.localPosition = new Vector3(posX, posY, m_WeaponSwayDS._Position.z);

            Quaternion currentRotation = m_WeaponSwayTransform.localRotation;
            Quaternion targetRotation = Quaternion.Euler(
                m_WeaponSwayDS._Rotation.x + (mouseInput.y * m_WeaponSwayDS._SwayRotationAmount + m_IdleSwayRandomX * m_WeaponSwayDS._IdleRandomSwayStrength) * delta,
                m_WeaponSwayDS._Rotation.y - (mouseInput.x * m_WeaponSwayDS._SwayRotationAmount + m_IdleSwayRandomY * m_WeaponSwayDS._IdleRandomSwayStrength) * delta,
                m_WeaponSwayDS._Rotation.z);

            m_WeaponSwayTransform.localRotation = Quaternion.Lerp(currentRotation, targetRotation, m_WeaponSwayDS._SwayRotationSpeed);

        }

        public Vector2 ClampVector2(Vector2 input, Vector2 min, Vector2 max)
        {
            return new Vector2(Mathf.Clamp(input.x, min.x, max.x), Mathf.Clamp(input.y, min.y, max.y));
        }
    }
}
