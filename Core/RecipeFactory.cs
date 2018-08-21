using System;
using System.Collections.Generic;

namespace FusionLib.Core
{
    public abstract class RecipeFactory<U>
    {
        protected abstract Dictionary<Type, Action<Fusion>> Recipes { get; }

        protected abstract void Init(FusionView<U> view, U bundle);

        public Fusion GetView(Type type)
        {
            if (!Recipes.ContainsKey(type)) throw new Exception($"You don't have a recipe for {type.Name}!");
            var recipe = Recipes[type];
            return Fusion.Create(type.Name, recipe);
        }
        
        public T GetView<T>(U bundle) where T : FusionView<U>
        {
            var type = typeof(T);
            var fusion = GetView(type);
            
            var view = fusion.Get<T>();
            BaseInit(view, bundle);
            Init(view, bundle);
            return view;
        }
        
        protected void BaseInit<T>(T view, U bundle) where T : FusionView<U>
        {
            foreach (var fusionView in view.GetComponentsInChildren<FusionView<U>>())
            {
                fusionView.Init(bundle);
            }
        }
    }
}