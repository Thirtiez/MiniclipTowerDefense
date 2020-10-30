using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    [CreateAssetMenu(menuName = "Data/DeployableData")]
    public class DeployableData : ScriptableObject
    {
        [ShowInInspector]
        private List<DeployableUIElement> Deployables { get; set; }
    }
}
