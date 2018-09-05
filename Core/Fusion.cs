using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FusionLib.Core
{
    public class Fusion
    {
        public static Fusion Create(string name, Action<Fusion> addComponents)
        {
            var fuser = new Fusion(name);
            fuser.Add(addComponents);
            return fuser;
        }

        public static Fusion Mount(GameObject go)
        {
            return new Fusion(go);
        }

        public static Fusion Prefab(string path)
        {
            return Mount(Object.Instantiate(LoadResource<GameObject>(path)));
        }

        protected Fusion(GameObject go)
        {
            Go = go;
        }
        
        protected Fusion(string name)
        {
            Go = new GameObject(name);
        }

        public string Name => Go.name;
        public Transform Transform => Go.transform;
        public RectTransform Rect => Go.GetComponent<RectTransform>();
        
        public GameObject Go { get; }

        public Fusion Add(Action<Fusion> addComponents)
        {
            try
            {
                addComponents(this);
            }
            catch (Exception e)
            {
                Debug.Log($"Exception adding components to {Name}:\n{e}");
            }
            
            return this;
        }

        public T Add<T>(Action<T> configure = null) where T : Component
        {
            var comp = Go.AddComponent<T>();
            if (comp == null) throw new Exception($"{Name} couldn't add a component: {typeof(T).Name}");
            configure?.Invoke(comp);
            return comp;
        }

        public T Get<T>(Action<T> configure = null) where T : Component
        {
            var comp = Go.GetComponent<T>();
            if (comp == null) throw new Exception($"{Name} couldn't find a {typeof(T)}");
            configure?.Invoke(comp);
            return comp;
        }

        public T Get<T>(string tag, Action<T> configure = null) where T : Component
        {
            var child = GetChild(tag);
            return child.Get(configure);
        }

        public Fusion GetChild(string tag)
        {
            var stack = new Stack<Transform>();
            stack.Push(Transform);

            while (stack.Count > 0)
            {
                var transform = stack.Pop();
                if (transform.name == tag)
                {
                    return new Fusion(transform.gameObject);
                }
                for (int i = 0; i < transform.childCount; i++)
                {
                    stack.Push(transform.GetChild(i));
                }
            }

            throw new Exception($"{Name} could find child: {tag}");
        }

        public Fusion NewChild(string tag, Action<Fusion> addParts)
        {
            var child = new Fusion(tag);
            child.Transform.SetParent(Transform);
            child.Add(addParts);
            return child;
        }

        public static T LoadResource<T>(string path) where T : UnityEngine.Object
        {
            var resource = Resources.Load<T>(path);
            if (resource == null) throw new Exception($"Couldn't load {typeof(T)} from path: {path}");
            return resource;
        }

        public void AddPrefab(string path)
        {
            var child = Prefab(path);
            child.Transform.SetParent(Transform);
        }
    }
}