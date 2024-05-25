using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Script.Runtime.Data.UnityObjects;
using Unity.VisualScripting;

namespace Script.Runtime.Controller.Door
{
    public class DoorController : MonoBehaviour
    {
        #region serializefields

        [SerializeField] private float doorDirection;

        #endregion

        #region private

        private GameObject _door;
        private string _lastEnter;

        #endregion
        
        private void Start()
        { 
            _door = transform.GetChild(0).gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals(CD_Tags.PlayerTag) || other.tag.Equals(CD_Tags.NavmeshAgent))
            {
                _lastEnter = other.tag;
                var position = _door.transform.position;
                _door.transform.DOMove(new Vector3(position.x, position.y, transform.position.z+(1.2f*doorDirection)), 0.5f);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals(_lastEnter))
            {
                var position = _door.transform.position;
                _door.transform.DOMove(new Vector3(position.x, position.y, transform.position.z+(.37f*doorDirection)), 0.5f);
            }
        }
    }
}