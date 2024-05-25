using Script.Runtime.Data.ValueObjects;
using UnityEngine;

namespace Script.Runtime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Player", menuName = "Scriptable/CD_Player", order = 0)]
    public class CD_Player : ScriptableObject
    {
        public PlayerData Data;
    }
}