namespace Base.UI.StageSelection
{
    public class StageSelectionMenuPresenter
    {
        private readonly StageSelectionMenuView _view;
        private readonly StageSelectionMenuModel _model;

        public StageSelectionMenuPresenter(StageSelectionMenuView view, StageSelectionMenuModel model)
        {
            _view = view;
            _model = model;
        }

        public void Enable()
        {
            _view.StageSelected += OnStageIconClicked;
        }

        private void OnStageIconClicked(string name)
        {
        }

        private void OnStageSelected(string name)
        {
            _view.SetActiveStageIcon(name);
        }
    }
}
