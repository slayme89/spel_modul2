using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameEngine
{
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

        public static SystemManager GetInstance()
        {
            return systemManagerInstance;
        }

        public void AddSystem(ISystem system)
        {
            systems.Add(system.GetType(), system);
        }

        public void AddSystems(params ISystem[] systems)
        {
            foreach (ISystem system in systems)
                AddSystem(system);
        }

        public void RemoveSystem(ISystem system)
        {
            if (systems.ContainsKey(system.GetType()))
                systems.Remove(system.GetType());
        }

        public T GetSystem<T>()
        {
            ISystem system;
            systems.TryGetValue(typeof(T), out system);
            return (T)system;
        }

        public void Update<T>(GameTime gameTime)
        {
            ISystem system;
            systems.TryGetValue(typeof(T), out system);
            system?.Update(gameTime);
        }

        public void UpdateAllSystems(GameTime gameTime)
        {
            foreach (ISystem system in systems.Values)
                system.Update(gameTime);
        }
    }
}