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

        [SerializeField]
        private GameObject ghostObject;
        [SerializeField]
        private GameObject normalObject;

        private Positionable positionable;
        private Shooter shooter;
        private Explosive explosive;

        private SpriteRenderer sizeVisualizer;
        private SpriteRenderer radiusVisualizer;

        protected void Awake()
        {
            var collider = GetComponent<BoxCollider>();
            positionable = GetComponent<Positionable>();
            shooter = GetComponent<Shooter>();
            explosive = GetComponent<Explosive>();

            collider.size = new Vector3(positionable.Size.x, 1, positionable.Size.y);
            collider.center = new Vector3(0, 0.5f, 0);
        }

        public void SetPositioning()
        {
            ghostObject.SetActive(true);
            normalObject.SetActive(false);

            sizeVisualizer = Instantiate(ApplicationController.Instance.Prefabs.SizeVisualizer, transform);
            sizeVisualizer.transform.localScale = new Vector3(positionable.Size.x, positionable.Size.y, 1);

            float range = shooter?.FireRange ?? explosive?.ExplosionRadius ?? 0;
            radiusVisualizer = Instantiate(ApplicationController.Instance.Prefabs.RadiusVisualizer, transform);
            radiusVisualizer.transform.localScale = new Vector3(range * 2f, range * 2f, 1);
        }

        public void SetReady()
        {
            ghostObject.SetActive(false);
            normalObject.SetActive(true);

            Destroy(sizeVisualizer.gameObject);
            sizeVisualizer = null;

            Destroy(radiusVisualizer.gameObject);
            radiusVisualizer = null;
        }
    }
}
