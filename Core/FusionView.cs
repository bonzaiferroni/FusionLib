using System;
using UnityEngine;

namespace FusionLib.Core
{
    public abstract class FusionView : MonoBehaviour
    {
        protected T GetChild<T>(string componentTag) where T : Component
        {
            T comp = null;
            foreach (var component in GetComponentsInChildren<T>())
            {
                if (component.name == componentTag)
                {
                    comp = component;
                    break;
                }
            }
            return Validate(comp);
        }

        protected T GetChild<T>() where T : Component
        {
            return Validate(GetComponentInChildren<T>());
        }

        protected T Get<T>() where T : Component
        {
            return Validate(GetComponent<T>());
        }

        private T Validate<T>(T comp) where T : Component
        {
            if (!comp) throw new Exception($"{GetType().Name} couldn't find a component: {typeof(T)}");
            return comp;
        }
    }
}