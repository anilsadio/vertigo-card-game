using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Gameplay.Core;
using Gameplay.Data;
using TMPro;
using UnityEngine.UI;
using Utilities.Pool;

namespace Gameplay.UI
{
    public class CardGameStepUIController : MonoBehaviour
    {
        private const float MOVE_DURATION = 0.7f;
        [SerializeField] private CardGamePanel cardGamePanel;
        [SerializeField] private RectTransform content; // HorizontalLayoutGroup olan
        [SerializeField] private RectTransform cursor; // Sabit imleç

        private List<TextMeshUIPoolObject> stepIndexTextList = new();

        private void Awake()
        {
            MainEventHandler.OnCardGameStarted += OnCardGameStarted;
            MainEventHandler.OnStepProceeded += OnStepProceeded;
            MainEventHandler.OnCardGameCompleted += OnCardGameCompleted;
        }

        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     MoveToIndexWithAnimation(GameStatus.CardGameRewardIndex).Forget();
            // }
        }

        private void OnCardGameStarted(CardGameData gameData)
        {
            for (int i = 0; i < gameData.StepList.Count; i++)
            {
                var stepText = PoolFactory.Instance.GetObject<TextMeshUIPoolObject>(ObjectType.StepText, content, Vector3.zero, Vector3.one);
                stepIndexTextList.Add(stepText);
                stepIndexTextList[i].SetText((i + 1).ToString());
                stepIndexTextList[i].SetColor(gameData.GetWheelInfo(gameData.StepList[i].WheelType).TextColor);
            }

            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(content);
            MoveToIndex(0).Forget();
        }

        private void OnCardGameCompleted(bool isWin)
        {
            MainEventHandler.OnCardGameStarted += OnCardGameStarted;
            MainEventHandler.OnStepProceeded += OnStepProceeded;
            foreach (var text in stepIndexTextList)
            {
                PoolFactory.Instance.ResetObject(text, false);
            }
        }

        private void OnStepProceeded(CardGameData gameData)
        {
            if (GameStateHolder.CardGameRewardIndex < stepIndexTextList.Count - 1)
            {
                MoveToIndexWithAnimation(GameStateHolder.CardGameCurrentStep).Forget();
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