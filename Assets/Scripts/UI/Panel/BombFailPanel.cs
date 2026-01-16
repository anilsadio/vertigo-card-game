using Gameplay.Core;
using LiveEventService;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panel
{
    public class BombFailPanel : MonoBehaviour
    {
        [SerializeField] private Button giveupButton;
        [SerializeField] private Button reviveButton;
        
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

        public void Initialize()
        {
            giveupButton.onClick.AddListener(OnGiveUpButtonClicked);
            reviveButton.onClick.AddListener(OnReviveButtonClicked);
        }
        
        private void OnGiveUpButtonClicked()
        {
            giveupButton.onClick.RemoveAllListeners();
            reviveButton.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
            MainEventHandler.OnWheelGameCompleted?.Invoke(false);
            MainEventHandler.OnWheelGameClosed?.Invoke();
            MainEventHandler.OnMenuOpened?.Invoke();
        }       
        private void OnReviveButtonClicked()
        {
            giveupButton.onClick.RemoveAllListeners();
            reviveButton.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
            LiveEvent.ProceedStep();
        }
    }
}
