namespace GameEngine.Components
{
    public class PlayerComponent : IComponent
    {
        public int Number { get; set; }

        public PlayerComponent(int playerNum)
        {
            Number = playerNum;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
