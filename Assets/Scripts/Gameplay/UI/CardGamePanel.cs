using System;
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
            // MainEventHandler.OnSpinEnded += ShowRewardsAndMoveToList;
            spinButton.onClick.AddListener(StartSpin);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartSpin();
            }
        }

        private void OnCardGameStarted(CardGameData gameData)
        {
            
        }

        private void StartSpin()
        {
            if (GameStateHolder.GameState == GameState.Playing)
                return;

            Debug.Log("Spin Started. GameState is " + GameStateHolder.GameState.ToString());
            GameStateHolder.GameState = GameState.Playing;
            CardGameController.Instance.SetRandomRewardIndex();
            MainEventHandler.OnSpinStarted?.Invoke(GameStateHolder.CardGameRewardIndex);
        }

        private void ShowRewardsAndMoveToList()
        {
            // await reward animations etc.
            CardGameController.Instance.ProceedStep();
        }
    }
}