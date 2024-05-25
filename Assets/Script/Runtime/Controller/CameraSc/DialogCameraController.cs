using System;
using Cinemachine;
using DG.Tweening;
using PixelCrushers.DialogueSystem.Wrappers;
using Script.Runtime.Managers;
using Script.Runtime.Signals;
using UnityEngine;

namespace Script.Runtime.Controller.CameraSc
{
    public class DialogCameraController : MonoBehaviour
    {
        
        #region Variables

        #region Serialize Variables
            
        [SerializeField] private CinemachineVirtualCamera dialogCamera;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform playerMoveTransform;
        [SerializeField] private Usable usableNurse;

        #endregion

        #region private Variables
            
        private bool _taskStarted;
        private Camera _mainCamera;
                
        #endregion

        #endregion

        private void OnEnable()
        {
            Subscription();
        }

        private void Subscription()
        {
            TaskSignals.Instance.OnEducationOver += OnDialogFinished;
        }

        public void OnDialogStarted()
        {
            dialogCamera.gameObject.SetActive(true);
            GameStateManager.Instance.SetPlayerCantInteract();
            GameStateManager.Instance.SetPlayerCantMove();
            playerTransform.DOMove(playerMoveTransform.position, 2f);
            playerTransform.DORotateQuaternion(playerMoveTransform.rotation, 2f);
            CameraSignals.Instance.OnCameraRelease();
        }
        
        public void OnDialogFinished()
        {
            dialogCamera.gameObject.SetActive(false);
            usableNurse.enabled = false;
            GameStateManager.Instance.SetPlayerCanInteract();
            GameStateManager.Instance.SetPlayerCanMove();
            CameraSignals.Instance.OnCameraLock();
        }

        private void OnDisable()
        {
            UnSubscription();
        }

        private void UnSubscription()
        {
            TaskSignals.Instance.OnEducationOver -= OnDialogFinished;
        }
    }
}