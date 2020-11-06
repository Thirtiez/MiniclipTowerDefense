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
        }

        public static class Game
        {
            public static readonly int Planning = Animator.StringToHash("Planning");
            public static readonly int Positioning = Animator.StringToHash("Positioning");
            public static readonly int Fighting = Animator.StringToHash("Fighting");
            public static readonly int Resolution = Animator.StringToHash("Resolution");
        }
    }

    public static class LocalizationKey
    {
        public static readonly string QuitModalTitle = "QuitModalTitle";
        public static readonly string QuitModalDescription = "QuitModalDescription";
        public static readonly string GiveUpModalTitle = "GiveUpModalTitle";
        public static readonly string GiveUpModalDescription = "GiveUpModalDescription";
        public static readonly string ResolutionModalTitle_Defeat = "ResolutionModalTitle_Defeat";
        public static readonly string ResolutionModalTitle_Victory = "ResolutionModalTitle_Victory";
    }

    public static class Layer
    {
        public static readonly LayerMask Floor = LayerMask.GetMask("Floor");
        public static readonly LayerMask Enemy = LayerMask.GetMask("Enemy");
        public static readonly LayerMask Tower = LayerMask.GetMask("Tower");
        public static readonly LayerMask Mine = LayerMask.GetMask("Mine");
    }
}