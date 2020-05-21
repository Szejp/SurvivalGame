public class Player : Agent
{
    protected void Awake()
    {
        //base.Awake();
        GameOvermind.instance.players.Add(this);
    }
}