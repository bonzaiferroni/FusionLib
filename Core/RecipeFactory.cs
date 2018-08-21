using System;
using System.Collections.Generic;
using UnityEngine;

namespace FusionLib.Core
{
    public abstract class RecipeFactory
    {
        protected abstract Dictionary<Type, Action<Fusion>> Recipes { get; }

        public Fusion GetView(Type type)
        {
            if (!Recipes.ContainsKey(type)) throw new Exception($"You don't have a recipe for {type.Name}!");
            var recipe = Recipes[type];
            return Fusion.Create(type.Name, recipe);
        }
        
        public T GetView<T>() where T : Component
        {
            var type = typeof(T);
            var fusion = GetView(type);
            var view = fusion.Get<T>();
            return view;
        }
    }
}