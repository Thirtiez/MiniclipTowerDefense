using Lean.Touch;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class CameraControls : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField]
        private float panSensitivity = 0.5f;
        [SerializeField]
        private float zoomSensitivity = 0.2f;

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
    }
}
