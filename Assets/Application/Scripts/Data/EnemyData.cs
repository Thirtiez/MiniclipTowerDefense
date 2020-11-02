using System.Collections.Generic;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    [CreateAssetMenu(menuName = "Data/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField]
        private List<AIControlled> enemies;
        public List<AIControlled> Enemies { get { return enemies; } }
    }
}
