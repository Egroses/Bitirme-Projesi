using System;

namespace Script.Runtime.Data.ValueObjects
{
    [Serializable]
    public struct PlayerData
    {
        public PlayerMovementData movementData;
    }
    [Serializable]
    public struct PlayerMovementData
    {
        public float walkSpeed;
        public float walkMaxSpeed;
        public float runSpeed;
        public float runMaxSpeed;
        public float RotationSpeed;
    }
}