using UnityEngine;
using UnityEngine.Events;

namespace Script.Runtime.Signals
{
    public class CameraSignals : MonoBehaviour
    {
        #region Singleton

        public static CameraSignals Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        #endregion

        public UnityAction OnCameraLock = delegate { };
        public UnityAction OnCameraRelease = delegate { };
    }
}