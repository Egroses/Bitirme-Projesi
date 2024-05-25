using System.Collections.Generic;
using Script.Runtime.Enum;
using Script.Runtime.Signals;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

namespace Script.Runtime.Controller.Navmesh
{
    public class NavmeshAgentMovementController : MonoBehaviour
    {
        #region Variables

        #region SerializeVariables
        
        [SerializeField] private Transform[] waypoints;  
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float playerDistance;
        [SerializeField] private float rotationSpeed = 10f;
        
        #endregion

        #region Private Variables

        private int _currentWaypointIndex = 0;
        private NavMeshAgent _agent;
        private NavmeshAgentAnimationController _animation;
        private Quaternion _originalRotation;
        private bool _isArrivedDestination = false;
        private bool _isAgentStart = false;
        private Transform _agentTransform;
        
        #endregion

        #endregion
        

        void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            if (TryGetComponent<NavMeshAgent>(out var navmeshComponent))
            {
                _agent = navmeshComponent;
            }
            else
            {
                Debug.LogError("NavMeshAgent Component not found");
            }
            if (TryGetComponent<NavmeshAgentAnimationController>(out var navmeshAgentAnimation))
            {
                _animation = navmeshAgentAnimation;
            }
            else
            {
                Debug.LogError("NavmeshAgentAnimationController Component not found");
            }

            if (waypoints.Length == 0)
            {
                Debug.LogError("Navmesh Points not set");
            }
            
            _agentTransform = transform;
        }

        void Update()
        {
            if (waypoints.Length > 0 && !_isArrivedDestination)
            {
                if (Vector3.Distance(playerTransform.position, transform.position) < playerDistance)
                {
                    if (_agent.isStopped)
                    {
                        LookAtForward();
                    }

                    if (Quaternion.Angle(transform.rotation, _originalRotation) < 10f || !_agent.isStopped)
                    {
                        if (_agent.isStopped)
                        {
                            _agent.isStopped = false;
                            MoveAgent();
                        }
                    
                        if (!_agent.pathPending && _agent.remainingDistance < 1f)
                        {
                            _currentWaypointIndex = _currentWaypointIndex + 1;

                            if (_currentWaypointIndex == waypoints.Length)
                            {
                                ArrivedDestination();
                                return;
                            }
                            MoveAgent();
                        }
                    }
                } 
                else
                {
                    LookAtThePlayer();
                    StopAgent();
                }
            }
        }

        private void ArrivedDestination()
        {
            _isArrivedDestination = true;
            StopAgent();
            _agentTransform.position = waypoints[waypoints.Length-1].position;
            _agentTransform.rotation = waypoints[waypoints.Length - 1].rotation;
            transform.gameObject.layer = 6;
            CoreUISignals.Instance.OnClosePanel?.Invoke(0);
        }

        private void StopAgent()
        {
            _agent.isStopped = true;
            _agent.velocity = Vector3.zero;
            _animation.OnWalkingFalse();
            _animation.OnStandTrue();
        }

        private void MoveAgent()
        {
            _agent.SetDestination(waypoints[_currentWaypointIndex].position);

            if (!_isAgentStart)
            {
                _isAgentStart = true;
                _animation.OnWavingFalse();
                CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.Story,0);
            }
            _animation.OnStandFalse();
            _animation.OnWalkingTrue();
            NarratorSignals.Instance.OnNarratorTalk?.Invoke();
        }

        private void LookAtForward()
        {
            _agentTransform.rotation = Quaternion.Lerp(transform.rotation, _originalRotation, Time.deltaTime * rotationSpeed );
        }

        private void LookAtThePlayer()
        {
            if (!_agent.isStopped)
            {
                _originalRotation = transform.rotation;
            }
            Vector3 direction = playerTransform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _agentTransform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed );
        }
    }
}