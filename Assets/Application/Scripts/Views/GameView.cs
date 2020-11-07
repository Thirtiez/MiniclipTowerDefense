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
        public UnityAction DoubleTimeButtonPressed { get; set; }
        public UnityAction NormalTimeButtonPressed { get; set; }
        public UnityAction DeployableButtonPressed { get; set; }
        public UnityAction<Deployable> PositioningConfirmButtonPressed { get; set; }
        public UnityAction PositioningCancelButtonPressed { get; set; }
        public UnityAction HeadquartersDestroyed { get; set; }
        public UnityAction AllEnemiesDefeated { get; set; }
        public UnityAction RestartButtonPressed { get; set; }
        public UnityAction ExitButtonPressed { get; set; }

        #endregion

        #region Inspector fields

        [Header("UI")]
        [SerializeField]
        private Canvas canvas;
        [SerializeField]
        private ResolutionModal resolutionModal;
        [SerializeField]
        private TMP_Text moneyText;
        [SerializeField]
        private Transform deployableContainer;

        [Header("Buttons")]
        [SerializeField]
        private Button fightButton;
        [SerializeField]
        private Button giveUpButton;
        [SerializeField]
        private Button doubleTimeButton;
        [SerializeField]
        private Button normalTimeButton;

        [Header("Grid")]
        [SerializeField]
        private Grid grid;
        [SerializeField]
        private Transform positionableContainer;
        [SerializeField]
        private Transform gridLineContainer;
        [SerializeField]
        private Transform internalFloor;

        #endregion

        #region Private fields

        private List<LineRenderer> gridLines = new List<LineRenderer>();
        private List<Vector2Int> spawnCells = new List<Vector2Int>();
        private List<DeployableButton> deployableButtons = new List<DeployableButton>();
        private List<Positionable> positionables = new List<Positionable>();
        private List<AIControlled> enemies = new List<AIControlled>();

        private Deployable currentDeployable;
        private PositioningButtons currentPositioningButtons;
        private bool isVictory = false;

        #endregion

        #region Public methods

        public void StartPlanning()
        {
            // Refresh UI
            deployableButtons.ForEach(deployableButton => deployableButton.Interactable = deployableButton.IsPurchasable);

            deployableContainer.gameObject.SetActive(true);
            fightButton.gameObject.SetActive(true);
            giveUpButton.gameObject.SetActive(false);
            doubleTimeButton.gameObject.SetActive(true);
            normalTimeButton.gameObject.SetActive(false);

            doubleTimeButton.interactable = false;

            moneyText.text = applicationController.CurrentMoney.ToString();
        }

        public void StartPositioning()
        {
            // Refresh UI
            deployableContainer.gameObject.SetActive(true);
            fightButton.gameObject.SetActive(false);
            giveUpButton.gameObject.SetActive(false);
            doubleTimeButton.gameObject.SetActive(true);
            normalTimeButton.gameObject.SetActive(false);

            deployableButtons.ForEach(deployableButton => deployableButton.Interactable = false);
            doubleTimeButton.interactable = false;

            // Instantiate the deployable
            var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            var position = Vector3.zero;
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Layer.Floor))
            {
                position = hit.point;
            }
            currentDeployable = Instantiate(currentDeployable, position, Quaternion.identity, positionableContainer);
            currentDeployable.SetPositioning();

            // Instantiate the confirm/cancel positioning buttons
            currentPositioningButtons = Instantiate(applicationController.Prefabs.PositioningButtons, canvas.transform);
            currentPositioningButtons.Initialize(currentDeployable.transform, () =>
            {
                Debug.Log($"{currentDeployable.gameObject.name} succesfully deployed.");

                LeanTouch.OnFingerUpdate -= OnFingerUpdate;

                Destroy(currentPositioningButtons.gameObject);
                currentPositioningButtons = null;

                currentDeployable.SetReady();

                positionables.Add(currentDeployable.GetComponent<Positionable>());

                PositioningConfirmButtonPressed?.Invoke(currentDeployable);
            }, () =>
            {
                Debug.Log($"{currentDeployable.gameObject.name} deployment canceled.");

                LeanTouch.OnFingerUpdate -= OnFingerUpdate;

                Destroy(currentPositioningButtons.gameObject);
                Destroy(currentDeployable.gameObject);
                currentPositioningButtons = null;
                currentDeployable = null;

                PositioningCancelButtonPressed?.Invoke();
            });

            UpdatePositioning(position);

            // Input
            LeanTouch.OnFingerUpdate += OnFingerUpdate;
        }

        public void StartFighting()
        {
            // Refresh UI
            deployableContainer.gameObject.SetActive(false);
            fightButton.gameObject.SetActive(false);
            giveUpButton.gameObject.SetActive(true);
            doubleTimeButton.gameObject.SetActive(true);
            normalTimeButton.gameObject.SetActive(false);

            doubleTimeButton.interactable = true;

            // Grid
            gridLines.ForEach(x => Destroy(x.gameObject));

            // Spawn enemies
            foreach (var enemy in applicationController.Prefabs.Enemies)
            {
                SpawnEnemy(enemy, applicationController.Settings.EnemiesToSpawn);
            }

            // Start shooting
            positionables.ForEach(x => x.GetComponent<Shooter>()?.StartLookingForTarget());

            // Input
            LeanTouch.OnFingerTap += OnFingerTap;
        }

        public void StartResolution()
        {
            // Refresh UI
            deployableContainer.gameObject.SetActive(false);
            fightButton.gameObject.SetActive(false);
            giveUpButton.gameObject.SetActive(true);

            resolutionModal.Show(
                isVictory,
                () => ExitButtonPressed?.Invoke(),
                () => RestartButtonPressed?.Invoke());
        }

        #endregion

        #region Protected methods

        protected override void Initialize()
        {
            // Setup deployable buttons
            deployableButtons = applicationController.Prefabs.Towers.Select(deployable =>
            {
                var deployableButton = Instantiate(applicationController.Prefabs.DeployableButton, deployableContainer);
                deployableButton.Initialize(deployable, OnDeployableButtonPressed);

                return deployableButton;
            }).ToList();

            // Setup grid and spawn cells
            var minGridDimensions = applicationController.Settings.MinGridDimensions;
            var maxGridDimensions = applicationController.Settings.MaxGridDimensions;

            gridLines = new List<LineRenderer>();
            for (int i = minGridDimensions.x; i <= maxGridDimensions.x; i++)
            {
                var startPoint = grid.CellToWorld(new Vector3Int(i, minGridDimensions.y, 0));
                var endPoint = grid.CellToWorld(new Vector3Int(i, maxGridDimensions.y, 0));

                var line = Instantiate(applicationController.Prefabs.GridLine, gridLineContainer);
                line.SetPosition(0, startPoint);
                line.SetPosition(1, endPoint);

                gridLines.Add(line);

                if (i != maxGridDimensions.x)
                {
                    spawnCells.Add(new Vector2Int(i, minGridDimensions.y - 1));
                    spawnCells.Add(new Vector2Int(i, maxGridDimensions.y));
                }
            }
            for (int j = minGridDimensions.y; j <= maxGridDimensions.y; j++)
            {
                var startPoint = grid.CellToWorld(new Vector3Int(minGridDimensions.x, j, 0));
                var endPoint = grid.CellToWorld(new Vector3Int(maxGridDimensions.y, j, 0));

                var line = Instantiate(applicationController.Prefabs.GridLine, gridLineContainer);
                line.SetPosition(0, startPoint);
                line.SetPosition(1, endPoint);

                gridLines.Add(line);

                if (j != maxGridDimensions.y)
                {
                    spawnCells.Add(new Vector2Int(minGridDimensions.x - 1, j));
                    spawnCells.Add(new Vector2Int(maxGridDimensions.x, j));
                }
            }

            // Resize floor accordingly
            var gridDimensions = applicationController.Settings.GridDimensions;
            internalFloor.localScale = new Vector3(gridDimensions.x / 10f, 1, gridDimensions.y / 10f);

            // Instantiate headquarters
            var headquartersPosition = grid.GetSnappedPosition(Vector3Int.zero, applicationController.Prefabs.Headquarters.SizeVector);
            var headquarters = Instantiate(applicationController.Prefabs.Headquarters, headquartersPosition, Quaternion.identity, positionableContainer);
            var damageable = headquarters.GetComponent<Damageable>();
            damageable.Destroyed += () => HeadquartersDestroyed?.Invoke();
            positionables.Add(headquarters);

            // Instantiate obstacles in random positions
            var obstacles = applicationController.Prefabs.Obstacles;
            for (int i = 0; i < applicationController.Settings.ObstaclesToSpawn; i++)
            {
                var obstacle = Instantiate(obstacles[Random.Range(0, obstacles.Count)], positionableContainer);
                do
                {
                    obstacle.Position = new Vector2Int(
                        Random.Range(minGridDimensions.x, maxGridDimensions.x + 1),
                        Random.Range(minGridDimensions.y, maxGridDimensions.y + 1));
                }
                while (!IsValidPosition(obstacle));

                obstacle.transform.position = grid.GetSnappedPosition(obstacle.Position, obstacle.SizeVector);

                positionables.Add(obstacle);
            }

            // Setup button events
            fightButton.onClick.AddListener(OnFightButtonPressed);
            giveUpButton.onClick.AddListener(OnGiveUpButtonPressed);
            doubleTimeButton.onClick.AddListener(OnDoubleTimeButtonPressed);
            normalTimeButton.onClick.AddListener(OnNormalTimeButtonPressed);
        }

        #endregion

        #region Private methods

        private void SpawnEnemy(AIControlled enemyPrefab, int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                int cellIndex = Random.Range(0, spawnCells.Count);
                var position = grid.GetSnappedPosition(spawnCells[cellIndex]);

                var enemy = Instantiate(enemyPrefab, position, Quaternion.identity, positionableContainer);
                enemy.gameObject.name = $"Enemy{i}";
                enemy.LookForDestination();

                var damageable = enemy.GetComponent<Damageable>();
                damageable.Destroyed += () => OnEnemyDestroyed(enemy);

                var shooter = enemy.GetComponent<Shooter>();
                shooter.StartLookingForTarget();

                enemies.Add(enemy);
            }
        }

        private void UpdatePositioning(Vector3 position)
        {
            var positionable = currentDeployable.GetComponent<Positionable>();
            positionable.Position = grid.WorldToCell(position).ToVector2Int();

            bool isValid = IsValidPosition(positionable);

            currentDeployable.SetValid(isValid);
            currentPositioningButtons.SetValid(isValid);

            var snappedPosition = grid.GetSnappedPosition(position, positionable.SizeVector);
            currentDeployable.transform.position = snappedPosition;
        }

        private bool IsValidPosition(Positionable positionable)
        {
            var positionableBounds = new BoundsInt(positionable.Position.ToVector3Int(true), positionable.SizeVector.ToVector3Int(true));
            var gridBounds = applicationController.Settings.GridBounds;

            return gridBounds.Contains(positionableBounds)
                && !positionables.Any(x => x.Bounds.Intersects(positionableBounds));
        }

        private void ExplodeMine(Transform transform)
        {
            var explosive = transform.GetComponent<Explosive>();
            explosive.Explode();
        }

        private void OnEnemyDestroyed(AIControlled enemy)
        {
            enemies.Remove(enemy);

            if (enemies.Count <= 0)
            {
                isVictory = true;

                AllEnemiesDefeated?.Invoke();
            }
        }

        private void OnFingerUpdate(LeanFinger finger)
        {
            if (!finger.IsOverGui && LeanTouch.Fingers.Count == 1)
            {
                var ray = finger.GetRay(Camera.main);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Layer.Floor))
                {
                    UpdatePositioning(hit.point);
                }
            }
        }

        private void OnFingerTap(LeanFinger finger)
        {
            if (!finger.IsOverGui && LeanTouch.Fingers.Count == 1)
            {
                var ray = finger.GetRay(Camera.main);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Layer.Mine))
                {
                    ExplodeMine(hit.transform);
                }
            }
        }

        private void OnDeployableButtonPressed(Deployable deployable)
        {
            currentDeployable = deployable;

            DeployableButtonPressed?.Invoke();
        }

        private void OnFightButtonPressed()
        {
            FightButtonPressed?.Invoke();
        }

        private void OnGiveUpButtonPressed()
        {
            GiveUpButtonPressed?.Invoke();
        }

        private void OnDoubleTimeButtonPressed()
        {
            DoubleTimeButtonPressed?.Invoke();

            doubleTimeButton.gameObject.SetActive(false);
            normalTimeButton.gameObject.SetActive(true);
        }

        private void OnNormalTimeButtonPressed()
        {
            NormalTimeButtonPressed?.Invoke();

            doubleTimeButton.gameObject.SetActive(true);
            normalTimeButton.gameObject.SetActive(false);
        }

        #endregion
    }
}
