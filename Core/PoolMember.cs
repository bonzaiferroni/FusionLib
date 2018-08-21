using System;
using UnityEngine;

namespace FusionLib.Core
{
    public class PoolMember : MonoBehaviour
    {
        public Type Type { get; private set; }
        public RecipeFactory Factory { get; private set; }
        
        public void Init(Type type, RecipeFactory recipeFactory)
        {
            Type = type;
            Factory = recipeFactory;
        }

        public void Return()
        {
            Factory.Return(Type, this);
        }

    }
}