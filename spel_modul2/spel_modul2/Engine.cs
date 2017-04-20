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

    sealed class Entity
    {
        Dictionary<Type, GameComponent> components;

        Entity()
        {
            components = new Dictionary<Type, GameComponent>();
        }

        void Add(GameComponent component)
        {
            components.Add(component.GetType(), component);
        }

        void Remove(Type component)
        {
            components.Remove(component);
        }

        void Remove<T>()
        {
            components.Remove(typeof(T));
        }
    }

    class EntityManager
    {

    }

    abstract class GameComponent
    {
        abstract public void Insert();
        abstract public void Remove();
        abstract public void Update();
    }

    interface IComponent { }
    interface ISystem
    {
        void Update();
    }

    class ComponentManager
    {
        Dictionary<int, Dictionary<Type, IComponent>> entityComponents;
        Dictionary<Type, Dictionary<int, IComponent>> componentEntities;
        static ComponentManager componentManagerInstance;

        static ComponentManager()
        {
            componentManagerInstance = new ComponentManager();
        }

        public static ComponentManager GetComponentManager()
        {
            return componentManagerInstance;
        }

        private ComponentManager()
        {
            entityComponents = new Dictionary<int, Dictionary<Type, IComponent>>();
            componentEntities = new Dictionary<Type, Dictionary<int, IComponent>>();
        }

        public void AddComponentsToEntity(int entity, params IComponent[] components)
        {
            //Check if the current entity have a dictionary
            if (!entityComponents.ContainsKey(entity) || entityComponents[entity] == null)
                entityComponents[entity] = new Dictionary<Type, IComponent>();

            foreach(IComponent component in components)
            {
                //Check if the component have a dictionary
                if (!componentEntities.ContainsKey(component.GetType()) || componentEntities[component.GetType()] == null)
                    componentEntities[component.GetType()] = new Dictionary<int, IComponent>();

                //Add the component to both dictionaries
                entityComponents[entity].Add(component.GetType(), component);
                componentEntities[component.GetType()][entity] = component;
            }
        }

        public Dictionary<int, IComponent> GetComponents<T>()
        {
            return componentEntities[typeof(T)];
        }

        public Dictionary<Type, IComponent> GetComponentsForEntity(int entity)
        {
            if(entityComponents.ContainsKey(entity))
                return entityComponents[entity];
            return null;
        }
    }
}