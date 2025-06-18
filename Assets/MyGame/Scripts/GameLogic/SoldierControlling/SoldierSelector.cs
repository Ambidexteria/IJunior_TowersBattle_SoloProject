using Base.Services.Factories.Game;
using Base.Soldier;
using UnityEngine;

public class SoldierSelector
{
    private LayerMask _mask;
    private float _raycastLength = 200f;

    public SoldierSelector(RaycastSettings soldierSelectorSettings)
    {
        ExceptionsTest.NullRefConstructorTest(nameof(ControlPointSelector), soldierSelectorSettings);

        _mask = soldierSelectorSettings.LayerMask;
        _raycastLength = soldierSelectorSettings.RaycastLength;
    }

    public bool TrySelectSoldier(out SoldierModel soldier, TeamType team)
    {
        soldier = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _raycastLength, _mask))
        {
            if (hit.collider.transform.root.TryGetComponent(out SoldierSetup setup))
            {
                if (setup.GetSoldier().GetTeam() == team && setup.GetSoldier().IsDead() == false)
                {
                    soldier = setup.GetSoldier();
                    return true;
                }
            }
        }

        return false;
    }
}
