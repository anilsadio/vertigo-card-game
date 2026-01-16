using UnityEngine;

namespace Utilities.Pool
{
        public enum ObjectType
        {
                [InspectorName("Items/UI/Reward/RewardUIItem")] RewardUIItem = 0,
                [InspectorName("Items/UI/StepText")] StepText = 1,
                [InspectorName("Items/UI/Reward/ClassicCardRewardUIItem")] NonconsumableCardRewardUIItem = 2,
                [InspectorName("Items/UI/Reward/WeaponCardRewardUIItem")] WeaponCardRewardUIItem = 3,
                [InspectorName("Items/UI/Reward/ArmorCardRewardUIItem")] ArmorCardRewardUIItem = 4,
                [InspectorName("Items/UI/LiveEvents/WheelGameMenuButton")] WheelGameMenuButton = 5,
        }
}
