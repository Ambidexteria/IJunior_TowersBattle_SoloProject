using UnityEngine;

namespace Base.GameLogic.Tutorial
{
    public class LaunchMinigameTutorialAction : ITutorialAction
    {
        private readonly ButtonClickHandler _launchMinigameButton;
        private readonly TutorialBattleController _tutorialBattleController;

        public LaunchMinigameTutorialAction(ButtonClickHandler launchMinigameButton, TutorialBattleController tutorialBattleController)
        {
            _launchMinigameButton = launchMinigameButton;
            _tutorialBattleController = tutorialBattleController;
        }

        public void Enable()
        {
            _tutorialBattleController.IncreaseEnergyIncome();
            _tutorialBattleController.PlaceFrame(_launchMinigameButton.transform);
            _launchMinigameButton.Clicked += OnButtonClicked;
        }

        public void Disable()
        {
            _tutorialBattleController.DecreaseEnergyIncome();
            _tutorialBattleController.HideFrame();
            _launchMinigameButton.Clicked -= OnButtonClicked;
        }

        public void SetTarget(Transform target)
        {
        }

        private void OnButtonClicked()
        {
            _tutorialBattleController.EndTutorial();
        }
    }
}
