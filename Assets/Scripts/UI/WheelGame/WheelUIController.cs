using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Gameplay.Core;
using Gameplay.Data;
using LiveEventService;
using UI.RewardItems;
using UnityEngine;
using UnityEngine.UI;

namespace UI.WheelGame
{
    public class WheelUIController : MonoBehaviour
    {
        private WheelGameLiveEvent liveEvent;
        private WheelGameLiveEvent LiveEvent
        {
            get
            {
                if (liveEvent == null)
                {
                    liveEvent = LiveEventSystem.Instance.GetLiveEvent<WheelGameLiveEvent>();
                }
                
                return liveEvent;
            }
        }
        
        [Header("References")] 
        [SerializeField] private List<RewardUIItem> rewardItems;
        [SerializeField] private Image wheelImage;
        [SerializeField] private Image cursorImage;

        [Header("Wheel Settings")] 
        private const int SLOT_COUNT = 8;
        private const float SPIN_DURATION = 3f;
        private const int SPIN_AMOUNT = 3; // 2 fast + 1 slowing rotation

        private Tween spinTween;
        private WheelType wheelType = WheelType.Bronze;

        public void Initialize()
        {
            MainEventHandler.OnWheelGameStarted += OnWheelGameStarted;
            MainEventHandler.OnSpinStarted += OnSpinStarted;
            MainEventHandler.OnSpinEnded += OnSpinEnded;
            MainEventHandler.OnStepProceeded += OnStepProceeded;
            MainEventHandler.OnWheelGameCompleted += OnWheelGameCompleted;
        }

        private void OnWheelGameStarted(WheelGameLiveEventData gameData)
        {
            var i = 0;
            SetWheelType(gameData);

            foreach (var stepRewardsInfo in gameData.StepList[GameStateHolder.WheelGameCurrentStep].Rewards)
            {
                if (stepRewardsInfo.Reward.GetInventoryInfo() != null)
                {
                    rewardItems[i].Initialize(stepRewardsInfo.Reward.GetInventoryInfo().GetIcon(),
                        $"x{stepRewardsInfo.Amount}", stepRewardsInfo.Reward.RewardType);
                    rewardItems[i].transform.DOKill();
                    rewardItems[i].transform.DOScale(Vector3.one, 0.2f);
                }

                i++;
            }
        }

        private void OnSpinEnded(RectTransform rectTransform)
        {
            foreach (var item in rewardItems)
            {
                item.transform.DOKill();
                item.transform.DOScale(Vector3.zero, 0.2f).SetDelay(0.25f);
            }
        }

        private void OnWheelGameCompleted(bool isWin)
        {
            MainEventHandler.OnWheelGameStarted -= OnWheelGameStarted;
            MainEventHandler.OnSpinStarted -= OnSpinStarted;
            MainEventHandler.OnStepProceeded -= OnStepProceeded;
            MainEventHandler.OnWheelGameCompleted -= OnWheelGameCompleted;
        }

        private void OnStepProceeded(WheelGameLiveEventData eventData)
        {
            int i = 0;

            wheelImage.rectTransform.DORotate(new Vector3(0, 0, 0), 0);
            SetWheelType(eventData);

            foreach (var stepRewardsInfo in eventData.StepList[GameStateHolder.WheelGameCurrentStep].Rewards)
            {
                if (stepRewardsInfo.Reward.GetInventoryInfo() != null)
                {
                    rewardItems[i].Initialize(stepRewardsInfo.Reward.GetInventoryInfo().GetIcon(),
                        $"x{stepRewardsInfo.Amount}", stepRewardsInfo.Reward.RewardType);
                    rewardItems[i].transform.DOKill();
                    rewardItems[i].transform.DOScale(Vector3.one, 0.2f);
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
            LiveEvent.SpinEnded(rewardItems[GameStateHolder.WheelGameRewardIndex].RectTransform);
        }


        private void SetWheelType(WheelGameLiveEventData eventData)
        {
            if (wheelType == eventData.StepList[GameStateHolder.WheelGameCurrentStep].WheelType)
                return;

            //animate wheel type change
            var wheelInfo = eventData.GetWheelInfo(eventData.StepList[GameStateHolder.WheelGameCurrentStep].WheelType);
            wheelImage.sprite = wheelInfo.WheelImage;
            cursorImage.sprite = wheelInfo.CursorImage;
            wheelType = eventData.StepList[GameStateHolder.WheelGameCurrentStep].WheelType;
        }
    }
}