using System;
using UnityEngine;

namespace FusionLib.Core
{
    [ExecuteInEditMode]
    public abstract class CookView : MonoBehaviour
    {
        protected abstract Action<Fusion> Recipe { get; }

        private void OnEnable()
        {
            var fusion = Fusion.Mount(gameObject);
            fusion.Add(Recipe);
            DestroyImmediate(this);
        }
    }
}