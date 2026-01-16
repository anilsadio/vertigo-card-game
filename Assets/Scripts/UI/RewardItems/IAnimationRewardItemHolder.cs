using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utilities.Pool;

namespace UI.RewardItems
{
    public interface IAnimationRewardItemHolder
    {
        [field: SerializeField] public ObjectType AnimationObjectType { get; set; }
        public UniTask MoveRewardToTargetAnimation(int itemCount, RectTransform startTransform, RectTransform targetTransform, Action onArrived = null);
    }
}
