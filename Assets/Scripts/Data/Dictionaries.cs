using System;
using System.Collections.Generic;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    [CreateAssetMenu(menuName = "Data/Dictionaries")]
    public class Dictionaries : ScriptableObject
    {
        [Header("Tiles")]
        [SerializeField]
        private Dictionary<int, string> something = new Dictionary<int, string>();
    }
}
