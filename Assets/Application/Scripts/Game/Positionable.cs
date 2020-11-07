using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class Positionable : MonoBehaviour
    {
        public Vector2Int Position { get; set; }
        public BoundsInt Bounds { get { return new BoundsInt(Position.ToVector3Int(true), size.ToVector3Int(true, 1)); } }

        [SerializeField]
        private Vector2Int size = new Vector2Int(2, 2);
        public Vector2Int Size { get { return size; } }
    }
}
