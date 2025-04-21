public interface ISoldierState
{
    void OnStart(SoldierStateContext context);

    void OnUpdate();

    void OnStop();
}
