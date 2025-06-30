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
        ExceptionsTest.NullRefMethodTest(nameof(AuthorizationMenu), nameof(Init), stateMachine);

        _stateMachine = stateMachine;
    }

    private void Awake()
    {
        ExceptionsTest.NullRefMethodTest(nameof(AuthorizationMenu), nameof(Awake), _confirmButton, _cancelButton);
    }

    private void OnEnable()
    {
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
