using System.Threading.Tasks;
using Script.Runtime.Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Runtime.Signals
{
    public class TaskSignals : MonoBehaviour
    {
        
        #region Singleton

        public static TaskSignals Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

        }

        #endregion

        public UnityAction OnInjectionLocationStart = delegate{};
        public UnityAction OnInjectionRotationStart = delegate{};
        public UnityAction OnEducationOver = delegate{};
    }
}