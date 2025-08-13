using Base.UI.StateMachine;
using UnityEngine;
using YG;

public class AuthorizationMenu : MonoBehaviour
{
    [SerializeField] private ButtonClickHandler _confirmButton;
    [SerializeField] private ButtonClickHandler _cancelButton;

    private MainMenuUIStateMachine _stateMachine;

    public void Init(MainMenuUIStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    private void OnEnable()
    {
        if (_stateMachine == null)
            throw new System.NullReferenceException(nameof(AuthorizationMenu));

        _confirmButton.Clicked += OnConfirmButtonClicked;
        _cancelButton.Clicked += OnCancelButtonCliked;
    }

    private void OnDisable()
    {
        _confirmButton.Clicked -= OnConfirmButtonClicked;
        _cancelButton.Clicked -= OnCancelButtonCliked;
    }

    private void OnConfirmButtonClicked()
    {
        YG2.OpenAuthDialog();
        YG2.onGetSDKData += OnAuthorizationCompleted;
    }

    private void OnCancelButtonCliked()
    {
        _stateMachine.Enter<MainMenuState>();
    }

    private void OnAuthorizationCompleted()
    {
        _stateMachine.Enter<LeaderboardWindowState>();
    }
}
