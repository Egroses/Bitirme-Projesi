using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    #region singleton
    private static MapManager instance;
    public static MapManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
    /* //instant map tracking
    private void Update()
    {
        
    }*/

    public void AimTarget()
    {
        
    }
}
