using Script.Runtime.Data.ValueObjects;
using UnityEngine;

namespace Script.Runtime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Dialog", menuName = "Scriptable/CD_Dialog", order = 0)]
    public class CD_Dialog : ScriptableObject
    {
        public DialogData dialogData;
    }
}