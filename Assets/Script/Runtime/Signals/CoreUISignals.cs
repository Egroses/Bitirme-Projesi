using Script.Runtime.Enum;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Runtime.Signals
{
    public class CoreUISignals : MonoBehaviour
    {
        #region Singleton

        public static CoreUISignals Instance { get; private set; }
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

        public UnityAction<UIPanelTypes, int> OnOpenPanel = delegate { };
        public UnityAction<int> OnClosePanel = delegate { };
        public UnityAction OnCloseAllPanels = delegate { };
    }
}