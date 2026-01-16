using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Gameplay.Core;
using Gameplay.Data;
using Gameplay.Data.Utils;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Pool;

namespace UI.WheelGame
{
    public class WheelGameStepUIController : MonoBehaviour
    {
        private const float MOVE_DURATION = 0.7f;
        [SerializeField] private RectTransform content; // HorizontalLayoutGroup olan
        [SerializeField] private RectTransform cursor; // Sabit imleç
        [SerializeField] private StepZoneUILayout superZoneLayout;
        [SerializeField] private StepZoneUILayout safeZoneLayout;

        private List<TextMeshUIPoolObject> stepIndexTextList = new();

        public void Initialize()
        {
            MainEventHandler.OnWheelGameStarted += OnWheelGameStarted;
            MainEventHandler.OnStepProceeded += OnStepProceeded;
            MainEventHandler.OnWheelGameCompleted += OnWheelGameCompleted;
        }

        private void OnWheelGameStarted(WheelGameLiveEventData gameData)
        {
            for (int i = 0; i < gameData.StepList.Count; i++)
            {
                var stepText = PoolFactory.Instance.GetObject<TextMeshUIPoolObject>(ObjectType.StepText, content);
                stepIndexTextList.Add(stepText);
                stepIndexTextList[i].SetText((i + 1).ToString());
                stepIndexTextList[i].SetColor(gameData.GetWheelInfo(gameData.StepList[i].WheelType).TextColor);
            }
            
            SetWheelZoneLayout(gameData);
            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(content);
            MoveToIndex(0).Forget();
        }

        private void OnWheelGameCompleted(bool isWin)
        {
            MainEventHandler.OnWheelGameStarted -= OnWheelGameStarted;
            MainEventHandler.OnStepProceeded -= OnStepProceeded;
            MainEventHandler.OnWheelGameCompleted -= OnWheelGameCompleted;
            
            foreach (var text in stepIndexTextList)
            {
                text.ResetObject();
            }
            
            stepIndexTextList.Clear();
        }

        private void OnStepProceeded(WheelGameLiveEventData gameData)
        {
            if (GameStateHolder.WheelGameRewardIndex < stepIndexTextList.Count - 1)
            {
                MoveToIndexWithAnimation(GameStateHolder.WheelGameCurrentStep).Forget();
            }
            
            SetWheelZoneLayout(gameData);
        }

        private void SetWheelZoneLayout(WheelGameLiveEventData gameData)
        {
            //SuperZone
            if (gameData.StepList.TryFindInRange(GameStateHolder.WheelGameCurrentStep + 1, gameData.StepList.Count - 1, x => x.WheelType == WheelType.Gold, out int superZoneIndex))
            {
                superZoneLayout.gameObject.SetActive(true);
                superZoneLayout.Initialize((superZoneIndex + 1).ToString());
            }
            else
            {
                superZoneLayout.gameObject.SetActive(false);
            }
            
            //SafeZone
            if (gameData.StepList.TryFindInRange(GameStateHolder.WheelGameCurrentStep + 1, gameData.StepList.Count - 1, x => x.WheelType == WheelType.Silver, out int safeZoneIndex))
            {
                safeZoneLayout.gameObject.SetActive(true);
                safeZoneLayout.Initialize((safeZoneIndex + 1).ToString());
            }
            else
            {
                safeZoneLayout.gameObject.SetActive(false);
            }
        }

        private async UniTask MoveToIndexWithAnimation(int _index, float _moveDuration = 0.7f)
        {
            RectTransform target = content.GetChild(Mathf.Clamp(_index, 0, content.childCount - 1)) as RectTransform;

            // World pozisyonları
            float targetWorldX = target.position.x;
            float cursorWorldX = cursor.position.x;

            float diff = cursorWorldX - targetWorldX;

            Vector3 newPos = content.position;
            newPos.x += diff;

            await content.DOMoveX(newPos.x, MOVE_DURATION).SetEase(Ease.OutQuad).ToUniTask()
                .AttachExternalCancellation(content.GetCancellationTokenOnDestroy());
        }

        private async UniTask MoveToIndex(int _index, float _moveDuration = 0.7f)
        {
            RectTransform target = content.GetChild(Mathf.Clamp(_index, 0, content.childCount - 1)) as RectTransform;

            // World pozisyonları
            float targetWorldX = target.position.x;
            float cursorWorldX = cursor.position.x;

            float diff = cursorWorldX - targetWorldX;

            Vector3 newPos = content.position;
            newPos.x += diff;

            await content.DOMoveX(newPos.x, 0).SetEase(Ease.OutQuad).ToUniTask()
                .AttachExternalCancellation(content.GetCancellationTokenOnDestroy());
        }
    }
}