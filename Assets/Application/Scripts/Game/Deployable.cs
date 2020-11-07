using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    [RequireComponent(typeof(Positionable))]
    public class Deployable : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        private Sprite icon;
        public Sprite Icon { get { return icon; } }

        [Header("Parameters")]
        [SerializeField]
        private int cost = 10;
        public int Cost { get { return cost; } }

        [Header("References")]
        [SerializeField]
        private Material ghostMaterial;
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
            positionable = GetComponent<Positionable>();
            shooter = GetComponent<Shooter>();
            explosive = GetComponent<Explosive>();
        }

        public void SetPositioning()
        {
            ghostObject.SetActive(true);
            normalObject.SetActive(false);

            sizeVisualizer = Instantiate(ApplicationController.Instance.Prefabs.SizeVisualizer, transform);
            sizeVisualizer.transform.localScale = new Vector3(positionable.SizeVector.x, positionable.SizeVector.y, 1);

            float range = shooter?.FireRange ?? explosive?.ExplosionRadius ?? 0;
            radiusVisualizer = Instantiate(ApplicationController.Instance.Prefabs.RadiusVisualizer, transform);
            radiusVisualizer.transform.localScale = new Vector3(range * 2f, range * 2f, 1);
        }

        public void SetValid(bool isValid)
        {
            var color = (isValid ? ApplicationController.Instance.Settings.PrimaryColor : ApplicationController.Instance.Settings.SecondaryColor).WithAlpha(0.5f);
            sizeVisualizer.color = color;
            ghostMaterial.color = color;

            radiusVisualizer.gameObject.SetActive(isValid);
        }

        public void SetReady()
        {
            ghostObject.SetActive(false);
            normalObject.SetActive(true);

            Destroy(sizeVisualizer.gameObject);
            Destroy(radiusVisualizer.gameObject);
            sizeVisualizer = null;
            radiusVisualizer = null;
        }
    }
}
