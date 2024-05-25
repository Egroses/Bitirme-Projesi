using Script.Runtime.Controller;
using Script.Runtime.Controller.Player;
using Script.Runtime.Data.UnityObjects;
using Script.Runtime.Data.ValueObjects;
using Script.Runtime.Signals;
using UnityEngine;

namespace Script.Runtime.Managers
{
    public class PlayerManager: MonoBehaviour
    {
        #region SelfVariables

        #region Serialized Variables

        [SerializeField] private PlayerMovementController movementController;
        #endregion
        #region PrivateVariables

        private PlayerData _data;

        #endregion

        #endregion

        private void Awake()
        {
            GetPlayerData();
            SendDataToController();
        }
        
        private void GetPlayerData()
        {
            _data = Resources.Load<CD_Player>("Data/CD_Player").Data;
        }

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            InputSignals.Instance.OnInputTaken += movementController.OnInputTaken;
            //PlayerSignals.Instance.OnPlayerCanMove += OnPlayerCanMove;
        }

        private void OnPlayerCanMove()
        {
            //movementcontroller boolan
            
        }
        private void UnSubscribeEvent()
        {
            InputSignals.Instance.OnInputTaken += movementController.OnInputTaken;
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        private void SendDataToController()
        {
            movementController.SetMovementData(_data.movementData);
        }
    }
}