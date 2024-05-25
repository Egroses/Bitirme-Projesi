using UnityEngine;

namespace Script.Runtime.Controller.Navmesh
{
    public class NavmeshAgentAnimationController : MonoBehaviour
    {
        #region Variables

        #region Serialized Variables
        
        [SerializeField] private Animator animator;
        
        #endregion

        #endregion
        
        public void OnWalkingTrue()
        {
            animator.SetBool("Walking",true);
        }

        public void OnWalkingFalse()
        {
            animator.SetBool("Walking",false);
        }
        public void OnWavingTrue()
        {
            animator.SetBool("Started",false);
        }

        public void OnWavingFalse()
        {
            animator.SetBool("Started",true);
        }
        public void OnStandTrue()
        {
            animator.SetBool("Waiting",true);
        }

        public void OnStandFalse()
        {
            animator.SetBool("Waiting",false);
        }
    }
}