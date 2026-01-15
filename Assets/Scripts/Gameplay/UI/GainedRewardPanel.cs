using System;
using System.Collections.Generic;
using DG.Tweening;
using Gameplay.Core;
using Gameplay.Data;
using Gameplay.Data.Inventory;
using Gameplay.Data.Rewards;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Pool;

namespace Gameplay.UI
{
    public class GainedRewardPanel : MonoBehaviour
    {
        [SerializeField] private Button exitButton;
        [SerializeField] private RectTransform gainedRewardsParent;
        [SerializeField] private List<RewardUIItem> gainedRewardUIItems;
        private Dictionary<RewardType, int> gainedRewards;

        private void OnEnable()
        {
            MainEventHandler.OnGameStarted += OnGameStarted;
            MainEventHandler.OnCardGameCompleted += OnCardGameCompleted;
            exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnDisable()
        {
            MainEventHandler.OnGameStarted -= OnGameStarted;
            exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnExitButtonClicked()
        {
            if (GameStateHolder.GameState == GameState.Playing) 
                return;
            
            MainEventHandler.OnCardGameCompleted?.Invoke(true);
        }

        private void OnCardGameCompleted(bool isWin)
        {
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
            MainEventHandler.OnCardGameCompleted += OnGameEnded;
            MainEventHandler.OnSpinEnded += OnSpinEnded;
            InitializeCollections();
        }

        private void OnGameEnded(bool isWin)
        {
            MainEventHandler.OnCardGameCompleted -= OnGameEnded;
            MainEventHandler.OnSpinEnded -= OnSpinEnded;
            ResetPanel();
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
            var stepRewardInfo = CardGameController.Instance.GetCurrentStepRewardInfo();

            if (TryGetRewardUIItem(stepRewardInfo, out var rewardUIItem))
            {
                await rewardUIItem.SetAmountWithTween(gainedRewards[stepRewardInfo.Reward.RewardType]);
            }
            else
            {
                var rewardItem =
                    PoolFactory.Instance.GetObject<RewardUIItem>(ObjectType.RewardUIItem, gainedRewardsParent);
                rewardItem.RectTransform.sizeDelta = new Vector2(gainedRewardsParent.rect.width / 2, gainedRewardsParent.rect.width / 2);
                rewardItem.Initialize(stepRewardInfo.Reward.GetInventoryIcon(), 0.ToString(), stepRewardInfo.Reward.RewardType);
                
                gainedRewardUIItems.Add(rewardItem);
                gainedRewards.Add(stepRewardInfo.Reward.RewardType, stepRewardInfo.Amount);
                await rewardItem.SetAmountWithTween(gainedRewards[stepRewardInfo.Reward.RewardType]);
            }

            // await reward animations etc.
            CardGameController.Instance.ProceedStep();
        }

        private bool TryGetRewardUIItem(StepRewardInfo stepRewardInfo, out RewardUIItem result)
        {
            if (stepRewardInfo.Reward.GetInventoryInfo().InventoryItemConsumeType == InventoryItemConsumeType.NonConsumable)
            {
                var gainedReward = gainedRewardUIItems.Find(x => x.RewardType == stepRewardInfo.Reward.RewardType);
            
                if (gainedReward)
                {
                    var fallbackReward = stepRewardInfo.Reward.GetInventoryInfo().FallbackReward.GetInventoryInfo().FallbackReward;
                    var fallbackGainedReward = gainedRewardUIItems.Find(x => x.RewardType == fallbackReward.RewardType);

                    if (fallbackGainedReward)
                    {
                        result = fallbackGainedReward;
                        gainedRewards[fallbackReward.RewardType] += stepRewardInfo.Amount;
                        return true;
                    }
                }
            }
            else
            {
                var gainedReward = gainedRewardUIItems.Find(x => x.RewardType == stepRewardInfo.Reward.RewardType);
                
                if (gainedReward)
                {
                    result = gainedReward;
                    gainedRewards[stepRewardInfo.Reward.RewardType] += stepRewardInfo.Amount;
                    
                    return true;
                }
            }

            result = null;
            return false;
        }
    }
}