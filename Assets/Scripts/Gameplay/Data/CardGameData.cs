using System.Collections.Generic;
using Gameplay.Data.Inventory;
using Gameplay.Data.Rewards;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "card_game_data", menuName = "CardGame/Data", order = 0)]
    public class CardGameData : ScriptableObject
    {
        //Shows the error box on inspector in case of silver and gold steps include bomb. It is an assistant on building data.
        [InfoBox("Elements that are multiples of 5 cannot contain bombs. Check the elements.", InfoMessageType.Error, VisibleIf = nameof(ValidateSteps))]
        [ListDrawerSettings(ShowFoldout = false, DraggableItems = false, ShowIndexLabels = true, AddCopiesLastElement = true)]
        public List<Step> StepList = new List<Step>();

        public List<WheelDisplayInfo> WheelImageInfos = new List<WheelDisplayInfo>();

        public WheelDisplayInfo GetWheelInfo(WheelType wheelType)
        {
            return WheelImageInfos.Find(target => target.WheelType == wheelType);
        }

#if UNITY_EDITOR
        private bool ValidateSteps()
        {
            for (int i = 0; i <= StepList.Count; i += 5)
            {
                var index = i == 0 ? i : i - 1;

                //Sets wheel type on step list validated.
                if (i == 0)
                {
                    StepList[index].WheelType = WheelType.Silver;
                }
                else if (i % 30 == 0 )
                {
                    StepList[index].WheelType = WheelType.Gold;
                }
                else if (i % 5 == 0)
                {
                    StepList[index].WheelType = WheelType.Silver;
                }
                else
                {
                    Debug.Log(index + ". items wheel type is Bronze");
                    StepList[index].WheelType = WheelType.Bronze;
                }

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

        public WheelType WheelType;

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
    public struct WheelDisplayInfo
    {
        public Sprite WheelImage;
        public Sprite CursorImage;
        public WheelType WheelType;
        public Color TextColor;
    }

    public enum WheelType
    {
        Bronze = 0,
        Silver = 1,
        Gold = 2
    }
}