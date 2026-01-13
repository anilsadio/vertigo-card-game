using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class RewardUIItem : MonoBehaviour
    {
        [SerializeField] private Image rewardIcon;
        [SerializeField] private TextMeshProUGUI rewardAmountText;

        public void Initialize(Sprite icon, string amount)
        {
            rewardIcon.sprite = icon;
            rewardAmountText.text = amount;
        }
    }
}
