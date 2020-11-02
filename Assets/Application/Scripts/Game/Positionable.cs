using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class Positionable : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int size = new Vector2Int(2, 2);
        public Vector2Int Size { get { return size; } }
    }
}
