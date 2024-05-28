using System;
using System.Threading.Tasks;
using Cinemachine;
using Script.Runtime.Controller.CameraSc;
using Script.Runtime.Signals;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;
using Script.Runtime.Enum;

namespace Script.Runtime.Managers
{
    public class TaskManager : MonoBehaviour
    {
        public void OnInjectionLocationTaskStarted()
        {
            TaskSignals.Instance.OnInjectionLocationStart?.Invoke();
        }
        public void OnInjectionRotationTaskStarted()
        {
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.IntejtionRotationTask,0);
            TaskSignals.Instance.OnInjectionRotationStart?.Invoke();
        }
        public void OnEducationOver()
        {
            TaskSignals.Instance.OnEducationOver?.Invoke();
        }
        public void OnGameOver()
        {
            GameStateManager.Instance.SetPlayerCanInteract();
            GameStateManager.Instance.SetPlayerCanMove();
            CameraSignals.Instance.OnCameraRelease();
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.Welcome,0);
        }
    }
}