using Cinemachine;
using Script.Runtime.Data.ValueObjects;
using Script.Runtime.Keys;
using Script.Runtime.Managers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Runtime.Controller.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region SelfVariables

        #region Serialized Variables
        [Header("Player Camera")] 
        [SerializeField] private CinemachineFreeLook _playerCamera;
        
        [Header("Player Step Climb")] 
        [SerializeField] private Transform upperStep;
        [SerializeField] private Transform lowerStep;
        [SerializeField] private float stepSmooth=.2f;
        
        [Header("Player Movement Buttons")]
        [SerializeField] KeyCode forwardKey = KeyCode.W;
        [SerializeField] KeyCode backwardKey = KeyCode.S;
        [SerializeField] KeyCode leftKey = KeyCode.A;
        [SerializeField] KeyCode rightKey = KeyCode.D;
        [SerializeField] KeyCode runKey = KeyCode.LeftShift;
        #endregion
        #region Private Variables

        private PlayerManager _manager;
        private PlayerAnimationController _animation;
        private Rigidbody _rigidbody;
        private float2 _inputValues;
        private PlayerMovementData _data;
        private Quaternion _targetRotation;
        private float _speed;
        
        #endregion


        #endregion

        private void Awake()
        {
            GetReferences();
        }
        private void GetReferences()
        {
            _animation = GetComponent<PlayerAnimationController>();
            _manager = GetComponent<PlayerManager>();
            _rigidbody = GetComponent<Rigidbody>();
            _speed = 1f;
        }
        public void SetMovementData(PlayerMovementData movementData)
        {
            _data = movementData;
        }
        internal void OnInputTaken(InputParams inputParams)
        {
            _inputValues = inputParams.InputValues;
        }

        private void FixedUpdate()
        {
            if(GameStateManager.Instance.playerCanMove)
            {
                if (Input.GetKey(forwardKey) || Input.GetKey(backwardKey) || Input.GetKey(leftKey) ||
                    Input.GetKey(rightKey))
                {
                    MovePlayer();
                    _animation.OnWalkingTrue();

                    if (Input.GetKey(runKey))
                    {
                        _animation.OnRunningTrue();
                        _speed = _data.runSpeed;
                    }
                }
                else
                {
                    isPlayerOnGround();
                    _animation.OnWalkingFalse();
                    _animation.OnRunningFalse();
                    _speed = 1f;
                }

                if (!Input.GetKey(runKey))
                {
                    _animation.OnRunningFalse();
                    _speed = 1f;
                }

                StepClimb();
            }
            else
            {
                _animation.OnWalkingFalse();
                _animation.OnRunningFalse();
            }
        }

        private void isPlayerOnGround()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out var hit, .1f))
            {
                if (hit.collider.CompareTag("Ground") && _rigidbody.velocity.normalized.magnitude>0)
                {
                    StopPlayer();
                }
            }
        }

        private void MovePlayer()
        {
            var movementDirection =Quaternion.AngleAxis(_playerCamera.m_XAxis.Value,Vector3.up)* new Vector3(_inputValues.x, 0, _inputValues.y);
            movementDirection.Normalize();
            
            if (movementDirection != Vector3.zero) {
                _targetRotation = Quaternion.LookRotation(movementDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, _data.RotationSpeed * Time.deltaTime);
            }
            
            var targetVelocity = _targetRotation * Vector3.forward * (_data.walkSpeed * Time.fixedDeltaTime * _speed);

            float maxSpeed = Input.GetKey(runKey) ? _data.runMaxSpeed : _data.walkMaxSpeed;

            Vector3 newVelocity = Vector3.ClampMagnitude(_rigidbody.velocity + targetVelocity, maxSpeed);

            _rigidbody.velocity = new Vector3(newVelocity.x, _rigidbody.velocity.y, newVelocity.z);
            
        }

        private void StepClimb()
        {
            // Adımların yönlendirme vektörleri
            Vector3[] stepDirections = { Vector3.forward, new Vector3(1.5f, 0, 1), new Vector3(-1.5f, 0, 1) };

            // Her bir yönlük için kontrol yap
            foreach (Vector3 direction in stepDirections)
            {
                RaycastHit hitLowerStep, hitUpperStep;
                // Alt adım raycast'i
                if (Physics.Raycast(lowerStep.position, transform.TransformDirection(direction), out hitLowerStep, 0.1f))
                {
                    // Üst adım raycast'i
                    if (!Physics.Raycast(upperStep.position, transform.TransformDirection(direction), out hitUpperStep, 0.1f))
                    {
                        // Engel yoksa karakterin yüksekliğini düşür
                        _rigidbody.position -= new Vector3(0, -stepSmooth, 0);
                    }
                }
            }
        }

        
        private void StopPlayer()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
        
        
        
    }
}