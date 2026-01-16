using System.Collections.Generic;
using Gameplay.Data.Inventory;
using Gameplay.Data.Rewards;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "wheel_game_live_event_config", menuName = "LiveEventSystem/WheelGame/Config", order = 0)]
    public class WheelGameLiveEventData : BaseLiveEventData
    {
        //Shows the error box on inspector in case of silver and gold steps include bomb. It is an assistant on building data.
        [field: InfoBox("Elements that are multiples of 5 cannot contain bombs. Check the elements.", InfoMessageType.Error, VisibleIf = nameof(ValidateSteps))]
        [field: ListDrawerSettings(ShowFoldout = false, DraggableItems = false, ShowIndexLabels = true, AddCopiesLastElement = true)]
        [field: SerializeField] public List<Step> StepList { get; private set; }

        [field: SerializeField] public List<WheelDisplayInfo> WheelImageInfos { get; private set; }

        public WheelDisplayInfo GetWheelInfo(WheelType wheelType)
        {
            return WheelImageInfos.Find(target => target.WheelType == wheelType);
        }
        
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
    }

    [System.Serializable]
    public class Step
    {
        //Holds array with 8 elements and shows warning box.
        [InfoBox("This array's element count must be 8.", InfoMessageType.Warning)]
        [ValidateInput(nameof(ValidateRewards))]
        public StepRewardInfo[] Rewards = new StepRewardInfo[8];

        public WheelType WheelType;

        private bool ValidateRewards(StepRewardInfo[] value)
        {
            if (Rewards is not { Length: 8 })
            {
                System.Array.Resize(ref Rewards, 8);
            }

            return value is { Length: 8 };
        }
    }

    [System.Serializable]
    public struct StepRewardInfo
    {
        public Reward Reward;
        
        [ShowIf(nameof(IsItemConsumable))]
        public int Amount;
        //You can add fallback reward amount for nonconsumable items.

        private bool IsItemConsumable()
        {
            var info = Reward.GetInventoryInfo();
            if (info.InventoryItemConsumeType == InventoryItemConsumeType.NonConsumable)
            {
                Amount = 1;
                return false;
            }
            
            return true;
        }
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