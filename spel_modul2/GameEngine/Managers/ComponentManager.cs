using GameEngine.Components;
using GameEngine.Managers;
using System;
using System.Collections.Generic;

namespace GameEngine.Managers
{
    public class ComponentManager
    {
        private Dictionary<int, Dictionary<Type, IComponent>> entityComponents;
        private List<int> entities;
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
            entities = new List<int>();
        }

        public static ComponentManager GetInstance()
        {
            return componentManagerInstance;
        }

        public delegate void OnComponentAddedEvent(int entity, IComponent component);
        public event OnComponentAddedEvent OnComponentAdded;

        public delegate void OnComponentRemovedEvent(int entity, IComponent component);
        public event OnComponentRemovedEvent OnComponentRemoved;

        public delegate void OnEntityRemovedEvent(int entity);
        public event OnEntityRemovedEvent OnEntityRemoved;

        public List<int> GetEntities()
        {
            return entities;
        }

        public bool HasEntity(int entity)
        {
            return entityComponents.ContainsKey(entity);
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
                OnComponentAdded(entity, component);
            }
        }

        public void AddEntityWithComponents(params IComponent[] components)
        {
            int entity = EntityManager.GetEntityId();
            AddComponentsToEntity(entity, components);
            entities.Add(entity);
        }

        public void RemoveEntity(int entity)
        {
            removedEntities.Add(entity);
            entities.Remove(entity);
        }

        internal void Update()
        {
            foreach (int entity in removedEntities)
            {
                Dictionary<Type, IComponent> components;
                if (entityComponents.TryGetValue(entity, out components))
                {
                    foreach (var type in components.Keys)
                    {
                        Dictionary<int, IComponent> group;
                        componentGroups.TryGetValue(type, out group);
                        group?.Remove(entity);
                    }

                    entityComponents.Remove(entity);
                }
                OnEntityRemoved(entity);
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

        public bool TryGetComponentForEntity<T>(int entity, out T arg)
        {
            Dictionary<Type, IComponent> components;
            IComponent component;
            arg = default(T);

            if(entityComponents.TryGetValue(entity, out components) && components.TryGetValue(typeof(T), out component))
            {
                arg = (T)component;
                return true;
            }

            return false;
        }

        public bool TryGetComponentsForEntity<T1, T2>(int entity, out T1 arg1, out T2 arg2)
        {
            arg1 = default(T1);
            arg2 = default(T2);
            Dictionary<Type, IComponent> components;

            if (entityComponents.TryGetValue(entity, out components))
            {
                IComponent arg1out, arg2out;
                if (components.TryGetValue(typeof(T1), out arg1out) && components.TryGetValue(typeof(T2), out arg2out))
                {
                    arg1 = (T1)arg1out;
                    arg2 = (T2)arg2out;
                    return true;
                }
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
            if(entityComponents.ContainsKey(entity))
            {
                T component = GetComponentForEntity<T>(entity);
                OnComponentRemoved(entity, component);

                entityComponents[entity].Remove(typeof(T));
                componentGroups[typeof(T)].Remove(entity);
            }
        }
    }
}