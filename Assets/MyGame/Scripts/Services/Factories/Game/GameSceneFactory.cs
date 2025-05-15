using UnityEngine;
using System;
using Base.Services.AssetManagment;
using Base.GameLogic.Cannon;
using Zenject;

namespace Base.Services.Factories.Game
{
    public class GameSceneFactory : MonoBehaviour
    {
        private const string PlayerCannon = "GameLogic/Cannon/NPCCannon Variant";
        private const string EnemyCannon = "GameLogic/Cannon/NPCCannon Variant";

        private AssetLoader _assetLoader;

        private CannonModel _playerCannon;
        private CannonModel _NPCCannon;

        [Inject]
        private void Init(AssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }

        public GameSceneFactory(AssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }

        private void Awake()
        {
            CreatePlayer();
            CreateNPC();

            _playerCannon.SetEnemy(_NPCCannon);
            _NPCCannon.SetEnemy(_playerCannon);
        }

        public void CreatePlayer()
        {
            Team team = new Team();
            team.SetType(TeamType.Player);

            _playerCannon = CreateCannon(PlayerCannon, team, 20, 2);
        }

        public void CreateNPC()
        {
            Team team = new Team();
            team.SetType(TeamType.NPC);

            _NPCCannon = CreateCannon(PlayerCannon, team, 10, 2);
        }

        private CannonModel CreateCannon(string assetPath, Team team, int damage, float fireDelay)
        {
            GameObject cannon = _assetLoader.Instantiate(assetPath);
            CannonSetup setup = cannon.GetComponent<CannonSetup>();
            team.SetType(TeamType.Player);
            setup.Init(team, damage, fireDelay);

            return cannon.GetComponent<CannonSetup>().GetModel();
        }
    }
}
