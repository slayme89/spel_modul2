using System.Collections.Generic;
using GameEngine.Components;

namespace Game
{
    public class EntityBuilder
    {
        List<IComponent> components;

        public EntityBuilder()
        {
            components = new List<IComponent>();
        }

        public EntityBuilder SetPositon(int x, int y)
        {
            components.Add(new PositionComponent(x, y));
            return this;
        }

        public IComponent[] Build()
        {
            return components.ToArray();
        }
    }
}
