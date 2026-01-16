using Gameplay.Data.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Pool;

namespace UI.RewardItems
{
    public class BaseCardRewardUIItem : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public ObjectType Type { get; set; }
        
        [SerializeField] protected Image rewardIcon;
        [SerializeField] protected TextMeshProUGUI rewardNameText;
        [SerializeField] protected TextMeshProUGUI rewardAmountText;
        public bool IsInThePool { get; set; }

        public virtual void Initialize(BaseInventoryItemInfo inventoryItemInfo, string amount)
        {
            rewardIcon.sprite = inventoryItemInfo.GetIcon();
            rewardNameText.text = inventoryItemInfo.ItemName;
            //Classic scripti oluştur
        }
        
        public virtual void OnSpawn()
        {
            
        }

        public virtual void OnReset()
        {
            transform.localScale = Vector3.one;
        }
    }
}