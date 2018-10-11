using System;
using UnityEngine;

namespace FusionLib.Core
{
    public class PoolMember : MonoBehaviour
    {
        public Type Type { get; private set; }
        public FactoryReturn ReturnDelegate { get; private set; }
        
        public void Init(Type type, FactoryReturn recipeFactory)
        {
            Type = type;
            ReturnDelegate = recipeFactory;
        }

        public void Return()
        {
            ReturnDelegate(Type, this);
        }
    }

    public delegate void FactoryReturn(Type type, PoolMember member);
}