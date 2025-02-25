using UnityEngine;

public interface IMovable
{
    void MoveTo(Transform target);

    void Stop();

    bool TargetReached();
}
