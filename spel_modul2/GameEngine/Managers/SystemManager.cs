using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using GameEngine.Systems;

namespace GameEngine.Managers
{
    public class SystemManager
    {
        private Dictionary<Type, object> systems;
        private static SystemManager systemManagerInstance;

        static SystemManager()
        {
            systemManagerInstance = new SystemManager();
        }

        private SystemManager()
        {
            systems = new Dictionary<Type, object>();
        }

        public static SystemManager GetInstance()
        {
            return systemManagerInstance;
        }

        public void AddSystem(object system)
        {
            systems.Add(system.GetType(), system);
        }

        public void AddSystems(params object[] systems)
        {
            foreach (var system in systems)
                AddSystem(system);
        }

        public void RemoveSystem(object system)
        {
            if (systems.ContainsKey(system.GetType()))
                systems.Remove(system.GetType());
        }

        public T GetSystem<T>()
        {
            object system;
            systems.TryGetValue(typeof(T), out system);
            return (T)system;
        }

        public void Update<T>(GameTime gameTime)
        {
            object system;
            systems.TryGetValue(typeof(T), out system);
            ((ISystem)system)?.Update(gameTime);
        }

        public void UpdateAllSystems(GameTime gameTime)
        {
            foreach (var system in systems.Values)
                if (system is ISystem)
                    ((ISystem)system).Update(gameTime);
        }

        public void Render<T>(RenderHelper renderHelper)
        {
            object system;
            systems.TryGetValue(typeof(T), out system);
            ((IRenderSystem)system)?.Render(renderHelper);
        }

        public void RenderAllSystems(RenderHelper renderHelper)
        {
            foreach (var system in systems.Values)
            {
                if(system is IRenderSystem)
                    ((IRenderSystem)system).Render(renderHelper);
            }
        }
    }
}