using UnityEngine;
using System.Collections.Generic;

namespace Container.Framework.Extensions
{
    public interface IGameObjectFactory
    {
        GameObject Create(string name);
        GameObject Create(GameObject original);
    }

    public class GameObjectFactory : IGameObjectFactory
    {
        private readonly Dictionary<string, GameObject> resourceCache;
        private readonly IBinder binder;

        public GameObjectFactory(IBinder binder)
        {
            this.binder = binder;
            this.resourceCache = new Dictionary<string, GameObject>();
        }

        public GameObject Create(string name)
        {
            var resource = GetResource(name);
            return Create(resource);
        }

        public GameObject Create(GameObject original)
        {
            var instance = Object.Instantiate(original);

            foreach (var script in instance.GetComponents<MonoBehaviour>())
            {
                binder.InjectProperties(script);
            }

            return instance;
        }

        private GameObject GetResource(string name)
        {
            if (!resourceCache.ContainsKey(name))
            {
                resourceCache[name] = (GameObject) Resources.Load(name);
            }

            return resourceCache[name];
        }
    }
}