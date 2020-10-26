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
        public static readonly int Result = Animator.StringToHash("Result");
    }

    public static class Messages
    {

    }
}