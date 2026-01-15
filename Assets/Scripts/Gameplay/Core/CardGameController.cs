using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Utils;
using Gameplay.Core;
using Gameplay.Data;
using Gameplay.Data.Inventory;
using Gameplay.Data.Inventory.InventorySaveSystem;
using Gameplay.Data.Rewards;
using UnityEngine;

public class CardGameController : SingletonBehaviour<CardGameController>
{
    private const int SLOT_COUNT = 8;
    
    [SerializeField] private CardGameData gameData;
    public CardGameData GameData => gameData;
    private Dictionary<BaseInventoryItemInfo, int> gainedRewardsInventory = new();
    public Dictionary<BaseInventoryItemInfo, int> GainedRewardsInventory => gainedRewardsInventory;

    public void Initialize()
    {
        MainEventHandler.OnCardGameStarted?.Invoke(gameData);
        MainEventHandler.OnCardGameCompleted += OnCardGameCompleted;
    }

    private void OnCardGameCompleted(bool isWin)
    {
        if (isWin)
        {
            foreach (var inventoryInfo in gainedRewardsInventory)
            {
                InventoryService.AddItemToInventory(inventoryInfo.Key.ID, inventoryInfo.Value);
            }
            
            InventoryService.SaveInventoryData();
        }
        
        GameStateHolder.CardGameCurrentStep = 0;
        MainEventHandler.OnCardGameCompleted -= OnCardGameCompleted;
        GainedRewardsInventory.Clear();
    }

    public void SpinEnded()
    {
        AddGainedReward();
        MainEventHandler.OnSpinEnded?.Invoke();
    }

    private void AddGainedReward()
    {
        var inventoryInfo = GetCurrentStepRewardInfo().Reward.GetInventoryInfo();
        if (inventoryInfo != null)
        {
            if (inventoryInfo.InventoryItemConsumeType == InventoryItemConsumeType.NonConsumable)
            {
                if (GainedRewardsInventory.ContainsKey(inventoryInfo))
                {
                    var fallbackInventory = inventoryInfo.FallbackReward.GetInventoryInfo();
                    GainedRewardsInventory[fallbackInventory] += GetCurrentStepRewardInfo().Amount;
                }
                else
                {
                    GainedRewardsInventory.Add(inventoryInfo, GetCurrentStepRewardInfo().Amount);
                }
                
            }
            else
            {
                if (GainedRewardsInventory.ContainsKey(inventoryInfo))
                {
                    GainedRewardsInventory[inventoryInfo] += GetCurrentStepRewardInfo().Amount;
                }
                else
                {
                    GainedRewardsInventory.Add(inventoryInfo, GetCurrentStepRewardInfo().Amount);
                }
            }
        }
        else
        {
            Debug.LogError("Inventory info catalog does not contain this reward's inventory info: " + GetCurrentStepRewardInfo().Reward.RewardType.ToString());
        }
    }
    
    public void ProceedStep()
    {
        GameStateHolder.CardGameCurrentStep++;
        MainEventHandler.OnStepProceeded?.Invoke(gameData);
        GameStateHolder.GameState = GameState.Waiting;
    }

    public void SetRandomRewardIndex()
    {
        GameStateHolder.CardGameRewardIndex = Random.Range(0, SLOT_COUNT);
    }
    
    public Step GetCurrentStepData()
    {
        return GameData.StepList[GameStateHolder.CardGameCurrentStep];
    }
    
    public StepRewardInfo GetCurrentStepRewardInfo()
    {
        return GameData.StepList[GameStateHolder.CardGameCurrentStep].Rewards[GameStateHolder.CardGameRewardIndex];
    }
}