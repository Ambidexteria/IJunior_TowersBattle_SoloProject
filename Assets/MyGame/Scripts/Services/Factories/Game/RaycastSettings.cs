using UnityEngine;
using System;

namespace Base.Services.Factories.Game
{
    [Serializable]
    public class RaycastSettings
    {
        public LayerMask LayerMask;
        public float RaycastLength;
    }
}
