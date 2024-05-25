using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Runtime.Managers
{
    public class GameStateManager : MonoBehaviour
    {
        #region Singleton

        public static GameStateManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

        }

        #endregion
        
        public bool playerCanMove = true;
        public bool playerCanInteract = true;


        public void SetPlayerCantMove()
        {
            playerCanMove = false;
        }
        
        public void SetPlayerCanMove()
        {
            playerCanMove = true;
        }
        
        public void SetPlayerCantInteract()
        {
            playerCanInteract = false;
        }
        
        public void SetPlayerCanInteract()
        {
            playerCanInteract = true;
        }
    }
}