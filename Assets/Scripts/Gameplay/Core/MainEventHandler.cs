using System;
using System.Collections.Generic;
using Gameplay.Data;

namespace Gameplay.Core
{
    //Provides decisive game events
    public static class MainEventHandler
    {
        public static Action OnGameStarted;
        public static Action<CardGameData> OnCardGameStarted;
        public static Action<int> OnSpinStarted;
        public static Action<int> OnSpinEnded;
        public static Action<List<StepRewardInfo>> OnStepProceeded;
        public static Action<bool> OnCardGameCompleted;
    }
}