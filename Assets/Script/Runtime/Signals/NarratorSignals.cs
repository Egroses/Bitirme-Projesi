using UnityEngine;
using UnityEngine.Events;

namespace Script.Runtime.Signals
{
    public class NarratorSignals : MonoBehaviour
    {
        #region Singleton

        public static NarratorSignals Instance { get; private set; }
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

        public UnityAction OnNarratorTalk = delegate { };
        
    }
}