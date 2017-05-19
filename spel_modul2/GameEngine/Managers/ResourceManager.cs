using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace GameEngine.Managers
{
    public class ResourceManager
    {
        static ResourceManager Instance;
        Dictionary<string, object> resources;
        public ContentManager Content;

        static ResourceManager()
        {
            Instance = new ResourceManager();
        }

        public static ResourceManager GetInstance()
        {
            return Instance;
        }

        private ResourceManager()
        {
            resources = new Dictionary<string, object>();
            Content = null;
        }

        public T GetResource<T>(string name)
        {
            object resource;
            if (!Instance.resources.TryGetValue(name, out resource))
            {
                resource = Content.Load<T>(name);
                AddResource(name, resource);
            }
            return (T)resource;
        }

        public void AddResource(string name, object resource)
        {
            Instance.resources.Add(name, resource);
        }
    }
}