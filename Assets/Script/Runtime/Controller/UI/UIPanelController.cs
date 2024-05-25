using System.Collections.Generic;
using Script.Runtime.Enum;
using Script.Runtime.Signals;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Script.Runtime.Controller.UI
{
    public class UIPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
    
        [SerializeField] private List<Transform> layers = new List<Transform>();

        #endregion

        #endregion

        private void Start()
        {
            Subscription();
        }

        private void Subscription()
        {
            CoreUISignals.Instance.OnOpenPanel+=OnOpenPanel; 
            CoreUISignals.Instance.OnClosePanel+=OnClosePanel;
            CoreUISignals.Instance.OnCloseAllPanels+=OnCloseAllPanels;
        }
        
        [Button("Open Panel")]
        private void OnOpenPanel(UIPanelTypes panelType, int value)
        {
            OnClosePanel(value);
            Instantiate(Resources.Load<GameObject>($"Screens/{panelType.ToString()}Panel"), layers[value]);
        }
        
        [Button("Close Panel")]
        private void OnClosePanel(int value)
        {
            if( layers[value].childCount<=0 ) return;
            #if UNITY_EDITOR
                DestroyImmediate(layers[value].GetChild(0).gameObject);
            #else
                Destroy(layers[value].GetChild(0).gameObject);
            #endif
        }
        
        [Button("Close All Panels")]
        private void OnCloseAllPanels() 
        {
            foreach (var layer in layers)
            {
                if( layer.childCount<=0 ) return;
#if UNITY_EDITOR
                DestroyImmediate(layer.GetChild(0).gameObject);
#else
                Destroy(layer.GetChild(0).gameObject);
#endif
            }
        }
        
        private void UnSubscription()
        {
            CoreUISignals.Instance.OnOpenPanel-=OnOpenPanel;
            CoreUISignals.Instance.OnClosePanel-=OnClosePanel;
            CoreUISignals.Instance.OnCloseAllPanels-=OnCloseAllPanels;
        }

        private void OnDisable()
        {
            UnSubscription();
        }
    }
}