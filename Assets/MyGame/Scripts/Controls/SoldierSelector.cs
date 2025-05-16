using Base.Services.Factories.Game;
using UnityEngine;

public class SoldierSelector
{
    private LayerMask _mask;
    private float _raycastLength = 200f;

    public SoldierSelector(RaycastSettings soldierSelectorSettings)
    {
        _mask = soldierSelectorSettings.LayerMask;
        _raycastLength = soldierSelectorSettings.RaycastLength;
    }

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
