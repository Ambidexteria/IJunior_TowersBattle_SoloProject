using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private SceneChangeController _sceneController;
    [SerializeField] private Player _player;
    [SerializeField] private NPC _npc;
    [SerializeField] private PlayerHUDModel _gameSceneUIController;

    private void OnEnable()
    {
        _player.Defeated += OnPlayerDefeated;
        _npc.Defeated += OnNPCDefeated;
    }

    private void OnDisable()
    {
        _player.Defeated -= OnPlayerDefeated;
        _npc.Defeated -= OnNPCDefeated;
    }

    private void OnPlayerDefeated()
    {
        _gameSceneUIController.ShowDefeatMessage();
        _player.Stop();
        _npc.Stop();
    }

    private void OnNPCDefeated()
    {
        _gameSceneUIController.ShowWinMessage();
        _player.Stop();
        _npc.Stop();
    }
}
