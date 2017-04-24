using System;
using System.Collections.Generic;

namespace spel_modul2.Entity
{
    public class Entity
    {
        private Dictionary<Type, object> components;
        public string Name { get; set; }

        public Entity()
        {
            components = new Dictionary<Type, object>();
        }
       
        public Dictionary<Type, object> GetComponents
        {
            get { return components; }
        }

        //adds a component to the entity
        public void AddComponent(object component)
        {
            if (component == null)
                throw new NullReferenceException("Component cannot be null.");
            components.Add(component.GetType(), component);
        }

        //remove the given component from entity
        public void Remove<T>()
        {
            Remove(typeof(T));
        }

        //remove the given component from entity
        public void Remove(Type componentClass)
        {
            if (components.ContainsKey(componentClass))
            {
                var comp = components[componentClass];
                components.Remove(componentClass);
            }
        }

        //returns a component from the entity
        public object Get(Type componentClass)
        {
            return components.ContainsKey(componentClass) ? components[componentClass] : null;
        }

        //returns a component from the entity
        public T Get<T>(Type componentClass)
        {
            return components.ContainsKey(componentClass) ? (T)components[componentClass] : default(T);
        }

        //boolean check if entity contains componentClass
        public bool Has(Type componentClass)
        {
            return components.ContainsKey(componentClass);
        }
    }
}
