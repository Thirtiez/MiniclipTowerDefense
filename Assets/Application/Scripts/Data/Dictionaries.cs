using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    [CreateAssetMenu(menuName = "Data/Dictionaries")]
    public class Dictionaries : SerializedScriptableObject
    {
        [Header("Tiles")]
        [SerializeField]
        private Dictionary<int, string> something = new Dictionary<int, string>();
    }
}
