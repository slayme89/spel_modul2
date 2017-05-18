namespace GameEngine.Managers
{
    public class EntityManager
    {
        static int nextId;

        public static int GetEntityId()
        {
            return nextId++;
        }
    }
}