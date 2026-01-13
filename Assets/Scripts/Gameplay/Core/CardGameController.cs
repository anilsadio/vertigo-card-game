using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Utils;
using Gameplay.Core;
using Gameplay.Data;
using UnityEngine;

public class CardGameController : SingletonBehaviour<CardGameController>
{
    [SerializeField] private CardGameData gameData;
    private const int SLOT_COUNT = 8;

    public void Initialize()
    {
        GameStatus.CardGameCurrentStep = 0;
        MainEventHandler.OnCardGameStarted?.Invoke(gameData);
    }

    public void SpinEnded()
    {
        MainEventHandler.OnSpinEnded?.Invoke(GameStatus.CardGameCurrentStep);
    }
    
    public void ProceedStep()
    {
        GameStatus.GameState = GameState.Waiting;
        GameStatus.CardGameCurrentStep++;
        MainEventHandler.OnStepProceeded?.Invoke(new List<StepRewardInfo>(gameData.StepList[GameStatus.CardGameCurrentStep].Rewards));
    }

    public void SetRandomRewardIndex()
    {
        GameStatus.CardGameRewardIndex = Random.Range(0, SLOT_COUNT);
    }
}