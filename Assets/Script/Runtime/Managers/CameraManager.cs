using System;
using Script.Runtime.Controller.CameraSc;
using Script.Runtime.Signals;
using TMPro.Examples;
using UnityEngine;

namespace Script.Runtime.Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Variables

        #region Serialize Variables

        [SerializeField] private MyCameraController myCameraController;

        #endregion

        #endregion
        
        private void Start()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            CameraSignals.Instance.OnCameraLock += myCameraController.LockCamera;
            CameraSignals.Instance.OnCameraRelease += myCameraController.ReleaseCamera;
        }

        private void UnSubscribeEvent()
        {
            CameraSignals.Instance.OnCameraLock -= myCameraController.LockCamera;
            CameraSignals.Instance.OnCameraRelease -= myCameraController.ReleaseCamera;
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }
    }
}