using System.Collections.Generic;
using Gameplay.Data.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gameplay.Data
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "card_game_data", menuName = "CardGame/Data", order = 0)]
    public class CardGameData : ScriptableObject
    {
        public List<Step> StepList = new List<Step>();
    }

    [System.Serializable]
    public class Step
    {
        [InfoBox("This array's element count must be 8.", InfoMessageType.Warning)]
        [ValidateInput(nameof(ValidateRewards))]
        public StepReward[] Rewards = new StepReward[8];

#if UNITY_EDITOR
        private bool ValidateRewards(StepReward[] value)
        {
            if (Rewards is not { Length: 8 })
            {
                System.Array.Resize(ref Rewards, 8);
            }

            return value is { Length: 8 };
        }
#endif
    }

    [System.Serializable]
    public struct StepReward
    {
        public BaseInventoryItemInfo ItemInfo;
        public int Amount;
    }
}