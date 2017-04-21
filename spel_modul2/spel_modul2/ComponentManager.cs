using System;
using System.Collections.Generic;
using spel_modul2;

namespace GameEngine
{
    class ComponentManager
    {
        private Dictionary<int, Dictionary<Type, IComponent>> entityComponents;
        private Dictionary<Type, Dictionary<int, IComponent>> componentEntities;
        private static ComponentManager componentManagerInstance;

        static ComponentManager()
        {
            componentManagerInstance = new ComponentManager();
        }

        private ComponentManager()
        {
            entityComponents = new Dictionary<int, Dictionary<Type, IComponent>>();
            componentEntities = new Dictionary<Type, Dictionary<int, IComponent>>();
        }

        public static ComponentManager GetComponentManager()
        {
            return componentManagerInstance;
        }

        public void AddComponentsToEntity(int entity, params IComponent[] components)
        {
            //Check if the current entity have a dictionary
            if (!entityComponents.ContainsKey(entity) || entityComponents[entity] == null)
                entityComponents[entity] = new Dictionary<Type, IComponent>();

            foreach (IComponent component in components)
            {
                //Check if the component have a dictionary
                if (!componentEntities.ContainsKey(component.GetType()) || componentEntities[component.GetType()] == null)
                    componentEntities[component.GetType()] = new Dictionary<int, IComponent>();

                //Add the component to both dictionaries
                entityComponents[entity].Add(component.GetType(), component);
                componentEntities[component.GetType()][entity] = component;
            }
        }

        public Dictionary<int, IComponent> GetComponentsOfType<T>()
        {
            return componentEntities[typeof(T)];
        }

        public Dictionary<Type, IComponent> GetComponentsForEntity(int entity)
        {
            if (entityComponents.ContainsKey(entity))
                return entityComponents[entity];
            return null;
        }

        public T GetComponentForEntity<T>(int entity) where T : IComponent
        {
            if (entityComponents.ContainsKey(entity))
                return entityComponents[entity][T.GetType()];
        }
    }
}