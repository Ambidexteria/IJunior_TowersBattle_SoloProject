using System.Collections.Generic;
using UnityEngine;

public class StagesWindowController : UIWindowController
{
    private const string StageId = nameof(StageId);

    [SerializeField] private StagesDatabase _stageListLoader;
    [SerializeField] private UIWindowController _stagesCntainerWindow;

    private List<StageIconController> _stageIconList;

    private void Awake()
    {
        _stageIconList = _stageListLoader.GetStageIconControllersList();

        foreach (var stageIcon in _stageIconList)
            stageIcon.transform.SetParent(_stagesCntainerWindow.transform);
    }

    private void OnEnable()
    {
        foreach (var stageIcon in _stageIconList)
        {
            stageIcon.Choosed += OnStageIconChoosed;
        }
    }

    private void OnDisable()
    {
        foreach (var stageIcon in _stageIconList)
        {
            stageIcon.Choosed -= OnStageIconChoosed;
        }
    }

    private void OnStageIconChoosed(StageIconController stageIcon)
    {
        PlayerPrefs.SetInt(StageId, stageIcon.StageId);
        Debug.Log($"choosed stage with id = {stageIcon.StageId}");
    }
}
