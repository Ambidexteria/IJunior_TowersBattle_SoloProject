namespace Base.Soldier
{
    public class SoldierSpawnControllerPresenter
    {
        private readonly SoldierSpawnControllerModel _model;
        private readonly SoldierSpawnControllerView _view;

        public SoldierSpawnControllerPresenter(SoldierSpawnControllerModel model, SoldierSpawnControllerView view)
        {
            _model = model;
            _view = view;
        }

        public void Enable()
        {
            _model.TimeBeforeNextSpawnChanged += OnSpawnTimeChanged;
        }

        public void Disable()
        {
            _model.TimeBeforeNextSpawnChanged -= OnSpawnTimeChanged;
        }

        private void OnSpawnTimeChanged(float time)
        {
            _view.Display(time);
        }
    }
}
