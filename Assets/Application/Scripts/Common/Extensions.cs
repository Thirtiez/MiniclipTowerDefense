using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || collection.Any() == false;
        }

        public static Vector3Int ToVector3Int(this Vector2Int vector, bool invertYZ = false)
            => invertYZ ? new Vector3Int(vector.x, 0, vector.y) : new Vector3Int(vector.x, vector.y, 0);

        public static Vector2Int ToVector2Int(this Vector3Int vector, bool invertYZ = false)
             => invertYZ ? new Vector2Int(vector.x, vector.z) : new Vector2Int(vector.x, vector.y);
    }
}

