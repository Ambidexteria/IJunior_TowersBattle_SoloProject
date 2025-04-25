using UnityEngine;
using Zenject;

namespace Base.Infrastructure
{
    public class EntryPoint : MonoBehaviour, ICoroutineRunner
    {
        private ProjectContext _projectContext;
        private DiContainer _container;
        private Game _game;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _game.GameStateMachine.Enter<BootstrapState>();
        }

        [Inject]
        private void Init(Game game)
        {
            _game = game;
        }
    }
}
