using Lean.Touch;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class CameraControls : MonoBehaviour
    {
        [Header("Pan")]
        [SerializeField]
        private float panSensitivity = 0.2f;

        [Header("Zoom")]
        [SerializeField]
        private float zoomSensitivity = 0.005f;
        [SerializeField]
        private float minZoomValue = 3.0f;
        [SerializeField]
        private float maxZoomValue = 12.0f;

        protected void Start()
        {
            LeanTouch.OnFingerUpdate += OnFingerUpdate;
        }

        private void OnFingerUpdate(LeanFinger finger)
        {
            if (LeanTouch.Fingers.Count == 1 && finger.TapCount >= 1)
            {
                HandleCameraPan();
            }
            else if (LeanTouch.Fingers.Count == 2)
            {
                HandleCameraZoom();
            }
        }

        private void HandleCameraPan()
        {
            var lastScreenPoint = LeanGesture.GetLastScreenCenter();
            var screenPoint = LeanGesture.GetScreenCenter();

            var lastWorldPoint = Camera.main.ScreenToWorldPoint(lastScreenPoint);
            var worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);

            var worldDelta = worldPoint - lastWorldPoint;

            Camera.main.transform.position += worldDelta * panSensitivity;
        }

        private void HandleCameraZoom()
        {
            float lastScreenDistance = LeanGesture.GetLastScreenDistance();
            float screenDistance = LeanGesture.GetScreenDistance();

            float delta = screenDistance - lastScreenDistance;

            Camera.main.orthographicSize += delta * zoomSensitivity;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoomValue, maxZoomValue);
        }
    }
}
