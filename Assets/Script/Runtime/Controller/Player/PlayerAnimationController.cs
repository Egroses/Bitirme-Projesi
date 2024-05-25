using System;
using Script.Runtime.Keys;
using Script.Runtime.Signals;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Runtime.Controller.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Variables

        #region Serialized Variables
        
        [SerializeField] private Animator animator;
        
        #endregion

        #endregion
        
        public void OnWalkingTrue()
        {
            animator.SetBool("walking",true);
        }

        public void OnWalkingFalse()
        {
            animator.SetBool("walking",false);
        }

        public void OnRunningTrue()
        {
            animator.SetBool("running",true);
        }
        public void OnRunningFalse()
        {
            animator.SetBool("running",false);
        }
    }
}