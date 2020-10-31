using Lean.Touch;
using System.Collections;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    [RequireComponent(typeof(Positionable))]
    [RequireComponent(typeof(BoxCollider))]
    public class Deployable : MonoBehaviour
    {
        [SerializeField]
        private Sprite icon;
        public Sprite Icon { get { return icon; } }

        [SerializeField]
        private int cost = 10;
        public int Cost { get { return cost; } }

        private Coroutine positioningRoutine;

        protected void Awake()
        {
            var collider = GetComponent<BoxCollider>();
            var positionable = GetComponent<Positionable>();

            collider.size = new Vector3(positionable.Size.x, 1, positionable.Size.y);
            collider.center = new Vector3(0, 0.5f, 0);
        }
    }
}
