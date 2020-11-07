using UnityEngine;
using UnityEngine.AI;

namespace Thirties.Miniclip.TowerDefense
{
    [RequireComponent(typeof(NavMeshObstacle))]
    public class Positionable : MonoBehaviour
    {
        public Vector2Int Position { get; set; }
        public Vector2Int SizeVector { get { return new Vector2Int(size, size); } }
        public BoundsInt Bounds { get { return new BoundsInt(Position.ToVector3Int(true), SizeVector.ToVector3Int(true, 1)); } }

        [SerializeField]
        private int size = 2;
        public int Size { get { return size; } }

        protected void OnEnable()
        {
            var obstacle = GetComponent<NavMeshObstacle>();
            obstacle.shape = NavMeshObstacleShape.Capsule;
            obstacle.radius = size * 0.5f;
            obstacle.height = size;
        }
    }
}
