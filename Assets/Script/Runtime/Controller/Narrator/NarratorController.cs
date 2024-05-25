using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Script.Runtime.Controller.Narrator
{
    public class NarratorController : MonoBehaviour
    {
        #region Variable

        #region Serialize Variables
        
        [SerializeField] private TextMeshProUGUI storyPanelText = null;
        
        #endregion

        #region Private Variables
        
        private List<string> _dialogueList = null;
        private int _currentDialogueIndex = 0;
        private string _currentDialogueText = "";
        private Coroutine _tempCoroutine = null;
        
        #endregion

        #endregion

        internal void SetDialogueList(List<string> dialogueTextList)
        {
            _dialogueList = dialogueTextList;
        }
        internal void OnStartCoroutine()
        {
            if (_tempCoroutine == null && _currentDialogueIndex<_dialogueList.Count)
            {
                storyPanelText.text = "";
                _currentDialogueText = "";
                _tempCoroutine = StartCoroutine(OnAgentTalk());
            }
        }
        
        private IEnumerator OnAgentTalk()
        {
            yield return null;

            while (_currentDialogueText.Length < _dialogueList[_currentDialogueIndex].Length)
            {
                _currentDialogueText += _dialogueList[_currentDialogueIndex][_currentDialogueText.Length];
                storyPanelText.text = _currentDialogueText;
                yield return new WaitForFixedUpdate();
            }

            OnStopCoroutine();
        }

        private void OnStopCoroutine()
        {
            _currentDialogueIndex++;
            StopCoroutine(_tempCoroutine);
            _tempCoroutine = null;
        }
    }
}