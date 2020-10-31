using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    [CreateAssetMenu(menuName = "Data/DeployableData")]
    public class DeployableData : ScriptableObject
    {
        [ShowInInspector]
        public List<Deployable> Deployables { get; set; }
    }
}
