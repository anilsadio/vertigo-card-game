using System.Collections.Generic;
using Gameplay.Core;
using Gameplay.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class CardGamePanel : MonoBehaviour
    {
        [SerializeField] private WheelUIController wheelUIController;
        [SerializeField] private Button spinButton;

        private void Start()
        {
            MainEventHandler.OnCardGameStarted += OnCardGameStarted;
            MainEventHandler.OnSpinEnded += ShowRewardsAndMoveToList;
            spinButton.onClick.AddListener(StartSpin);
        }
        
        private void OnCardGameStarted(CardGameData gameData)
        {
            
        }

        private void StartSpin()
        {
            if (GameStatus.GameState == GameState.Playing)
                return;

            GameStatus.GameState = GameState.Playing;
            CardGameController.Instance.SetRandomRewardIndex();
            MainEventHandler.OnSpinStarted?.Invoke(GameStatus.CardGameCurrentStep);
        }

        private void ShowRewardsAndMoveToList(int rewardIndex )
        {
            // await movetoreward etc.
            CardGameController.Instance.ProceedStep();
        }
    }
}