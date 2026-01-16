using System.Collections.Generic;
using Game.Scripts.Utils;
using Gameplay.Core;
using LiveEventService;
using UI.Panel;
using UnityEngine;
using Utilities.Pool;

namespace UI.Menu
{
    public class MenuController : SingletonBehaviour<MenuController>
    {
        public RectTransform ButtonsLeftLayout;
        private List<BaseMenuButton> MenuButtons = new();
        [SerializeField] private WheelGamePanel wheelGamePanel;
        [SerializeField] private CollectedRewardPanel collectedRewardPanel;

        public void Initialize()
        {
            OnMenuOpened();
            MainEventHandler.OnMenuOpened += OnMenuOpened;
        }

        private void OnMenuOpened()
        {
            foreach (var liveEventConfig in LiveEventSystem.Instance.ActiveLiveEventConfigs)
            {
                var menuButton = PoolFactory.Instance.GetObject<BaseMenuButton>(liveEventConfig.MenuButtonType, ButtonsLeftLayout);
                menuButton.Initialize();
                MenuButtons.Add(menuButton);
            }
        }
        
        public void StartWheelGame()
        {
            foreach (var button in MenuButtons)
            {
                button.ResetObject();
            }
            wheelGamePanel.gameObject.SetActive(true);
            wheelGamePanel.Initialize();
        }
    }
}
