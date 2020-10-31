using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class Positionable : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int size = new Vector2Int(2, 2);
        public Vector2Int Size { get { return size; } }

        public Vector3 GetSnappedPosition(Grid grid, Vector3 position)
        {
            var cellPosition = grid.WorldToCell(new Vector3(position.x, 0, position.z));
            
            return GetSnappedPosition(grid, cellPosition);
        }

        public Vector3 GetSnappedPosition(Grid grid, Vector3Int position)
        {
            var lowerLeft = grid.CellToWorld(position);
            var upperRight = grid.CellToWorld(position + size.ToVector3Int());

            return (upperRight + lowerLeft) / 2.0f;
        }
    }
}
