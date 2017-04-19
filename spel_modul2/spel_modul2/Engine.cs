using System;
using System.Collections.Generic;

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

    class ComponentManager
    {
        Dictionary<int, Dictionary<Type, GameComponent>> entityComponents;

        public void AddComponents(int entity, params GameComponent[] components)
        {
            if (!entityComponents.ContainsKey(entity) || entityComponents[entity] == null)
                entityComponents[entity] = new Dictionary<Type, GameComponent>();

            foreach(GameComponent component in components)
            {
                entityComponents[entity].Add(component.GetType(), component);
            }
        }

        public Dictionary<Type, GameComponent> GetComponents(int entity)
        {
            if(entityComponents.ContainsKey(entity))
                return entityComponents[entity];
            return null;
        }
    }
}