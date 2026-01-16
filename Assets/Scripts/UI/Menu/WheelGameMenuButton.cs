using Cysharp.Threading.Tasks;
using Gameplay.Core;
using LiveEventService;
using UI.Panel;

namespace UI.Menu
{
    public class WheelGameMenuButton : BaseMenuButton
    {
        private WheelGameLiveEvent liveEvent;
        private WheelGameLiveEvent LiveEvent
        {
            get
            {
                if (liveEvent == null)
                {
                    liveEvent = LiveEventSystem.Instance.GetLiveEvent<WheelGameLiveEvent>();
                }
                
                return liveEvent;
            }
        }
        
        public override void Initialize()
        {
            button.onClick.AddListener(OpenWheelPanel);
        }

        private void OpenWheelPanel()
        {
            GameStateHolder.GameState = GameState.WheelGameStarted;
            MenuController.Instance.StartWheelGame();
        }
    }
}
