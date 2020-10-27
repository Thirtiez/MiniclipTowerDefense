using Lean.Touch;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class GameView : BaseView
    {
        #region Inspector fields

        [Header("Input")]
        [SerializeField]
        private float panSensitivity = 0.05f;
        [SerializeField]
        private float zoomSensitivity = 0.2f;

        #endregion

        #region Private fields

        #endregion

        #region Public fields


        #endregion

        #region Events

        #endregion

        #region Private methods

        protected override void Initialize()
        {
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
                var delta = screenPoint - lastScreenPoint;

                Camera.main.transform.position += new Vector3(delta.x, 0, delta.y) * panSensitivity;
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
