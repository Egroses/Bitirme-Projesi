using System;
using UnityEngine;

namespace Script.Runtime.Controller.CameraSc
{
    public class MyCameraController : MonoBehaviour
    {
        internal void LockCamera()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        internal void ReleaseCamera()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
