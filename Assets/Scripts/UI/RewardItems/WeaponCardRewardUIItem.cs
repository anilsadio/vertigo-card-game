using DG.Tweening;
using Gameplay.Data.Inventory;
using Gameplay.Data.Inventory.InventorySaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.RewardItems
{
    public class WeaponCardRewardUIItem : BaseCardRewardUIItem
    {
        [SerializeField] private Image updateCategoryIcon;
        [SerializeField] private Image progressBar;

        public override void Initialize(BaseInventoryItemInfo inventoryItemInfo, string amount)
        {
            base.Initialize(inventoryItemInfo, amount);

            if (inventoryItemInfo is WeaponInventoryInfo info)
            {
                int _totalPointAmount = InventoryService.GetAmountOfItem(info.ID);
                rewardAmountText.text = _totalPointAmount.ToString() + "/" + info.UpdatePointAmount.ToString();
                var fillAmount = Mathf.Clamp((float)_totalPointAmount / (float)info.UpdatePointAmount, 0f, 1f);
                progressBar.DOFillAmount(fillAmount, 0.5f);
                updateCategoryIcon.sprite = info.FallbackReward.GetInventoryInfo().GetIcon();
            }
        }
    }
}