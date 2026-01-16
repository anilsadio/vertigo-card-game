using System.Collections.Generic;
using LiveEventService.Config;
using UnityEngine;
using Utilities.Singleton;

namespace LiveEventService
{
    public class LiveEventSystem : SingletonBehaviour<LiveEventSystem>
    {
        [SerializeField] private LiveEventCatalog catalog;
        public List<BaseLiveEventConfig> ActiveLiveEventConfigs { get; private set; }

        public void Initialize()
        {
            ActiveLiveEventConfigs = new List<BaseLiveEventConfig>(catalog.GetActiveLiveEvents());
            foreach (var config in ActiveLiveEventConfigs)
            {
                config.LiveEvent.Initialize();
            }
        }
        
        public T GetLiveEvent<T>() where T : class
        {
            foreach (var baseEvent in ActiveLiveEventConfigs)
            {
                if (baseEvent.LiveEvent is T castEventController)
                {
                    return castEventController;
                }
            }
            return null;
        }
        
    }
}
