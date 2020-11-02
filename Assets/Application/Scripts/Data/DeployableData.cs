using System.Collections.Generic;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    [CreateAssetMenu(menuName = "Data/DeployableData")]
    public class DeployableData : ScriptableObject
    {
        [SerializeField]
        private List<Deployable> deployables;
        public List<Deployable> Deployables { get { return deployables; } }
    }
}
