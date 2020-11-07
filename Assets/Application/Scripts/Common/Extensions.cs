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
            => GetSnappedPosition(grid, position.ToVector3Int(), size);

        public static Vector3 GetSnappedPosition(this Grid grid, Vector3Int position, Vector2Int? size = null)
        {
            var offset = size?.ToVector3Int() ?? new Vector3Int(1, 1, 0);
            var lowerLeft = grid.CellToWorld(position);
            var upperRight = grid.CellToWorld(position + offset);

            return (upperRight + lowerLeft) / 2.0f;
        }

        public static Vector3Int ToVector3Int(this Vector2Int vector, bool invertYZ = false, int missingValue = 0)
            => invertYZ ? new Vector3Int(vector.x, missingValue, vector.y) : new Vector3Int(vector.x, vector.y, missingValue);

        public static Vector2Int ToVector2Int(this Vector3Int vector, bool invertYZ = false)
            => invertYZ ? new Vector2Int(vector.x, vector.z) : new Vector2Int(vector.x, vector.y);

        public static Vector3Int ToVector3Int(this Vector3 vector, bool invertYZ = false)
            => invertYZ ? new Vector3Int((int)vector.x, (int)vector.z, (int)vector.y) : new Vector3Int((int)vector.x, (int)vector.y, (int)vector.z);

        public static Vector3 ToVector3(this Vector3Int vector, bool invertYZ = false)
            => invertYZ ? new Vector3(vector.x, vector.z, vector.y) : new Vector3(vector.x, vector.y, vector.z);

        public static Bounds ToBounds(this BoundsInt bounds)
            => new Bounds(bounds.center, bounds.size.ToVector3());

        public static BoundsInt ToBoundsInt(this Bounds bounds)
            => new BoundsInt(bounds.min.ToVector3Int(), bounds.size.ToVector3Int());

        public static bool Contains(this BoundsInt bounds, BoundsInt target)
            => bounds.Contains(target.min) && bounds.Contains(target.max);

        public static bool Intersects(this BoundsInt bounds, BoundsInt target)
        {
            foreach (var cell in target.allPositionsWithin)
            {
                if (bounds.Contains(cell))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasComponent<T>(this GameObject gameObject)
            => (gameObject.GetComponent<T>() as Component) != null;

        public static bool HasComponent<T>(this Transform transform)
            => (transform.GetComponent<T>() as Component) != null;

        public static Color WithAlpha(this Color color, float alpha)
            => new Color(color.r, color.g, color.b, alpha);
    }
}

