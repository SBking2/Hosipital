using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Movement : MonoBehaviour
    {
        public Action _OnStateSwitchEvent;

        #region Para
        [Header("------------ Movement Para --------------")]
        public float Acceleration;
        public float Deceleration;

        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_SprintSpeed;
        [SerializeField] private float m_CrouchSpeed;
        private float m_CurrentSpeed;

        [Header("------------ Gound About ----------------")]
        [SerializeField] private float m_Gravity;
        public LayerMask groundLayer;

        [Header("============== Crouch Para =======================")]
        [SerializeField] private float m_OriginalHeight;
        [SerializeField] private float m_CrouchHeight;
        private float m_OriginalCenter;
        private float m_CrouchCenter;

        [SerializeField] private float m_CrouchDownSpeed;
        [SerializeField] private float m_StandUpSpeed;
        #endregion

        #region Player State
        public bool IsMoving { get; private set; }
        public bool IsGround { get; private set; }
        public bool IsWalk { get; private set; }
        public bool IsSprint { get; private set; }
        public bool IsCrouching { get; private set; }
        #endregion

        #region Invisiable Property
        private Vector2 m_TargetDirect;
        #endregion

        #region Component
        private Transform m_CameraPivotTransform;
        private CharacterController m_CharacterController;
        private SphereCollider m_GroundCheckCollider;
        private Vector3 m_GroundCheckColliderStartPos;
        private Vector3 m_GroundCheckColliderCrouchPos;
        #endregion

        #region Move Value
        private Vector3 m_TargetVelocity;
        private Vector3 m_MoveVelocity;
        private float m_YVelocity;
        #endregion

        private void Awake()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_CameraPivotTransform = this.transform.Find("CameraPivot").transform;
            m_GroundCheckCollider = this.transform.Find("GroundCheck").GetComponent<SphereCollider>();
        }

        private void Start()
        {
            m_OriginalCenter = 0.0f;
            m_CrouchCenter = (m_OriginalHeight - m_CrouchHeight) / 2;

            //确定GroudCheck初始位置
            m_GroundCheckColliderStartPos = m_GroundCheckCollider.transform.localPosition;

            //确定蹲下后GroundCheck的位置
            m_GroundCheckColliderCrouchPos = m_GroundCheckColliderStartPos;
            m_GroundCheckColliderCrouchPos.y += (m_OriginalHeight - m_CrouchHeight);
        }

        private void Update()
        {
            float delta = Time.deltaTime;

            m_CurrentSpeed = m_WalkSpeed;

            GroundCheck();

            HandleMovement(delta);
            CrouchHandler(delta);

            m_CharacterController.Move(m_MoveVelocity * delta);
            m_CharacterController.Move(new Vector3(0.0f, m_YVelocity, 0.0f) * delta);

        }

        private void HandleMovement(float delta)
        {
            m_TargetVelocity = m_CameraPivotTransform.rotation * new Vector3(m_TargetDirect.x, 0.0f, m_TargetDirect.y);
            m_TargetVelocity.y = 0.0f;

            if (m_TargetDirect != Vector2.zero)
            {
                IsMoving = true;
                IsWalk = true;
            }
            else
            {
                IsMoving = false;
                IsWalk = false;
            }

            m_MoveVelocity = Vector3.Lerp(m_MoveVelocity, m_TargetVelocity * m_CurrentSpeed, delta * Acceleration);

            if(!IsGround)
            {
                m_YVelocity += m_Gravity * delta;
            }
        }

        private void GroundCheck()
        {
            Collider[] colliders = Physics.OverlapSphere(
            m_GroundCheckCollider.center + m_GroundCheckCollider.transform.position
            , m_GroundCheckCollider.radius, groundLayer);

            if (colliders.Length > 0)
                IsGround = true;
            else
                IsGround = false;
        }
        private void CrouchHandler(float delta)
        {
            if(IsCrouching)
            {
                m_CharacterController.height = Mathf.Lerp(m_CharacterController.height
                    , m_CrouchHeight, m_CrouchDownSpeed * delta);

                float centerY = m_CharacterController.center.y;
                centerY = Mathf.Lerp(centerY, m_CrouchCenter, m_CrouchDownSpeed * delta);
                m_CharacterController.center = new Vector3(0.0f, centerY, 0.0f);

                //处理GroundCheck的位置
                m_GroundCheckCollider.transform.localPosition
                    = Vector3.Lerp(m_GroundCheckCollider.transform.localPosition, m_GroundCheckColliderCrouchPos
                    , m_CrouchDownSpeed * delta);
            }
            else
            {
                m_CharacterController.enabled = false;

                m_CharacterController.height = Mathf.Lerp(m_CharacterController.height
                    , m_OriginalHeight, m_StandUpSpeed * delta);

                float centerY = m_CharacterController.center.y;
                centerY = Mathf.Lerp(centerY, m_OriginalCenter, m_StandUpSpeed * delta);
                m_CharacterController.center = new Vector3(0.0f, centerY, 0.0f);
                
                if(m_OriginalHeight - m_CharacterController.height > 0.01f)
                    transform.position += new Vector3(0.0f, 0.010f, 0.0f);

                m_CharacterController.enabled = true;

                //处理GroundCheck的位置
                m_GroundCheckCollider.transform.localPosition
                    = Vector3.Lerp(m_GroundCheckCollider.transform.localPosition, m_GroundCheckColliderStartPos
                    , m_StandUpSpeed * delta);
            }
        }
        public void SetInputMoveDirect(Vector2 direct) { m_TargetDirect = direct; }
    }

}