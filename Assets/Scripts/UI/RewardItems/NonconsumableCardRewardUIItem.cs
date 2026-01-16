using Gameplay.Data.Inventory;

namespace UI.RewardItems
{
    public class NonconsumableCardRewardUIItem : BaseCardRewardUIItem
    {
        public override void Initialize(BaseInventoryItemInfo inventoryItemInfo, string amount)
        {
            base.Initialize(inventoryItemInfo, amount);
            rewardAmountText.text = $"x{amount}";
        }
    }
}
