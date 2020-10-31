using Lean.Touch;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Thirties.Miniclip.TowerDefense
{
    public class GameView : BaseView
    {
        #region Events

        public UnityAction FightButtonPressed { get; set; }
        public UnityAction GiveUpButtonPressed { get; set; }
        public UnityAction DeployableButtonPressed { get; set; }
        public UnityAction<Deployable> PositioningConfirmButtonPressed { get; set; }
        public UnityAction PositioningCancelButtonPressed { get; set; }

        #endregion

        #region Inspector fields

        [Header("UI")]
        [SerializeField]
        private Canvas canvas;
        [SerializeField]
        private TMP_Text moneyText;
        [SerializeField]
        private Transform deployableContainer;
        [SerializeField]
        private DeployableButton deployableButtonPrefab;
        [SerializeField]
        private PositioningButtons positioningButtonsPrefab;

        [Header("Buttons")]
        [SerializeField]
        private Button fightButton;
        [SerializeField]
        private Button giveUpButton;

        [Header("Grid")]
        [SerializeField]
        private Grid grid;
        [SerializeField]
        private Vector2Int gridDimensions = new Vector2Int(10, 10);
        [SerializeField]
        private Transform positionableContainer;
        [SerializeField]
        private Positionable headquartersPrefab;
        [SerializeField]
        private Transform gridLineContainer;
        [SerializeField]
        private LineRenderer gridLinePrefab;


        #endregion

        #region Private fields

        private Vector2Int minGridDimensions => new Vector2Int(-gridDimensions.x / 2, -gridDimensions.y / 2);
        private Vector2Int maxGridDimensions => new Vector2Int(gridDimensions.x / 2, gridDimensions.y / 2);

        private List<LineRenderer> gridLines = new List<LineRenderer>();
        private List<DeployableButton> deployableButtons = new List<DeployableButton>();
        private List<Deployable> deployables = new List<Deployable>();

        private Deployable currentDeployable;

        #endregion

        #region Public methods

        public void StartPlanning()
        {
            deployableButtons.ForEach(deployableButton => deployableButton.Interactable = deployableButton.IsPurchasable);

            deployableContainer.gameObject.SetActive(true);
            fightButton.gameObject.SetActive(true);
            giveUpButton.gameObject.SetActive(false);

            moneyText.text = applicationController.CurrentMoney.ToString();
        }

        public void StartPositioning()
        {
            deployableButtons.ForEach(deployableButton => deployableButton.Interactable = false);

            deployableContainer.gameObject.SetActive(true);
            fightButton.gameObject.SetActive(false);
            giveUpButton.gameObject.SetActive(false);

            var positionable = currentDeployable.GetComponent<Positionable>();
            var position = positionable.GetSnappedPosition(grid, Vector3Int.zero);
            currentDeployable = Instantiate(currentDeployable, position, Quaternion.identity, positionableContainer);

            var positioningButtons = Instantiate(positioningButtonsPrefab, canvas.transform);
            positioningButtons.Initialize(currentDeployable.transform, () =>
            {
                Debug.Log($"{currentDeployable.gameObject.name} succesfully deployed.");

                LeanTouch.OnFingerUpdate -= HandlePositioning;

                Destroy(positioningButtons.gameObject);

                deployables.Add(currentDeployable);

                PositioningConfirmButtonPressed?.Invoke(currentDeployable);
            }, () =>
            {
                Debug.Log($"{currentDeployable.gameObject.name} deployment canceled.");

                LeanTouch.OnFingerUpdate -= HandlePositioning;

                Destroy(positioningButtons.gameObject);
                Destroy(currentDeployable.gameObject);

                PositioningCancelButtonPressed?.Invoke();
            });

            LeanTouch.OnFingerUpdate += HandlePositioning;
        }

        public void StartFighting()
        {
            deployableContainer.gameObject.SetActive(false);
            fightButton.gameObject.SetActive(false);
            giveUpButton.gameObject.SetActive(true);
        }

        public void StartResolution()
        {
            deployableContainer.gameObject.SetActive(false);
            fightButton.gameObject.SetActive(false);
            giveUpButton.gameObject.SetActive(false);
        }

        #endregion

        #region Protected methods

        protected override void Initialize()
        {
            // UI
            deployableButtons = applicationController.DeployableData.Deployables.Select(deployable =>
            {
                var deployableButton = Instantiate(deployableButtonPrefab, deployableContainer);
                deployableButton.Initialize(deployable, OnDeployableButtonPressed);

                return deployableButton;
            }).ToList();

            // Grid lines
            gridLines = new List<LineRenderer>();
            for (int i = minGridDimensions.x; i <= maxGridDimensions.x; i++)
            {
                var startPoint = grid.CellToWorld(new Vector3Int(i, minGridDimensions.y, 0));
                var endPoint = grid.CellToWorld(new Vector3Int(i, maxGridDimensions.y, 0));

                var line = Instantiate(gridLinePrefab, gridLineContainer);
                line.SetPosition(0, startPoint);
                line.SetPosition(1, endPoint);

                gridLines.Add(line);
            }
            for (int j = minGridDimensions.y; j <= maxGridDimensions.y; j++)
            {
                var startPoint = grid.CellToWorld(new Vector3Int(minGridDimensions.x, j, 0));
                var endPoint = grid.CellToWorld(new Vector3Int(maxGridDimensions.y, j, 0));

                var line = Instantiate(gridLinePrefab, gridLineContainer);
                line.SetPosition(0, startPoint);
                line.SetPosition(1, endPoint);

                gridLines.Add(line);
            }

            // Headquarters
            var position = headquartersPrefab.GetSnappedPosition(grid, Vector3Int.zero);
            Instantiate(headquartersPrefab, position, Quaternion.identity, positionableContainer);

            // Listeners
            fightButton.onClick.AddListener(() => FightButtonPressed?.Invoke());
        }

        #endregion

        #region Private methods

        private void HandlePositioning(LeanFinger finger)
        {
            if (!finger.IsOverGui)
            {
                var ray = finger.GetRay(Camera.main);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Layers.Floor))
                {
                    var positionable = currentDeployable.GetComponent<Positionable>();
                    var snappedPosition = positionable.GetSnappedPosition(grid, hit.point);

                    currentDeployable.transform.position = snappedPosition;
                }
            }
        }

        public void OnDeployableButtonPressed(Deployable deployable)
        {
            currentDeployable = deployable;

            DeployableButtonPressed?.Invoke();
        }

        #endregion
    }
}
