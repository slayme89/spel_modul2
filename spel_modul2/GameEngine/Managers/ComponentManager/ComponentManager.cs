﻿using GameEngine.Components;
using System;
using System.Collections.Generic;

namespace GameEngine.Managers
{
    public partial class ComponentManager
    {
        private Dictionary<int, Dictionary<Type, IComponent>> entityComponents;
        private Dictionary<Type, Dictionary<int, IComponent>> componentGroups;
        private static ComponentManager componentManagerInstance;
        private List<int> removedEntities;
        private List<Tuple<int, Type>> removedComponents;

        static ComponentManager()
        {
            componentManagerInstance = new ComponentManager();
        }

        private ComponentManager()
        {
            entityComponents = new Dictionary<int, Dictionary<Type, IComponent>>();
            componentGroups = new Dictionary<Type, Dictionary<int, IComponent>>();
            removedEntities = new List<int>();
            removedComponents = new List<Tuple<int, Type>>();
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
        }

        public void RemoveEntity(int entity)
        {
            removedEntities.Add(entity);
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
                        componentGroups[type].Remove(entity);
                    }

                    entityComponents.Remove(entity);
                }
                OnEntityRemoved(entity);
            }

            removedEntities.Clear();

            /*if (entityComponents.ContainsKey(entity))
            {
                T component = GetComponentForEntity<T>(entity);
                OnComponentRemoved(entity, component);

                entityComponents[entity].Remove(typeof(T));
                componentGroups[typeof(T)].Remove(entity);
            }*/

            foreach (Tuple<int, Type> entity in removedComponents)
            {
                Dictionary<Type, IComponent> components;
                IComponent component;
                Dictionary<int, IComponent> componentgroup;

                if (entityComponents.TryGetValue(entity.Item1, out components) && components.TryGetValue(entity.Item2, out component))
                {
                    OnComponentRemoved(entity.Item1, component);
                    components.Remove(entity.Item2);
                }

                if (componentGroups.TryGetValue(entity.Item2, out componentgroup))
                    componentgroup.Remove(entity.Item1);
            }

            removedComponents.Clear();
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
            removedComponents.Add(new Tuple<int, Type>(entity, typeof(T)));
        }
    }
}