using Base.Services.Factories.Game;
using UnityEngine;

public class ControlPointSelector
{

    private LayerMask _mask;
    private float _raycastLength;

    public ControlPointSelector(RaycastSettings controlPointSelectorSettings)
    {
        _mask = controlPointSelectorSettings.LayerMask;
        _raycastLength = controlPointSelectorSettings.RaycastLength;
    }

    public bool TrySelectControlPoint(out ControlPoint controlPoint)
    {
        controlPoint = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _raycastLength, _mask))
        {
            if (hit.collider.transform.TryGetComponent(out controlPoint))
            {
                return true;
            }
        }

        return false;
    }
}
