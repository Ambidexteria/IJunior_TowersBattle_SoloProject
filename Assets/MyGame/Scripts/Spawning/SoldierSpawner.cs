using Base.Data.Game;
using Base.Infrastructure;
using Base.Services.Audio;
using Base.Soldier;
using Zenject;

public class SoldierSpawner : GenericSpawner<SoldierSetup>
{
    private readonly Team _team;
    private readonly SoldierData _stats;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly TeamColorChanger _colorChanger;
    private readonly AudioPlayerService _audioPlayer;

    [Inject]
    public SoldierSpawner(Team team,  SoldierData stats, ICoroutineRunner coroutineRunner, TeamColorChanger colorChanger, SpawnerSettings settings, 
        GenericSpawnableObjectFactory<SoldierSetup> factory, AudioPlayerService audioPlayer) : base(settings, factory) 
    {
        ExceptionsTest.NullRefConstructorTest(nameof(SoldierSpawner), team, stats, coroutineRunner, colorChanger, settings, factory);

        _team = team;
        _stats = stats;
        _coroutineRunner = coroutineRunner;
        _colorChanger = colorChanger;
        _audioPlayer = audioPlayer;
    }

    protected override void PrepareOnCreateObject(ref SoldierSetup soldierSetup)
    {
        soldierSetup.Init(_team, _stats, _coroutineRunner, _colorChanger, _audioPlayer);
    }
}
