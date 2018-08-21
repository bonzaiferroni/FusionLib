using System;
using System.Collections.Generic;
using UnityEngine;

namespace FusionLib.Core
{
    public abstract class RecipeFactory
    {
        protected abstract Dictionary<Type, Action<Fusion>> Recipes { get; }

        public T GetView<T>(Type type, Action<T> init) where T : Component
        {
            if (!Recipes.ContainsKey(type)) throw new Exception($"You don't have a recipe for {type.Name}!");
            var recipe = Recipes[type];
            var fusion = Fusion.Create(type.Name, recipe);
            var view = fusion.Get<T>();
            init(view);
            return view;
        }
        
        public T GetView<T>(Action<T> init) where T : Component
        {
            var type = typeof(T);
            return  GetView(type, init);
        }
    }
}