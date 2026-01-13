using System.Collections.Generic;
using Gameplay.Data.Inventory;
using Gameplay.Data.Rewards;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "card_game_data", menuName = "CardGame/Data", order = 0)]
    public class CardGameData : ScriptableObject
    {
        //Shows the error box on inspector in case of silver and gold steps include bomb. It is an assistant on building data.
        [InfoBox("Elements that are multiples of 5 cannot contain bombs. Check the elements.", InfoMessageType.Error, VisibleIf = nameof(ShowErrorBox))]
        [ListDrawerSettings(ShowFoldout = false, DraggableItems = false, ShowIndexLabels = true)]
        [SerializeField] public List<Step> StepList = new List<Step>();

        [SerializeField] public List<WheelImageInfo> WheelImageInfos = new List<WheelImageInfo>();

#if UNITY_EDITOR
        private bool ShowErrorBox()
        {
            for (int i = 0; i < StepList.Count; i += 5)
            {
                var index = i == 0 ? i : i - 1;

                foreach (var reward in StepList[index].Rewards)
                {
                    if (reward.Reward.RewardType == RewardType.Bomb)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
#endif
    }

    [System.Serializable]
    public class Step
    {
        [InfoBox("This array's element count must be 8.", InfoMessageType.Warning)]
        [ValidateInput(nameof(ValidateRewards))]
        public StepRewardInfo[] Rewards = new StepRewardInfo[8];

#if UNITY_EDITOR
        private bool ValidateRewards(StepRewardInfo[] value)
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
    public struct StepRewardInfo
    {
        public Reward Reward;
        public int Amount;
    }

    [System.Serializable]
    public struct RewardInventoryInfo
    {
        public BaseInventoryItemInfo InventoryItemInfo;
        public int Amount;
    }

    [System.Serializable]
    public struct WheelImageInfo
    {
        public Sprite WheelImage;
        public Sprite CursorImage;
        public WheelType WheelType;
    }

    public enum WheelType
    {
        Bronze = 0,
        Silver = 1,
        Gold = 2
    }
}