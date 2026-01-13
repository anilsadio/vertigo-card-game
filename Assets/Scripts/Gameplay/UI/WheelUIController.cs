using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Gameplay.Core;
using Gameplay.Data;
using Gameplay.Data.Inventory;
using UnityEngine;

namespace Gameplay.UI
{
    public class WheelUIController : MonoBehaviour
    {
        [SerializeField] private List<RewardUIItem> rewardItems = new();
        
        [Header("References")]
        [SerializeField] private RectTransform wheelTransform;

        [Header("Wheel Settings")]
        [SerializeField] private const int SLOT_COUNT = 8;
        [SerializeField] private const float SPIN_DURATION = 4f;
        [SerializeField] private const int SPIN_AMOUNT = 3; // 2 sabit + 1 yavaş

        private Tween spinTween;
        private WheelType wheelType;

        private void Awake()
        {
            MainEventHandler.OnCardGameStarted += InitializeRewards;
            MainEventHandler.OnSpinStarted += OnSpinStarted;
            MainEventHandler.OnStepProceeded += OnPassedNextStep;
        }

        private void InitializeRewards(CardGameData gameData)
        {
            var i = 0;
            foreach (var reward in gameData.StepList[GameStatus.CardGameCurrentStep].Rewards)
            {
                if (reward.Reward.GetInventoryInfo() != null)
                {
                    rewardItems[i].Initialize(reward.Reward.GetInventoryInfo().GetIcon(), $"x{reward.Amount}");
                }

                i++;
            }
        }
        
        private void OnPassedNextStep(List<StepRewardInfo> rewardList)
        {
            var i = 0;
            foreach (var reward in rewardList)
            {
                if (reward.Reward.GetInventoryInfo() != null)
                {
                    rewardItems[i].Initialize(reward.Reward.GetInventoryInfo().GetIcon(), $"x{reward.Amount}");
                }

                i++;
            }
        }
        
        private async void OnSpinStarted(int rewardIndex)
        {
            Debug.Log("Spin Started");
            
            spinTween?.Kill();

            float anglePerSlot = 360f / SLOT_COUNT;
            float targetAngle = rewardIndex * anglePerSlot;
            
            float finalRotation =
                (SPIN_AMOUNT * 360f) +
                (360f - targetAngle);

            spinTween = wheelTransform.DORotate(
                    new Vector3(0, 0, -finalRotation),
                    SPIN_DURATION,
                    RotateMode.FastBeyond360
                ).SetEase(Ease.OutCubic);

            await spinTween.AwaitForComplete(cancellationToken: this.GetCancellationTokenOnDestroy());
            OnSpinEnded();
        }
        
        private void OnSpinEnded()
        {
            Debug.Log("Spin Ended");
            spinTween = null;
            CardGameController.Instance.SpinEnded();
        }
    }
}