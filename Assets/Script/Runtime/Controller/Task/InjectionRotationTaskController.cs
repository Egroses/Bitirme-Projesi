using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

namespace Script.Runtime.Controller.Task
{
    public class InjectionRotationTaskController : MonoBehaviour,IDragHandler,IBeginDragHandler
    {
        #region Variables

        #region SerializeVariables

        [SerializeField] private TextMeshProUGUI degreeText;
        [SerializeField] private Button completeButton; 
        [SerializeField] private Image degreeMesurementImage; 
        [SerializeField] private float leftLimit;
        [SerializeField] private float rightLimit;
        [SerializeField] private float targetAngle;
        [SerializeField] private float maxDistance;
        
        #endregion
        #region Private Variables

        private Vector3 _tempPosition;
        private Vector3 _distancePosition;
        private Quaternion _targetRotation;
        #endregion

        #endregion

        private void Start()
        {
            var eulerAngles = transform.eulerAngles;
            
            degreeText.text = "Degree: "+(eulerAngles.z-leftLimit).ToString("F0")+"\u00b0";
            degreeMesurementImage.fillAmount = (eulerAngles.z - leftLimit) / 180;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _tempPosition = Input.mousePosition;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            _distancePosition = Input.mousePosition - _tempPosition;
            _tempPosition = Input.mousePosition;
            _targetRotation = Quaternion.AngleAxis(_distancePosition.y*0.2f,Vector3.forward);
            var transform1 = transform;
            var rotation = transform1.rotation;
            rotation *= _targetRotation;

            var eulerAnglesZ = rotation.eulerAngles.z;
            
            if (eulerAnglesZ <= rightLimit && eulerAnglesZ >= leftLimit)
            {
                transform1.rotation = rotation;
                degreeMesurementImage.fillAmount = (eulerAnglesZ - leftLimit )/ 180;
                degreeText.text = "Degree: "+(eulerAnglesZ-leftLimit).ToString("F0")+"\u00b0";

                if (eulerAnglesZ<targetAngle+maxDistance && eulerAnglesZ>targetAngle-maxDistance)
                {
                    completeButton.interactable = true;
                }
                else
                {
                    completeButton.interactable = false;
                }
            }
        }
    }
}