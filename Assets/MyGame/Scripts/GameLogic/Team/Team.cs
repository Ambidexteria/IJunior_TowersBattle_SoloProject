public class Team
{
    private TeamType _type;

    public Team(TeamType teamType)
    {
        _type = teamType;
    }

    public TeamType Type => _type;
}
