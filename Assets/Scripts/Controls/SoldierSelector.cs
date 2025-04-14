using System;
using UnityEngine;

public class SoldierSelector : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _raycastLength = 200f;

    public event Action<Soldier> Selected;

    public bool TrySelectSoldier(out Soldier soldier, TeamType team)
    {
        soldier = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _raycastLength, _mask))
        {
            if (hit.collider.transform.root.TryGetComponent(out soldier))
            {
                if (soldier.GetTeam() == team && soldier.IsDead() == false)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
