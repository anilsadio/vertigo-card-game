using Gameplay.Core;
using UnityEngine;

namespace LiveEventService.Config
{
    [CreateAssetMenu(fileName = "wheel_game_live_event_config", menuName = "Live Event System/Wheel Game Config", order = 1)]
    public class WheelGameLiveEventConfig : BaseLiveEventConfig
    {
        [field: SerializeField] public WheelGameLiveEvent WheelGameLiveEvent { get; set; }
        public override BaseLiveEvent LiveEvent => WheelGameLiveEvent;
    }
}
