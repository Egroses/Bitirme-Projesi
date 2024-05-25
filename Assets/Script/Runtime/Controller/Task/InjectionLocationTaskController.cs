using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using Script.Runtime.Signals;
using Unity.VisualScripting;

namespace Script.Runtime.Controller.Task
{
    public class InjectionLocationTaskController : MonoBehaviour
    {
        [SerializeField] private string trueTagString;
        [SerializeField] private CinemachineVirtualCamera injectionTaskCamera;
        [SerializeField] private GameObject womanModel;
        [SerializeField] private GameObject anatomyModel;
        [SerializeField] private Material taskSuccessMaterial;
        [SerializeField] private Material taskFailMaterial;
        
        private DialogueSystemTrigger _dialogueSystemTrigger;
        private bool _taskBoolForModels;
        private bool _taskIsDone;
        private bool _taskIsStarted;
        private Camera _mainCamera;

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _mainCamera = Camera.main;
            _dialogueSystemTrigger = GetComponent<DialogueSystemTrigger>();
        }

        private void OnEnable()
        {
            _taskIsStarted = false;
            OnSubscription();
        }

        private void OnSubscription()
        {
            TaskSignals.Instance.OnInjectionLocationStart += OnInjectionLocationStart;
        }

        public void OnInjectionLocationStart()
        {
            _taskIsStarted = true;
            _taskIsDone = false;
            _taskBoolForModels = true;
            SetGameObjectActives();
        }

        private void SetGameObjectActives()
        {
            injectionTaskCamera.gameObject.SetActive(_taskBoolForModels);
            womanModel.SetActive(!_taskBoolForModels);
            anatomyModel.SetActive(_taskBoolForModels);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _taskIsStarted)
            {
                SendRayCast();
            }
        }

        private void SendRayCast()
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                var rendererOfHit = hit.transform.gameObject.GetComponent<Renderer>();
                Debug.DrawLine(ray.origin, hit.point);

                StartCoroutine(ChangeMaterialBack(hit.transform,rendererOfHit));
            }
        }

        private IEnumerator ChangeMaterialBack(Transform hitTransform,Renderer rendererOfHit)
        {
            var oldMaterial = rendererOfHit.material;
            if (oldMaterial.color.ToHexString() != taskFailMaterial.color.ToHexString())
            {
                if (hitTransform.transform.tag.Equals(trueTagString)) // Burada dialog devam etsin
                {
                    rendererOfHit.material = taskSuccessMaterial;
                    _dialogueSystemTrigger.enabled = true;
                    _taskIsDone = true;
                }
                else
                {
                    rendererOfHit.material = taskFailMaterial;
                }

                yield return new WaitForSeconds(1f);

                rendererOfHit.material = oldMaterial;
                _dialogueSystemTrigger.enabled = false;

                if (_taskIsDone)
                {
                    _taskIsStarted = false;
                    _taskBoolForModels = false;
                    SetGameObjectActives();
                }
            }
        }

        private void OnUnSubscription()
        {
            TaskSignals.Instance.OnInjectionLocationStart -= OnInjectionLocationStart;
        }
        private void OnDisable()
        {
            OnUnSubscription();
        }
    }
}