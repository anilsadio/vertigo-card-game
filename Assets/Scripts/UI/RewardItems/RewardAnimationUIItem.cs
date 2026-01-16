using Cysharp.Threading.Tasks;
using DG.Tweening;
using Gameplay.Data.Inventory;
using Gameplay.Data.Rewards;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Pool;

namespace UI.RewardItems
{
    [RequireComponent(typeof(Image))]
    public class RewardAnimationUIItem : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public ObjectType Type { get; set; }
        public bool IsInThePool { get; set; }
        [SerializeField] private Image rewardIcon;

        private Sequence sequence;
        
        public void Initialize(Reward reward)
        {
            rewardIcon.sprite = reward.GetInventoryIcon(InventoryIconType.AnimationIcon);
        }

        public async UniTask MoveToTarget(Vector3 startPosition, Vector3 targetPosition)
        {
            sequence?.Kill();
            sequence = DOTween.Sequence();
            
            transform.localScale = Vector3.zero;
            
            Vector2 randomPos = Random.insideUnitCircle;
            Vector2 randomPosition = new(randomPos.x * Random.Range(-100f, 100f), randomPos.y * Random.Range(-100f, 100f));
            transform.position = startPosition + new Vector3(randomPosition.x, randomPosition.y, 0);
            
            sequence.AppendInterval(Random.Range(0.1f, 0.3f));
            sequence.Append(transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutQuad));
            sequence.Append(transform.DOMove(targetPosition, 0.75f).SetEase(Ease.InBack));
            sequence.Join(transform.DOScale(Vector3.zero, 0.35f).SetDelay(0.5f).SetEase(Ease.InQuint));
            
            await sequence.ToUniTask(cancellationToken: this.GetCancellationTokenOnDestroy());
        }

        public void OnSpawn()
        {
        }

        public void OnReset()
        {
        }
    }
}