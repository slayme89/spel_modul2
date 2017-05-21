using GameEngine.Components;
using System;
using System.Collections.Generic;

namespace GameEngine.Managers
{
    public partial class ComponentManager
    {
        public bool TryGetEntityComponent<T>(int entity, out T arg)
        {
            Dictionary<Type, IComponent> components;
            IComponent component;
            arg = default(T);

            if (entityComponents.TryGetValue(entity, out components) && components.TryGetValue(typeof(T), out component))
            {
                arg = (T)component;
                return true;
            }

            return false;
        }

        public bool TryGetEntityComponents<T1, T2>(int entity, out T1 arg1, out T2 arg2)
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

        public bool TryGetEntityComponents<T1, T2, T3>(int entity, out T1 arg1, out T2 arg2, out T3 arg3)
        {
            arg1 = default(T1);
            arg2 = default(T2);
            arg3 = default(T3);
            Dictionary<Type, IComponent> components;

            if (entityComponents.TryGetValue(entity, out components))
            {
                IComponent arg1out, arg2out, arg3out;
                if (components.TryGetValue(typeof(T1), out arg1out) &&
                    components.TryGetValue(typeof(T2), out arg2out) &&
                    components.TryGetValue(typeof(T3), out arg3out))
                {
                    arg1 = (T1)arg1out;
                    arg2 = (T2)arg2out;
                    arg3 = (T3)arg3out;
                    return true;
                }
            }

            return false;
        }

        public bool TryGetEntityComponents<T1, T2, T3, T4>(int entity, out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4)
        {
            arg1 = default(T1);
            arg2 = default(T2);
            arg3 = default(T3);
            arg4 = default(T4);
            Dictionary<Type, IComponent> components;

            if (entityComponents.TryGetValue(entity, out components))
            {
                IComponent arg1out, arg2out, arg3out, arg4out;
                if (components.TryGetValue(typeof(T1), out arg1out) &&
                    components.TryGetValue(typeof(T2), out arg2out) &&
                    components.TryGetValue(typeof(T3), out arg3out) &&
                    components.TryGetValue(typeof(T4), out arg4out))
                {
                    arg1 = (T1)arg1out;
                    arg2 = (T2)arg2out;
                    arg3 = (T3)arg3out;
                    arg4 = (T4)arg4out;
                    return true;
                }
            }

            return false;
        }
    }
}