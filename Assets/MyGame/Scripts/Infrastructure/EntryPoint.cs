using UnityEngine;
using Zenject;

namespace Base.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            ExceptionsTest.NullRefTest(nameof(EntryPoint), nameof(Awake), _game);

            _game.EnterBootstrapState();

            DontDestroyOnLoad(gameObject);
        }

        [Inject]
        private void Init(Game game)
        {
            _game = game;
        }
    }
}
