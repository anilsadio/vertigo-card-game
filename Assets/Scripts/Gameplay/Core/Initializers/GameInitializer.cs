using Game.Scripts.Utils;
using Gameplay.Data.Inventory;
using Gameplay.Data.Inventory.InventorySaveSystem;
using LiveEventService;
using UI.Menu;
using UnityEngine;

namespace Gameplay.Core.Initializers
{
    public class GameInitializer : SingletonBehaviour<GameInitializer>
    {
        [Header("Main Controllers")] 
        [SerializeField] private LiveEventSystem liveEventSystem;
        [SerializeField] private MenuController menuController;

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            _ = InventoryItemCatalog.instance; //Creating instance early. Because game will use the catalog a lot.
            liveEventSystem.Initialize();
            InventoryService.InitializeInventory();
            menuController.Initialize();
        }
    }
}
