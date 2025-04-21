using UnityEngine;

public class StageLoader : MonoBehaviour
{
    private const string StageId = nameof(StageId);

    [SerializeField] private StagesDatabase _database;
    private int _stageId;

    private void Awake()
    {
        _stageId = PlayerPrefs.GetInt(StageId);

        if(_database.TryGetStageById(out Stage stage, _stageId))
        {
            Debug.Log($"stage with id = {_stageId} sucessfully loaded");
            Instantiate(stage);
            stage.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log($"Cannot load stage with id = {_stageId}");
        }
    }
}
