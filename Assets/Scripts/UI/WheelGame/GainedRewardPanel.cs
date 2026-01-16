using System.Collections.Generic;
using Gameplay.Core;
using Gameplay.Data;
using Gameplay.Data.Rewards;
using LiveEventService;
using UI.RewardItems;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Pool;

namespace UI.WheelGame
{
    public class GainedRewardPanel : MonoBehaviour
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
        
        [SerializeField] private Button exitButton;
        [SerializeField] private RectTransform gainedRewardsParent;
        [SerializeField] private List<RewardUIItem> gainedRewardUIItems;
        private Dictionary<RewardType, int> gainedRewards;

        public void Initialize()
        {
            MainEventHandler.OnWheelGameCompleted += OnGameEnded;
            MainEventHandler.OnSpinEnded += OnSpinEnded;
            MainEventHandler.OnWheelGameStarted += OnWheelGameStarted;
            MainEventHandler.OnWheelGameCompleted += OnWheelGameCompleted;
        }

        private void OnWheelGameStarted(WheelGameLiveEventData gameData)
        {
            InitializeCollections();
            exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnExitButtonClicked()
        {
            if (GameStateHolder.GameState == GameState.Playing) 
                return;
             
            MainEventHandler.OnWheelGameCompleted?.Invoke(true);
        }

        private void OnWheelGameCompleted(bool isWin)
        {
            MainEventHandler.OnWheelGameCompleted -= OnGameEnded;
            MainEventHandler.OnSpinEnded -= OnSpinEnded;
            MainEventHandler.OnWheelGameStarted -= OnWheelGameStarted;
            MainEventHandler.OnWheelGameCompleted -= OnWheelGameCompleted;
            exitButton.onClick.AddListener(OnExitButtonClicked);
            ResetPanel();
        }

        private void ResetPanel()
        {
            foreach (var rewardUIItem in gainedRewardUIItems)
            {
                rewardUIItem.ResetObject();
            }
            
            gainedRewardUIItems.Clear();
            gainedRewards.Clear();
        }

        private void OnGameStarted()
        {
            
        }

        private void OnGameEnded(bool isWin)
        {
            
        }

        private void InitializeCollections()
        {
            if (gainedRewards == null)
                gainedRewards = new Dictionary<RewardType, int>();
            else
                gainedRewards.Clear();

            if (gainedRewardUIItems == null)
                gainedRewardUIItems = new();
            else
                gainedRewardUIItems.Clear();
        }

        private async void OnSpinEnded()
        {
            var _lastGainedReward = LiveEvent.GetLastGainedReward();

            if (TryGetRewardUIItem(_lastGainedReward.Key, _lastGainedReward.Value, out var rewardUIItem))
            {
                await rewardUIItem.SetAmountWithTween(gainedRewards[_lastGainedReward.Key.RewardType]);
            }
            else
            {
                var rewardItem = PoolFactory.Instance.GetObject<RewardUIItem>(ObjectType.RewardUIItem, gainedRewardsParent);
                rewardItem.RectTransform.sizeDelta = new Vector2(gainedRewardsParent.rect.width / 2, gainedRewardsParent.rect.width / 2);
                rewardItem.Initialize(_lastGainedReward.Key.GetInventoryIcon(), 0.ToString(), _lastGainedReward.Key.RewardType);
                
                gainedRewardUIItems.Add(rewardItem);
                gainedRewards.Add(_lastGainedReward.Key.RewardType, _lastGainedReward.Value);
                await rewardItem.SetAmountWithTween(gainedRewards[_lastGainedReward.Key.RewardType]);
            }

            // await reward animations etc.
            LiveEvent.ProceedStep();
        }

        private bool TryGetRewardUIItem(Reward reward, int amount, out RewardUIItem result)
        {
            var _gainedReward = gainedRewardUIItems.Find(x => x.RewardType == reward.RewardType);
            if (_gainedReward)
            {
                result = _gainedReward;
                gainedRewards[reward.RewardType] += amount;
                    
                return true;
            }

            result = null;
            return false;
        }
    }
}