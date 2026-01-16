using Cysharp.Threading.Tasks;
using DG.Tweening;
using Gameplay.Data.Rewards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Pool;

namespace UI.RewardItems
{
    public class RewardUIItem : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public ObjectType Type { get; set; }
        public RewardType RewardType;
        public RectTransform RectTransform;
        
        [SerializeField] private Image rewardIcon;
        [SerializeField] private TextMeshProUGUI rewardAmountText;
        public bool IsInThePool { get; set; }
        
        private Tween textUpdateTween;

        public void Initialize(Sprite icon, string amount, RewardType rewardType)
        {
            rewardIcon.sprite = icon;
            rewardAmountText.text = amount;
            RewardType = rewardType;
        }

        public void SetText(string amount)
        {
            rewardAmountText.text = amount;
        }
        public async UniTask SetAmountWithTween(int amount)
        {
            if (!int.TryParse(rewardAmountText.text, out int value))
            {
                rewardAmountText.text = amount.ToString();
                Debug.LogWarning("Text could not be parsed to integer value.");
                await UniTask.Yield();
            }
            
            textUpdateTween = DOTween.To(() => value, x => value = x, amount, 0.25f)
                .SetEase(Ease.Linear).OnUpdate(() =>
                {
                    rewardAmountText.text = value.ToString();
                });
            
            await textUpdateTween.AwaitForComplete(cancellationToken: this.GetCancellationTokenOnDestroy());
        }

        
        public void OnSpawn()
        {
            
        }

        public void OnReset()
        {
            transform.localScale = Vector3.one;
        }
    }
}
