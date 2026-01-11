using DG.Tweening;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class CardGameStepLayoutController : MonoBehaviour
    {
        [SerializeField] private RectTransform content; // HorizontalLayoutGroup olan
        [SerializeField] private RectTransform cursor; // Sabit imleç
        [SerializeField] private float moveDuration = 0.7f;
        [SerializeField] private int index = 0;

        private async void Start()
        {
            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(content);
            MoveToIndex(0 ,0).Forget();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MoveToIndex(index).Forget();
            }
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

            await content.DOMoveX(newPos.x, moveDuration).SetEase(Ease.OutQuad).ToUniTask().AttachExternalCancellation(content.GetCancellationTokenOnDestroy());
        }
    }
}