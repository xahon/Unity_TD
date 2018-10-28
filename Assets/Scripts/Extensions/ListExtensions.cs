using System.Collections.Generic;
using UnityEngine;

namespace Extensions {

    public static class ListExtensions {

        public static void Shuffle<T>(this IList<T> list) {
            int n = list.Count;
            while (n > 1) {
                n--;
                int k     = Random.Range(0, n + 1);
                T   value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T RandomElementOrDefault<T>(this IList<T> list, T defaultValue) {
            return list.Count == 0 ? defaultValue : list[Random.Range(0, list.Count - 1)];
        }
    }

    public static class Vector2IntExtensions {
        public static Vector2Int MiddlePosition(this Vector2Int vec1, Vector2Int vec2) {
            return new Vector2Int(vec1.x + (vec2.x - vec1.x) / 2, vec1.y + (vec2.y - vec1.y) / 2);
        }
    }

}