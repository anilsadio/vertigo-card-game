
using System.Collections.Generic;
using Gameplay.Core;
using Gameplay.Data.Inventory;
using UI.RewardItems;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Pool;
using Button = UnityEngine.UI.Button;

public class CollectedRewardPanel : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Button claimButton;
    [SerializeField] private List<BaseCardRewardUIItem> cardRewardUIItems;
    
    public void Initialize(Dictionary<BaseInventoryItemInfo, int> GainedRewardInventory)
    {
        foreach (var inventoryInfo in GainedRewardInventory)
        {
            var cardUIItem = PoolFactory.Instance.GetObject<BaseCardRewardUIItem>(inventoryInfo.Key.CardUIItemType, scrollRect.content);
            cardRewardUIItems.Add(cardUIItem);
            cardUIItem.Initialize(inventoryInfo.Key, inventoryInfo.Value.ToString());
        }
        
        Canvas.ForceUpdateCanvases();
        scrollRect.horizontalNormalizedPosition = 0f;
        claimButton.onClick.AddListener(OnContinueButtonClicked);
    }

    private void OnContinueButtonClicked()
    {
        foreach (var baseCardRewardUIItem in cardRewardUIItems)
        {
            baseCardRewardUIItem.ResetObject();
        }
        claimButton.onClick.RemoveAllListeners();
        gameObject.SetActive(false);
        MainEventHandler.OnWheelGameClosed?.Invoke();
        MainEventHandler.OnMenuOpened?.Invoke();
    }
}
