using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    class Engine : Game
    {
        private GraphicsDeviceManager graphics;

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }

    class SystemManager
    {
        private Dictionary<Type, ISystem> systems;
        private static SystemManager systemManagerInstance;

        static SystemManager()
        {
            systemManagerInstance = new SystemManager();
        }

        private SystemManager()
        {
            systems = new Dictionary<Type, ISystem>();
        }

        public static SystemManager GetSystemManager()
        {
            return systemManagerInstance;
        }

        public void AddSystem(ISystem system)
        {
            systems.Add(system.GetType(), system);
        }

        public void RemoveSystem(ISystem system)
        {
            if (systems.ContainsKey(system.GetType()))
                systems.Remove(system.GetType());
        }
    }

    interface IComponent { }
    interface ISystem
    {
        void Update();
    }

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