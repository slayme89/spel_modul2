using GameEngine.Components;
using GameEngine.Managers;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Group<T1, T2> : IEnumerable<Tuple<T1, T2>>
    {
        ComponentManager cm = ComponentManager.GetInstance();
        Dictionary<int, Tuple<T1, T2>> entities;
        Type t1, t2;

        public Group()
        {
            cm.OnComponentAdded += ComponentAdded;
            cm.OnComponentRemoved += ComponentRemoved;
            cm.OnEntityRemoved += EntityRemoved;

            entities = new Dictionary<int, Tuple<T1, T2>>();
            t1 = typeof(T1);
            t2 = typeof(T2);
        }

        private void EntityRemoved(int entity)
        {
            entities.Remove(entity);
        }

        private void ComponentRemoved(int entity, IComponent component)
        {
            Type t = component.GetType();
            if (t == t1 || t == t2)
                entities.Remove(entity);
        }

        private void ComponentAdded(int entity, IComponent component)
        {
            IComponent v1, v2;
            Dictionary<Type, IComponent> components;
            Type t;

            if (entities.ContainsKey(entity))
                return;
            t = component.GetType();
            components = cm.GetComponentsForEntity(entity);
            if (components.TryGetValue(t1, out v1) && components.TryGetValue(t2, out v2))
                entities.Add(entity, Tuple.Create((T1)v1, (T2)v2));
        }

        public IEnumerator<Tuple<T1, T2>> GetEnumerator()
        {
            foreach (var entity in entities)
            {
                yield return entity.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var entity in entities)
            {
                yield return entity.Value;
            }
        }
    }
}
