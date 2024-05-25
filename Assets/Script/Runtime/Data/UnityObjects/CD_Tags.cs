using Script.Runtime.Data.ValueObjects;
using UnityEngine;

namespace Script.Runtime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Tags", menuName = "Scriptable/CD_Tags", order = 0)]
    public class CD_Tags : ScriptableObject
    {
        public const string PlayerTag = "Player";
        public const string NpcTag = "Npc";
        public const string InteractionTag = "Interaction";
        public const string NavmeshAgent = "Agent";
    }
}