using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StagesDatabase : MonoBehaviour
{
    [SerializeField] private StageIconController _prefab;
    [SerializeField] private List<Stage> _loadedStages;
    [SerializeField] private string _path;

    [SerializeField] private List<StageIconController> _stageIconControllersList;

    private void Awake()
    {
        _loadedStages = Resources.LoadAll<Stage>(_path).ToList();
        Debug.Log("Stages loaded");
        CreateStageIconControllers();
    }

    public bool TryGetStageById(out Stage stage,int id)
    {
        stage = null;

        foreach(var tempStage in _loadedStages)
        {
            if(tempStage.Id == id)
            {
                stage = tempStage;
                return true;
            }
        }

        return false;
    }

    public List<StageIconController> GetStageIconControllersList()
    {
        return new List<StageIconController>(_stageIconControllersList);
    }

    private void CreateStageIconControllers()
    {
        StageIconController stageIconController;

        foreach (var stage in _loadedStages)
        {
            stageIconController = Instantiate(_prefab);
            stageIconController.SetStage(stage);
            _stageIconControllersList.Add(stageIconController);
        }
    }
}
