using Lean.Touch;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class GameView : BaseView
    {
        #region Inspector fields

        [Header("Camera")]
        [SerializeField]
        private float panSensitivity = 0.5f;
        [SerializeField]
        private float zoomSensitivity = 0.2f;

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

        #region Public fields


        #endregion

        #region Events

        #endregion

        #region Private methods

        protected override void Initialize()
        {
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
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected void Update()
        {
            HandleCameraPan();
            HandleCameraZoom();
        }

        protected void HandleCameraPan()
        {
            if (LeanTouch.Fingers.Count == 2)
            {
                var lastScreenPoint = LeanGesture.GetLastScreenCenter();
                var screenPoint = LeanGesture.GetScreenCenter();

                var lastWorldPoint = Camera.main.ScreenToWorldPoint(lastScreenPoint);
                var worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);

                var delta = worldPoint - lastWorldPoint;

                Camera.main.transform.position += delta * panSensitivity;
            }
        }

        protected void HandleCameraZoom()
        {
            if (LeanTouch.Fingers.Count == 2)
            {
                float lastScreenDistance = LeanGesture.GetLastScreenDistance();
                float screenDistance = LeanGesture.GetScreenDistance();
                float delta = screenDistance - lastScreenDistance;

                Camera.main.orthographicSize += delta * zoomSensitivity;
            }
        }

        #endregion
    }
}
