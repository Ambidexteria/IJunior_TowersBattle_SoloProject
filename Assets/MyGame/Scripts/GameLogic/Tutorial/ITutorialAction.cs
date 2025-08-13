using UnityEngine;

namespace Base.GameLogic.Tutorial
{
    public interface ITutorialAction
    {
        void Enable();

        void Disable();

        void SetTarget(Transform target);
    }
}
