using System.Collections.Generic;
using LiveEventService.Config;
using TMPro;
using UnityEngine;

namespace LiveEventService
{
    [CreateAssetMenu(fileName = "live_event_catalog", menuName = "Live Event System/Live Event Catalog", order = 1)]
    public class LiveEventCatalog : ScriptableObject
    {
        [field: SerializeField]public List<BaseLiveEventConfig> configList { get; set;}
        
        public List<BaseLiveEventConfig> GetActiveLiveEvents()
        {
            var activeEvents = new List<BaseLiveEventConfig>();
            foreach (var config in configList)
            {
                if (config.IsActive)
                {
                    activeEvents.Add(config);
                }
            }
            
            return activeEvents;
        }
    }
}
