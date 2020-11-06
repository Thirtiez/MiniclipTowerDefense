using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class Positionable : MonoBehaviour
    {
        public Vector2Int Position { get; set; }
        public Bounds Bounds { get { return new Bounds(Position.ToVector3() + size.ToVector3() / 2, size.ToVector3()); } }

        [SerializeField]
        private Vector2Int size = new Vector2Int(2, 2);
        public Vector2Int Size { get { return size; } }
    }
}
