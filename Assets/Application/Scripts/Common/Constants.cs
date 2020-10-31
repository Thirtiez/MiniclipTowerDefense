using System.Collections.Generic;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public static class FSMTrigger
    {
        public static class Scene
        {
            public static readonly int Forward = Animator.StringToHash("Forward");
            public static readonly int Back = Animator.StringToHash("Back");
            public static readonly int Splash = Animator.StringToHash("Splash");
            public static readonly int MainMenu = Animator.StringToHash("MainMenu");
            public static readonly int Game = Animator.StringToHash("Game");
            public static readonly int Result = Animator.StringToHash("Result");
        }

        public static class Game
        {
            public static readonly int Planning = Animator.StringToHash("Planning");
            public static readonly int Positioning = Animator.StringToHash("Positioning");
            public static readonly int Fighting = Animator.StringToHash("Fighting");
            public static readonly int Resolution = Animator.StringToHash("Resolution");
        }
    }

    public static class Layers
    {
        public static readonly int Floor = LayerMask.NameToLayer("Floor");
    }
}