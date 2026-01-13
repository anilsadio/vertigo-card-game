using System.Collections.Generic;
using Gameplay.Data;
using Gameplay.Data.Inventory;
using UnityEngine;

namespace Gameplay.Core
{
    public class GameInitializer : MonoBehaviour
    {
        [Header("Main Controllers")]
        [SerializeField] private CardGameController cardGameController;

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            _ = InventoryItemCatalog.instance;
            GameStatus.GameState = GameState.Started;
            MainEventHandler.OnGameStarted?.Invoke();
            cardGameController.Initialize();
        }
    }
}
