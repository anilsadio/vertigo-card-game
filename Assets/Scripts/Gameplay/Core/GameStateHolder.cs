namespace Gameplay.Core
{
    //Holds game's decisive values
    public static class GameStateHolder
    {
        public static int WheelGameCurrentStep = 0;
        public static int WheelGameRewardIndex = 0;
        public static GameState GameState = GameState.WheelGameStarted;
    }

    public enum GameState
    {
        WheelGameStarted = 0,
        Playing = 1,
        Waiting = 2,
        GameEnded = 3
    }
}
