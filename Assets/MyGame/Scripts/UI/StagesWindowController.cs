using System.Collections.Generic;
using UnityEngine;

public class StagesWindowController : UIWindowController
{
    private const string StageId = nameof(StageId);

    [SerializeField] private StagesDatabase _stageDatabasePrefab;
    [SerializeField] private UIWindowController _stagesCntainerWindow;

    private List<StageIconController> _stageIconList;
    private StagesDatabase _database;

    private void Awake()
    {
        if(FindObjectOfType<StagesDatabase>() == null)
            _database = Instantiate(_stageDatabasePrefab);

        _stageIconList = _database.GetStageIconControllersList();
    }

    private void Start()
    {
        foreach (var stageIcon in _stageIconList)
        {
            stageIcon.transform.SetParent(_stagesCntainerWindow.transform);
            Debug.Log("parent set");
        }
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
