using System;
using System.Collections.Generic;
using Gameplay.Data;

namespace Gameplay.Core
{
    //Provides decisive game events
    public static class MainEventHandler
    {
        //register event handler yap
        public static Action OnGameStarted;
        public static Action<WheelGameLiveEventData> OnWheelGameStarted;
        public static Action<int> OnSpinStarted;
        public static Action OnSpinEnded;
        public static Action<WheelGameLiveEventData> OnStepProceeded;
        public static Action<bool> OnWheelGameCompleted;
        public static Action OnWheelGameClosed;
        public static Action OnMenuOpened;
    }
}