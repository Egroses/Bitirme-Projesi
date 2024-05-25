using System;
using System.Security.Cryptography;
using Script.Runtime.Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Runtime.Signals
{
    public class InputSignals : MonoBehaviour
    {
        #region Singleton

        public static InputSignals Instance;

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

        public UnityAction<InputParams> OnInputTaken =delegate{};
        
    }
}