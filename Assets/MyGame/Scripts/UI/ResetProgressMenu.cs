using Base.Data.Game;
using Base.Services.PersistentProgress;
using Base.Services.SaveLoad;
using UnityEngine;
using Zenject;

public class ResetProgressMenu : MonoBehaviour
{
    [SerializeField] private ButtonClickHandler _confirmButton;

    private ISaveLoadService _saveLoadService;
    private IPersisentDataService _persisentDataService;

    [Inject]
    private void Init(ISaveLoadService saveLoadService, IPersisentDataService persisentDataService)
    {
        _saveLoadService = saveLoadService;
        _persisentDataService = persisentDataService;
    }

    private void OnEnable()
    {
        _confirmButton.Clicked += OnConfirmButtonClicked;
    }

    private void OnDisable()
    {
        _confirmButton.Clicked -= OnConfirmButtonClicked;
    }

    private void OnConfirmButtonClicked()
    {
        GameData gameData = _persisentDataService.GameData;
        gameData.PlayerData.Upgrades = new();
        gameData.PlayerData.CannonData = new();
        gameData.PlayerData.SoldierData = new();
        gameData.StagesData = new();
        _persisentDataService.GameData = gameData;

        _saveLoadService.SaveProgress();
    }
}
