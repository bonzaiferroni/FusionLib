using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FusionLib.Core
{
    public abstract class RecipeFactory
    {
        private readonly Dictionary<Type, GameObject> _prototypes = new Dictionary<Type, GameObject>();
        private readonly Dictionary<Type, Stack<GameObject>> _pools = new Dictionary<Type, Stack<GameObject>>();

        private Transform _prototypesParent;
        private Transform _pooledParent;

        protected abstract Dictionary<Type, Action<Fusion>> Recipes { get; }

        public RecipeFactory()
        {
            _prototypesParent = new GameObject("Prototypes").transform;
            _pooledParent = new GameObject("Pooled").transform;
            CreatePrototypes();
        }

        private void CreatePrototypes()
        {
            foreach (var kvp in Recipes)
            {
                var recipe = kvp.Value;
                var type = kvp.Key;
                var go = Fusion.Create(type.Name, recipe).Go;
                go.transform.SetParent(_prototypesParent);
                _prototypes[type] = go;
                _pools[type] = new Stack<GameObject>();
            }
        }

        public T GetView<T>(Type type, Action<T> init = null) where T : Component
        {
            if (!Recipes.ContainsKey(type)) throw new Exception($"You don't have a recipe for {type.Name}!");
            var pool = _pools[type];
            
            T view;
            if (pool.Count > 0)
            {
                var go = pool.Pop();
                view = go.GetComponent<T>();
            }
            else
            {
                var go = Object.Instantiate(_prototypes[type]);
                view = go.GetComponent<T>();
                init?.Invoke(view);
                var member = go.GetComponent<PoolMember>();
                if (member) member.Init(type, this);
                var initializer = view as IFusionInit;
                initializer?.Init();
            }
            
            return view;
        }
        
        public T GetView<T>(Action<T> init = null) where T : Component
        {
            var type = typeof(T);
            return  GetView(type, init);
        }

        public void Return(Type type, PoolMember poolMember)
        {
            if (!Recipes.ContainsKey(type)) 
                throw new Exception($"Returned poolmember for unregistered type: {type.Name}");
            _pools[type].Push(poolMember.gameObject);
            poolMember.transform.SetParent(_pooledParent);
        }
    }
}