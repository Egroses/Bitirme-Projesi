using System;
using System.Collections.Generic;
using Script.Runtime.Controller.Narrator;
using Script.Runtime.Data.UnityObjects;
using Script.Runtime.Signals;
using UnityEngine;

namespace Script.Runtime.Managers
{
    public class NarratorManager : MonoBehaviour
    {
        
        #region SelfVariables

        #region Serialized Variables

        [SerializeField] private NarratorController narratorController;
        
        #endregion
        #region PrivateVariables
        
        private List<string> _dialogueList = new List<string>();
        
        #endregion

        #endregion

        private void Awake()
        {
            GetPlayerData();
            SendDialogToController();
        }
        private void SendDialogToController()
        {
            narratorController.SetDialogueList(_dialogueList);
        }
        private void GetPlayerData()
        {
            _dialogueList = Resources.Load<CD_Dialog>("Data/CD_Dialog").dialogData.dialogList;
        }
        private void OnEnable()
        {
            SubscribeEvent();
        }
        private void SubscribeEvent()
        {
            NarratorSignals.Instance.OnNarratorTalk += narratorController.OnStartCoroutine; 
        }
        private void UnSubscribeEvent()
        {
            NarratorSignals.Instance.OnNarratorTalk -= narratorController.OnStartCoroutine; 
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }
    }
}