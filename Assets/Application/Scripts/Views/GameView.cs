using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Thirties.Miniclip.TowerDefense
{
    public class GameView : BaseView
    {
        #region Events

        private UnityAction FightButtonPressed { get; set; }
        private UnityAction GiveUpButtonPressed { get; set; }
        private UnityAction<Deployable> DeployableButtonPressed { get; set; }

        #endregion

        #region Inspector fields

        [Header("Deployables")]
        [SerializeField]
        private Transform deployableContainer;
        [SerializeField]
        private DeployableUIElement deployableUIElementPrefab;

        [Header("Buttons")]
        [SerializeField]
        private Button fightButton;
        [SerializeField]
        private Button giveUpButton;

        [Header("Grid")]
        [SerializeField]
        private Grid grid;
        [SerializeField]
        private LineRenderer gridLine;
        [SerializeField]
        private Vector2Int gridDimensions = new Vector2Int(10, 10);

        #endregion

        #region Private fields

        private Vector2Int minGridDimensions => new Vector2Int(-gridDimensions.x / 2, -gridDimensions.y / 2);
        private Vector2Int maxGridDimensions => new Vector2Int(gridDimensions.x / 2, gridDimensions.y / 2);

        #endregion

        #region Protected methods

        protected override void Initialize()
        {
            // Grid
            for (int i = minGridDimensions.x; i <= maxGridDimensions.x; i++)
            {
                var startPoint = grid.CellToWorld(new Vector3Int(i, minGridDimensions.y, 0));
                var endPoint = grid.CellToWorld(new Vector3Int(i, maxGridDimensions.y, 0));

                var line = Instantiate(gridLine, grid.transform);
                line.SetPosition(0, startPoint);
                line.SetPosition(1, endPoint);
            }
            for (int j = minGridDimensions.y; j <= maxGridDimensions.y; j++)
            {
                var startPoint = grid.CellToWorld(new Vector3Int(minGridDimensions.x, j, 0));
                var endPoint = grid.CellToWorld(new Vector3Int(maxGridDimensions.y, j, 0));

                var line = Instantiate(gridLine, grid.transform);
                line.SetPosition(0, startPoint);
                line.SetPosition(1, endPoint);
            }

            // Listeners
            fightButton.onClick.AddListener(FightButtonPressed);
            giveUpButton.onClick.AddListener(GiveUpButtonPressed);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        #endregion

        #region Private methods

        #endregion
    }
}
