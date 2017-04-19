namespace Engine
{
    abstract class GameSystem
    {
        abstract public void Update();
    }

    class SystemManager
    {

    }

    abstract class GameComponent { }

    enum Component { Position, Scale, Sprite};

    class ComponentManager
    {
        public void AddComponents(int entity, params Component[] component)
        {
        }
    }
}