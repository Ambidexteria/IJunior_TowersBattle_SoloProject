using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private StageSceneController _sceneController;
    [SerializeField] private Player _player;
    [SerializeField] private NPC _npc;
    [SerializeField] private UIWindowController _winMessage;
    [SerializeField] private UIWindowController _defeatMessage;

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
        Debug.Log("Player defeated");
        _defeatMessage.Show();
        _player.Stop();
        _npc.Stop();
    }

    private void OnNPCDefeated()
    {
        _winMessage.Show();
        _player.Stop();
        _npc.Stop();
    }
}
