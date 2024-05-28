using Script.Runtime.Enum;
using Script.Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Runtime.Controller.UI
{
    public class WelcomePanelController : MonoBehaviour
    {
        #region Variables

        #region Serialize Variables
        
        [SerializeField] private GameObject sceneContent;
        [SerializeField] private Transform sceneTransform;
        
        #endregion

        #region Private Variables

        private GameObject _objectHolder;

        #endregion

        #endregion
        
        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            if (GameObject.FindWithTag("SceneTrasform") != null)
            {
                sceneTransform = GameObject.FindWithTag("SceneTrasform").transform;
            }
            else
            {
                Debug.LogError("Scene Transform Not Found");
            }
        }
        private void OnEnable()
        {
            OnRemoveGame();
        }

        [Button("Start Game")]
        public void OnStartGame()
        {
            _objectHolder = Instantiate(Resources.Load<GameObject>($"Environment/{sceneContent.name}"),sceneTransform);
            CameraSignals.Instance.OnCameraLock?.Invoke();
            CoreUISignals.Instance.OnClosePanel?.Invoke(0);
        }
        
        [Button("Remove Game")]
        public void OnRemoveGame()
        {
            

            if(sceneTransform.childCount>0)
            {
                _objectHolder = sceneTransform.GetChild(0).gameObject;
#if UNITY_EDITOR
                DestroyImmediate(_objectHolder);
#else
                Destroy(_objectHolder);
#endif
            }
        }
    }
}