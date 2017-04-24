using System;
using System.Collections.Generic;
using spel_modul2;

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
}