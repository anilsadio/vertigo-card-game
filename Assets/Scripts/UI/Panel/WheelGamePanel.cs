using Gameplay.Core;
using Gameplay.Data;
using Gameplay.Data.Rewards;
using LiveEventService;
using UI.WheelGame;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panel
{
    public class WheelGamePanel : MonoBehaviour
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

        private WheelGameLiveEventData eventData => LiveEvent.LiveEventData as WheelGameLiveEventData;

        [SerializeField] private WheelUIController wheelUIController;
        [SerializeField] private WheelGameStepUIController wheelGameStepUIController;
        [SerializeField] private GainedRewardPanel gainedRewardPanel;
        [SerializeField] private CollectedRewardPanel collectedRewardPanel;
        [SerializeField] private BombFailPanel bombFailPanel;
        [SerializeField] private Button spinButton;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartSpin();
            }
        }

        public void Initialize()
        {
            MainEventHandler.OnWheelGameCompleted += OnWheelGameCompleted;
            MainEventHandler.OnSpinEnded += OnSpinEnded;
            
            wheelUIController.Initialize();
            wheelGameStepUIController.Initialize();
            gainedRewardPanel.Initialize();
            spinButton.onClick.AddListener(StartSpin);
            MainEventHandler.OnWheelGameStarted?.Invoke(eventData);
        }

        private void OnWheelGameCompleted(bool isWin)
        {
            MainEventHandler.OnWheelGameCompleted -= OnWheelGameCompleted;
            MainEventHandler.OnSpinEnded -= OnSpinEnded;
            
            spinButton.onClick.RemoveAllListeners();
            if (isWin)
            {
                collectedRewardPanel.gameObject.SetActive(true);
                collectedRewardPanel.Initialize(LiveEvent.GainedRewardsInventory);
            }
            gameObject.SetActive(false);
        }

        private void StartSpin()
        {
            if (GameStateHolder.GameState == GameState.Playing || GameStateHolder.GameState == GameState.GameEnded)
                return;

            Debug.Log("Spin Started. GameState wass " + GameStateHolder.GameState.ToString());
            GameStateHolder.GameState = GameState.Playing;
            LiveEvent.SetRandomRewardIndex();
            
            MainEventHandler.OnSpinStarted?.Invoke(GameStateHolder.WheelGameRewardIndex);
        }

        private void OnSpinEnded(Transform rewardTransform)
        {
            if (LiveEvent.GetLastGainedReward().Key.RewardType == RewardType.Bomb)
            {
                bombFailPanel.gameObject.SetActive(true);
                bombFailPanel.Initialize();
            }
                
        }
    }
}