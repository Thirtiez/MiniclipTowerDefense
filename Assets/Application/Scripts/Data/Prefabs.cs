using System.Collections.Generic;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    [CreateAssetMenu(menuName = "Data/Prefabs")]
    public class Prefabs : ScriptableObject
    {
        [Header("Game")]
        [SerializeField]
        private List<Deployable> towers;
        public List<Deployable> Towers { get { return towers; } }
        [SerializeField]
        private List<AIControlled> enemies;
        public List<AIControlled> Enemies { get { return enemies; } }
        [SerializeField]
        private Positionable headquarters;
        public Positionable Headquarters { get { return headquarters; } }
        [SerializeField]
        private LineRenderer gridLine;
        public LineRenderer GridLine { get { return gridLine; } }

        [Header("UI")]
        [SerializeField]
        private DeployableButton deployableButton;
        public DeployableButton DeployableButton { get { return deployableButton; } }
        [SerializeField]
        private PositioningButtons positioningButtons;
        public PositioningButtons PositioningButtons { get { return positioningButtons; } }
        [SerializeField]
        private SpriteRenderer sizeVisualizer;
        public SpriteRenderer SizeVisualizer { get { return sizeVisualizer; } }
        [SerializeField]
        private SpriteRenderer radiusVisualizer;
        public SpriteRenderer RadiusVisualizer { get { return radiusVisualizer; } }
    }
}
