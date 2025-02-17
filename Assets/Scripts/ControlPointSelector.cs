using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPointSelector : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _raycastLength = 200f;

    public bool TrySelectControlPoint(out ControlPoint controlPoint)
    {
        controlPoint = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _raycastLength, _mask))
        {
            if (hit.collider.transform.root.TryGetComponent(out controlPoint))
            {
                return true;
            }
        }

        return false;
    }
}
