using System;
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

        public static Vector3 GetSnappedPosition(this Grid grid, Vector3 position, Vector2Int? size = null)
        {
            var cellPosition = grid.WorldToCell(new Vector3(position.x, 0, position.z));

            return GetSnappedPosition(grid, cellPosition, size);
        }

        public static Vector3 GetSnappedPosition(this Grid grid, Vector2Int position, Vector2Int? size = null)
        {
            return GetSnappedPosition(grid, position.ToVector3Int(), size);
        }

        public static Vector3 GetSnappedPosition(this Grid grid, Vector3Int position, Vector2Int? size = null)
        {
            var offset = size?.ToVector3Int() ?? new Vector3Int(1, 1, 0);
            var lowerLeft = grid.CellToWorld(position);
            var upperRight = grid.CellToWorld(position + offset);

            return (upperRight + lowerLeft) / 2.0f;
        }

        public static Vector3Int ToVector3Int(this Vector2Int vector, bool invertYZ = false)
            => invertYZ ? new Vector3Int(vector.x, 0, vector.y) : new Vector3Int(vector.x, vector.y, 0);

        public static Vector3Int ToVector3Int(this Vector3 vector, bool invertYZ = false)
            => invertYZ ? new Vector3Int((int)vector.x, (int)vector.z, (int)vector.y) : new Vector3Int((int)vector.x, (int)vector.y, (int)vector.z);

        public static Vector3 ToVector3(this Vector3Int vector, bool invertYZ = false)
            => invertYZ ? new Vector3(vector.x, 0, vector.y) : new Vector3(vector.x, vector.y, 0);

        public static Vector3 ToVector3(this Vector2Int vector, bool invertYZ = false)
            => invertYZ ? new Vector3(vector.x, 0, vector.y) : new Vector3(vector.x, vector.y, 0);

        public static Vector2Int ToVector2Int(this Vector3Int vector, bool invertYZ = false)
            => invertYZ ? new Vector2Int(vector.x, vector.z) : new Vector2Int(vector.x, vector.y);

        public static Vector2Int ToVector2Int(this Vector2 vector)
            => new Vector2Int((int)vector.x, (int)vector.y);

        public static Vector2 ToVector2(this Vector2Int vector)
             => new Vector2(vector.x, vector.y);

        public static Vector2 ToVector2(this Vector3 vector, bool invertYZ = false)
            => invertYZ ? new Vector2(vector.x, vector.z) : new Vector2(vector.x, vector.y);

        public static bool HasComponent<T>(this GameObject gameObject)
        {
            return (gameObject.GetComponent<T>() as Component) != null;
        }

        public static bool HasComponent<T>(this Transform transform)
        {
            return (transform.GetComponent<T>() as Component) != null;
        }
    }
}

