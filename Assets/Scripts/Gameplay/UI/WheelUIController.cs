using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Gameplay.Core;
using Gameplay.Data;
using Gameplay.Data.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class WheelUIController : MonoBehaviour
    {
        [SerializeField] private List<RewardUIItem> rewardItems = new();

        [Header("References")] [SerializeField]
        private Image wheelImage;

        [SerializeField] private Image cursorImage;

        [Header("Wheel Settings")] [SerializeField]
        private const int SLOT_COUNT = 8;

        private const float SPIN_DURATION = 3f;
        private const int SPIN_AMOUNT = 3; // 2 fast + 1 slowing rotation

        private Tween spinTween;
        private WheelType wheelType = WheelType.Bronze;

        private void Awake()
        {
            MainEventHandler.OnCardGameStarted += Initialize;
            MainEventHandler.OnSpinStarted += OnSpinStarted;
            MainEventHandler.OnStepProceeded += OnStepProceeded;
        }

        private void Initialize(CardGameData gameData)
        {
            var i = 0;
            SetWheelType(gameData);

            foreach (var stepRewardsInfo in gameData.StepList[GameStateHolder.CardGameCurrentStep].Rewards)
            {
                if (stepRewardsInfo.Reward.GetInventoryInfo() != null)
                {
                    rewardItems[i].Initialize(stepRewardsInfo.Reward.GetInventoryInfo().GetIcon(),
                        $"x{stepRewardsInfo.Amount}", stepRewardsInfo.Reward.RewardType);
                }

                i++;
            }
        }

        private void OnStepProceeded(CardGameData gameData)
        {
            var i = 0;

            //Animate and wait animation

            wheelImage.rectTransform.DORotate(new Vector3(0, 0, 0), 0);
            SetWheelType(gameData);

            foreach (var stepRewardsInfo in gameData.StepList[GameStateHolder.CardGameCurrentStep].Rewards)
            {
                if (stepRewardsInfo.Reward.GetInventoryInfo() != null)
                {
                    rewardItems[i].Initialize(stepRewardsInfo.Reward.GetInventoryInfo().GetIcon(),
                        $"x{stepRewardsInfo.Amount}", stepRewardsInfo.Reward.RewardType);
                }

                i++;
            }
        }


        private async void OnSpinStarted(int rewardIndex)
        {
            Debug.Log("Spin Started");

            float startAngle = wheelImage.rectTransform.eulerAngles.z;

            float anglePerSlot = 360f / SLOT_COUNT;
            float targetAngle = rewardIndex * anglePerSlot;

            float finalRotation = 360f - targetAngle;

            float targetRotation = (360f * SPIN_AMOUNT) + finalRotation;
            Debug.Log("Chosen reward: " + CardGameController.Instance.GameData
                .StepList[GameStateHolder.CardGameCurrentStep].Rewards[rewardIndex].Reward.ID.hash);

            spinTween = wheelImage.rectTransform.DORotate(new Vector3(0, 0, -(startAngle + targetRotation)),
                    SPIN_DURATION, RotateMode.FastBeyond360)
                .SetEase(Ease.OutCubic);

            await spinTween.AwaitForComplete(cancellationToken: this.GetCancellationTokenOnDestroy());

            OnSpinEnded();
        }

        private void OnSpinEnded()
        {
            Debug.Log("Spin Ended");
            spinTween?.Kill();
            spinTween = null;
            CardGameController.Instance.SpinEnded();
        }


        private void SetWheelType(CardGameData gameData)
        {
            if (wheelType == gameData.StepList[GameStateHolder.CardGameCurrentStep].WheelType)
                return;

            //animate wheel type change
            var wheelInfo = gameData.GetWheelInfo(gameData.StepList[GameStateHolder.CardGameCurrentStep].WheelType);
            wheelImage.sprite = wheelInfo.WheelImage;
            cursorImage.sprite = wheelInfo.CursorImage;
            wheelType = gameData.StepList[GameStateHolder.CardGameCurrentStep].WheelType;
        }
    }
}