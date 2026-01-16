using System;
using System.Collections.Generic;
using Gameplay.Data;
using Gameplay.Data.Inventory;
using Gameplay.Data.Inventory.InventorySaveSystem;
using Gameplay.Data.Rewards;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Core
{
    [Serializable]
    [CreateAssetMenu(fileName = "wheel_game_live_event_controller", menuName = "Live Event System/Wheel Game Controller", order = 1)]
    public class WheelGameLiveEvent : BaseLiveEvent
    {
        private const int SLOT_COUNT = 8;
        [field:SerializeField] public override BaseLiveEventData LiveEventData { get; set; }
        private WheelGameLiveEventData gameData = null;
        public WheelGameLiveEventData GameData
        {
            get
            {
                if (gameData == null)
                {
                    gameData = LiveEventData as WheelGameLiveEventData;
                }
                
                return gameData;
            }
        }
        
        private Dictionary<BaseInventoryItemInfo, int> gainedRewardsInventory = new();
        public Dictionary<BaseInventoryItemInfo, int> GainedRewardsInventory => gainedRewardsInventory;
    
        private KeyValuePair<Reward, int> lastGainedRewardInfo = new KeyValuePair<Reward, int>();
        // [SerializeField] private WheelGamePanel WheelGamePanel;
        // public CollectedRewardPanel CollectedRewardPanel;


        public override void Initialize()
        {
            MainEventHandler.OnWheelGameCompleted += OnCardGameCompleted;
            MainEventHandler.OnWheelGameClosed += OnWheelGameClosed;
        }

        private void OnCardGameCompleted(bool isWin)
        {
            if (isWin)
            {
                foreach (var _inventoryInfo in gainedRewardsInventory)
                {
                    InventoryService.AddItemToInventory(_inventoryInfo.Key.ID, _inventoryInfo.Value);
                }

                InventoryService.SaveInventoryData();
            }
        
            // CollectedRewardPanel.gameObject.SetActive(true);
            // CollectedRewardPanel.Initialize(GainedRewardsInventory);

            GameStateHolder.WheelGameCurrentStep = 0;
            lastGainedRewardInfo = new KeyValuePair<Reward, int>();
            //Get CollectedRewardPanel
        
            MainEventHandler.OnWheelGameCompleted -= OnCardGameCompleted;
        }

        private void OnWheelGameClosed()
        {
            GainedRewardsInventory.Clear();
            MainEventHandler.OnWheelGameClosed -= OnWheelGameClosed;
        }

        public void SpinEnded()
        {
            AddGainedReward();
            Debug.Log("Spin ended. Gained reward: " + lastGainedRewardInfo.Key.RewardType.ToString());
            MainEventHandler.OnSpinEnded?.Invoke();
        }

        private void AddGainedReward()
        {
            BaseInventoryItemInfo _inventoryInfo = GetCurrentStepRewardInfo().Reward.GetInventoryInfo();
            if (_inventoryInfo != null)
            {
                if (_inventoryInfo.InventoryItemConsumeType == InventoryItemConsumeType.NonConsumable)
                {
                    if (InventoryService.HasItem(_inventoryInfo.ID) || GainedRewardsInventory.ContainsKey(_inventoryInfo))
                    {
                        BaseInventoryItemInfo _fallbackInventoryInfo = _inventoryInfo.FallbackReward.GetInventoryInfo();
                        if (GainedRewardsInventory.ContainsKey(_fallbackInventoryInfo))
                        {
                            lastGainedRewardInfo = new KeyValuePair<Reward, int>(_inventoryInfo.FallbackReward, GetCurrentStepRewardInfo().Amount);
                            GainedRewardsInventory[_fallbackInventoryInfo] += GetCurrentStepRewardInfo().Amount;
                        }
                        else
                        {
                            lastGainedRewardInfo = new KeyValuePair<Reward, int>(_inventoryInfo.FallbackReward, GetCurrentStepRewardInfo().Amount);
                            GainedRewardsInventory.Add(_fallbackInventoryInfo, GetCurrentStepRewardInfo().Amount);
                        }
                    }
                    else
                    {
                        lastGainedRewardInfo = new KeyValuePair<Reward, int>(GetCurrentStepRewardInfo().Reward, GetCurrentStepRewardInfo().Amount);
                        GainedRewardsInventory.Add(_inventoryInfo, GetCurrentStepRewardInfo().Amount);
                    }
                }
                else
                {
                    if (GainedRewardsInventory.ContainsKey(_inventoryInfo))
                    {
                        GainedRewardsInventory[_inventoryInfo] += GetCurrentStepRewardInfo().Amount;
                    }
                    else
                    {
                        GainedRewardsInventory.Add(_inventoryInfo, GetCurrentStepRewardInfo().Amount);
                    }
                    
                    lastGainedRewardInfo = new KeyValuePair<Reward, int>(GetCurrentStepRewardInfo().Reward, GetCurrentStepRewardInfo().Amount);
                }
            }
            else
            {
                Debug.LogError("Inventory info catalog does not contain this reward's inventory info: " +
                               GetCurrentStepRewardInfo().Reward.RewardType.ToString());
            }
        }

        public void ProceedStep()
        {
            GameStateHolder.WheelGameCurrentStep++;

            if (GameStateHolder.WheelGameCurrentStep >= GameData.StepList.Count)
            {
                GameStateHolder.GameState = GameState.GameEnded;
                MainEventHandler.OnWheelGameCompleted?.Invoke(gameData);
            }
            else
            {
                GameStateHolder.GameState = GameState.Waiting;
                MainEventHandler.OnStepProceeded?.Invoke(gameData);
            }

        }

        public void SetRandomRewardIndex()
        {
            GameStateHolder.WheelGameRewardIndex = Random.Range(0, SLOT_COUNT);
        }

        public Step GetCurrentStepData()
        {
            return GameData.StepList[GameStateHolder.WheelGameCurrentStep];
        }

        public StepRewardInfo GetCurrentStepRewardInfo()
        {
            return GameData.StepList[GameStateHolder.WheelGameCurrentStep].Rewards[GameStateHolder.WheelGameRewardIndex];
        }

        public KeyValuePair<Reward, int> GetLastGainedReward()
        {
            return lastGainedRewardInfo;
        }

    }
}