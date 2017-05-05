using System;
using System.Collections.Generic;

namespace GameEngine
{
    class ComponentManager
    {
        private Dictionary<int, Dictionary<Type, IComponent>> entityComponents;
        private Dictionary<Type, Dictionary<int, IComponent>> componentGroups;
        private static ComponentManager componentManagerInstance;
        private List<int> removedEntities;

        static ComponentManager()
        {
            componentManagerInstance = new ComponentManager();
        }

        private ComponentManager()
        {
            entityComponents = new Dictionary<int, Dictionary<Type, IComponent>>();
            componentGroups = new Dictionary<Type, Dictionary<int, IComponent>>();
            removedEntities = new List<int>();
        }

        public static ComponentManager GetInstance()
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
                if (!componentGroups.ContainsKey(component.GetType()) || componentGroups[component.GetType()] == null)
                    componentGroups[component.GetType()] = new Dictionary<int, IComponent>();

                //Add the component to both dictionaries
                entityComponents[entity].Add(component.GetType(), component);
                componentGroups[component.GetType()][entity] = component;
            }
        }

        public void AddEntityWithComponents(params IComponent[] components)
        {
            int entity = EntityManager.GetEntityId();
            AddComponentsToEntity(entity, components);
        }

        public void RemoveEntity(int entity)
        {
            removedEntities.Add(entity);
        }

        public void Update()
        {
            foreach(int entity in removedEntities)
            {
                var components = entityComponents[entity];
                var type = components.GetType().GetGenericArguments()[0];
                var group = componentGroups[type];

                group.Remove(entity);
                entityComponents.Remove(entity);
            }

            removedEntities.Clear();
        }

        public Dictionary<int, IComponent> GetComponentsOfType<T>()
        {
            Dictionary<int, IComponent> components;
            componentGroups.TryGetValue(typeof(T), out components);

            return components ?? new Dictionary<int, IComponent>();
        }

        public Dictionary<Type, IComponent> GetComponentsForEntity(int entity)
        {
            if (entityComponents.ContainsKey(entity))
                return entityComponents[entity];
            return null;
        }

        public bool HasEntityComponent<T>(int entity)
        {
            Dictionary<Type, IComponent> components;

            if (entityComponents.TryGetValue(entity, out components))
            {
                return components.ContainsKey(typeof(T));
            }

            return false;
        }

        public T GetComponentForEntity<T>(int entity) where T : IComponent
        {
            if (entityComponents.ContainsKey(entity))
            {
                var components = entityComponents[entity];
                IComponent component;
                components.TryGetValue(typeof(T), out component);
                return (T)component;
            }
            return default(T);
        }

        public void RemoveComponentFromEntity<T>(int entity) where T : IComponent
        {
            if (entityComponents[entity].ContainsKey(typeof(T)))
            {
                entityComponents[entity].Remove(typeof(T));
                componentGroups[typeof(T)].Remove(entity);
            }
        }
    }
}