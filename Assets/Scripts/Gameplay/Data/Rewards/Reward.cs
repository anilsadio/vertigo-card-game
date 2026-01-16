using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Gameplay.Data.Interfaces;
using Gameplay.Data.Inventory;
using Gameplay.Data.Utils;
using UI.Menu;
using UI.RewardItems;
using UnityEngine;
using Utilities.Pool;
using Random = UnityEngine.Random;

namespace Gameplay.Data.Rewards
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "reward_info", menuName = "RewardSystem/Infos/RewardInfo", order = 1)]
    public abstract class Reward : BaseReward, IInventoryItemIDHolder, IAnimationRewardItemHolder
    {
        [field: SerializeField] public ObjectType AnimationObjectType { get; set; }
        public virtual BaseInventoryItemInfo GetInventoryInfo()
        {
            return InventoryItemCatalog.instance.GetInfo(ID);
        }       
        
        public virtual Sprite GetInventoryIcon(InventoryIconType iconType = InventoryIconType.GameIcon)
        {
            return InventoryItemCatalog.instance.GetInfo(ID).GetIcon(iconType);
        }

        public virtual async UniTask MoveRewardToTargetAnimation(int itemCount, RectTransform startTransform, RectTransform targetTransform, Action onArrived = null)
        {
            List<RewardAnimationUIItem> rewardItems = new ();
            List<UniTask> animationTasks = new List<UniTask>();
            for (int i = 0; i < itemCount; i++)
            {
                RewardAnimationUIItem rewardItem = PoolFactory.Instance.GetObject<RewardAnimationUIItem>(AnimationObjectType, MenuController.Instance.transform);
                rewardItem.Initialize(this);
                rewardItem.transform.position = startTransform.position;
                
                Canvas.ForceUpdateCanvases();  
                
                Vector3 _startPos = startTransform.TransformPoint(targetTransform.rect.center);
                Vector3 _targetPos = targetTransform.TransformPoint(targetTransform.rect.center);
                
                animationTasks.Add(rewardItem.MoveToTarget(_startPos, _targetPos));
            }
            await UniTask.WhenAll(animationTasks);
            onArrived?.Invoke();
        }
    }
    
    [System.Serializable]
    public abstract class BaseReward : ScriptableObject
    {
        public virtual InventoryItemID ID => itemType.ToID(RewardType);
        public RewardType RewardType;
        public InventoryItemType itemType;
    }
}
