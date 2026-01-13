namespace Gameplay.Core
{
    //Holds game's decisive values
    public static class GameStatus
    {
        public static int CardGameCurrentStep = 0;
        public static int CardGameRewardIndex = 0;
        public static GameState GameState = GameState.Started;
    }

    public enum GameState
    {
        Started = 0,
        Playing = 1,
        Waiting = 2,
        Ended = 3
    }
}
