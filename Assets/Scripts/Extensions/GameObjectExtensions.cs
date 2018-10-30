﻿using UnityEngine;

namespace Extensions {

    public static class GameObjectExtensions {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component {
            T component = gameObject.GetComponent<T>();

            return component == null ? gameObject.AddComponent<T>() : component;
        }
    }

}