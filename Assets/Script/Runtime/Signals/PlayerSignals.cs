using System;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Runtime.Signals
{
    public class PlayerSignals : MonoBehaviour
    {
        #region Singleton

        public static PlayerSignals Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        #endregion

        public UnityAction OnPlayerCanMove = delegate { };
    }
}