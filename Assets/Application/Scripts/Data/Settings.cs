using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    [CreateAssetMenu(menuName = "Data/Settings")]
    public class Settings : ScriptableObject
    {
        [Header("Colors")]
        [SerializeField]
        private Color lightColor;
        public Color LightColor { get { return lightColor; } }

        [SerializeField]
        private Color darkColor;
        public Color DarkColor { get { return darkColor; } }

        [SerializeField]
        private Color primaryColor;
        public Color PrimaryColor { get { return primaryColor; } }

        [SerializeField]
        private Color secondaryColor;
        public Color SecondaryColor { get { return secondaryColor; } }

        [SerializeField]
        private Color tertiaryColor;
        public Color TertiaryColor { get { return tertiaryColor; } }

        [Header("Parameters")]
        [SerializeField]
        private int startingMoney;
        public int StartingMoney { get { return startingMoney; } }

        [SerializeField]
        private Vector2Int gridDimensions;
        public Vector2Int GridDimensions { get { return gridDimensions; } }
        public BoundsInt GridBounds => new BoundsInt(MinGridDimensions.ToVector3Int(true), gridDimensions.ToVector3Int(true) + Vector3Int.one);
        public Vector2Int MinGridDimensions => new Vector2Int(-gridDimensions.x / 2, -gridDimensions.y / 2);
        public Vector2Int MaxGridDimensions => new Vector2Int(gridDimensions.x / 2, gridDimensions.y / 2);

        [SerializeField]
        private int enemiesToSpawn;
        public int EnemiesToSpawn { get { return enemiesToSpawn; } }
    }
}
