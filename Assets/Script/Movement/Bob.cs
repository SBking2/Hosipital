using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace Game
{
    /*public class Bob : MonoBehaviour
    {
        [SerializeField] private Movement m_Movement;

        [SerializeField] private Transform m_CameraBobTransform;
        [SerializeField] private Transform m_CameraPivotTransform;

        [SerializeField] private Transform m_WeaponBobTransform;

        [Header("============== Camera Bob ====================")]

        [SerializeField, Range(0, 0.5F)] private float m_WalkAmplitude = 0.018f;
        [SerializeField, Range(0, 30)] private float m_WalkFrequency = 10;

        [SerializeField, Range(0, 0.5f)] private float m_SprintAmplitude = 0.03f;
        [SerializeField, Range(0, 30)] private float m_SprintFrequency = 15;

        [SerializeField, Range(0, 0.5f)] private float m_CrouchAmplitude = 0.01f;
        [SerializeField, Range(0, 30)] private float m_CrouchFrequency = 8;

        private bool m_CameraBobStarted = false;
        private bool m_WeaponBobStarted = false;

        private Vector3 m_WeaponCurrentPosition;

        private WeaponBobDS m_WeaponBobDS = new WeaponBobDS();

        private void OnEnable()
        {
            m_Movement._OnStateSwitchEvent += CameraBobStart;
            m_Movement._OnStateSwitchEvent += WeaponBobStart;
        }

        private void OnDisable()
        {
            m_Movement._OnStateSwitchEvent -= CameraBobStart;
            m_Movement._OnStateSwitchEvent -= WeaponBobStart;
        }

        public void SwitchWeapon(WeaponBobDS data)
        {
            m_WeaponBobDS = data;
        }

        private void Update()
        {
            ResetCameraBobPAR();
            ResetWeaponPos();

            m_WeaponBobTransform.localPosition = m_WeaponCurrentPosition * 0.1f;

            if (m_Movement.IsMoving && m_Movement.IsGround)
            {
                CameraBobStep();
                WeaponBobStep();
                FocusTarget();
            }
            else
            {
                m_CameraBobStarted = false;
                m_WeaponBobStarted = false;
            }
                
        }
        private void ResetCameraBobPAR()
        {
            m_CameraBobTransform.localPosition = Vector3.Lerp(m_CameraBobTransform.localPosition, Vector3.zero, m_Movement.Deceleration * Time.deltaTime);
            m_CameraBobTransform.localRotation = Quaternion.Lerp(m_CameraBobTransform.localRotation, Quaternion.identity, m_Movement.Deceleration * Time.deltaTime);
        }

        private void CameraBobStep()
        {
            float _amplitude = 0.0f;
            float _frequency = 0.0f;

            if (m_Movement.IsCrouching)
            {
                _amplitude = m_CrouchAmplitude;
                _frequency = m_CrouchFrequency;
            }
            else
            {
                if (m_Movement.CurrentMoveState == Movement.MoveState.Walk)
                {
                    _amplitude = m_WalkAmplitude;
                    _frequency = m_WalkFrequency;
                }
                else if(m_Movement.CurrentMoveState == Movement.MoveState.Sprint)
                {
                    _amplitude = m_SprintAmplitude;
                    _frequency = m_SprintFrequency;
                }
            }

            Vector3 pos = Vector3.zero;
            pos.y += Mathf.Sin(Time.time * _frequency) * _amplitude;
            pos.x += Mathf.Cos(Time.time * _frequency / 2) * _amplitude * 2;

            if (m_CameraBobStarted)
                m_CameraBobTransform.localPosition = pos;
            else
            {
                m_CameraBobTransform.localPosition = Vector3.Lerp(
                    m_CameraBobTransform.localPosition, pos, m_Movement.Acceleration * Time.deltaTime);

                if ((m_CameraBobTransform.localPosition - pos).magnitude <= 0.01f)
                    m_CameraBobStarted = true;
            }
        }
        private void FocusTarget()
        {
            Vector3 pos = new Vector3(m_CameraBobTransform.position.x
                , m_CameraPivotTransform.position.y
                , m_CameraPivotTransform.position.z);

            pos += m_CameraPivotTransform.forward * 15.0f;

            m_CameraBobTransform.LookAt(pos);
        }

        private void ResetWeaponPos()
        {
            m_WeaponCurrentPosition = Vector3.Lerp(m_WeaponCurrentPosition, Vector3.zero, m_WeaponBobDS._WeaponBobEndSpeed * Time.deltaTime);
        }
        private void WeaponBobStep()
        {
            float x = Mathf.Sin(Time.time * m_WeaponBobDS._WeaponBobSpeed) * m_WeaponBobDS._WeaponHoriMuti;
            float y = Mathf.Abs(Mathf.Cos(Time.time * m_WeaponBobDS._WeaponBobSpeed)) * m_WeaponBobDS._WeaponVertiMuti;

            Vector3 pos = new Vector3(x, y, 0.0f);

            if (m_WeaponBobStarted)
                m_WeaponCurrentPosition = pos;
            else
            {
                m_WeaponCurrentPosition = Vector3.Lerp(
                    m_WeaponCurrentPosition, pos, m_WeaponBobDS._WeaponBobStartSpeed * Time.deltaTime);

                if (Vector3.Distance(m_WeaponCurrentPosition, pos) <= m_WeaponBobDS._WeaponBobStartClamp)
                    m_WeaponBobStarted = true;
            }
        }

        public void CameraBobStart() { m_CameraBobStarted = false; }
        public void WeaponBobStart() { m_WeaponBobStarted = false; }
    }*/
}
