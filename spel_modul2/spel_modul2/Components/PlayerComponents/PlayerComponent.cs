namespace GameEngine
{
    public class PlayerComponent : IComponent
    {
        public int Number { get; set; }
        public PlayerComponent(int playerNum)
        {
            Number = playerNum;
        }
    }
}
