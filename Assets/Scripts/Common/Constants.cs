using System.Collections.Generic;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public static class FSMTrigger
    {
        public static readonly int Forward = Animator.StringToHash("Forward");
        public static readonly int Back = Animator.StringToHash("Back");
        public static readonly int Splash = Animator.StringToHash("Splash");
        public static readonly int MainMenu = Animator.StringToHash("MainMenu");
        public static readonly int Game = Animator.StringToHash("Game");
        public static readonly int Reward = Animator.StringToHash("Reward");

        public static Dictionary<string, int> SceneToTrigger = new Dictionary<string, int>()
        {
            { "00_Splash", Splash },
            { "01_MainMenu", MainMenu },
            { "02_Game", Game },
            { "03_Reward", Reward },
        };
    }

    public static class Messages
    {

    }
}