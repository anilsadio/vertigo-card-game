“In this case study, I can say that I focused mostly on the inventory system and tried to design a data structure with no limits, aligned with design patterns.

Rather than treating the Wheel of Fortune mechanic as a core gameplay feature, I handled it as a live event within the game and built a live event system that allows creating many similar events.

Things I wanted to implement but couldn’t due to time constraints of Case Study: an addressable asset system, a command queue structure (to control menus, pop-ups, and gameplay), and a scope structure (to ensure clean transitions between menu and gameplay without leaving any related remnants behind).”


<img width="1245" height="740" alt="code-structure" src="https://github.com/user-attachments/assets/2014b239-319d-488a-8836-5e1f2e150201" />


DETAILED STRUCTURE NOTES:

MAIN EVENT HANDLER
Holds all the main actions that decisive for gameplay and menu.

INVENTORY SYSTEM

Save System
  - InventoryService: Loads and saves the inventory datas. Serializes data to save in playerprefs. Designed to be integrated with backend services.
  - InventoryData: Holds item identifications and amounts. It can easily serialize and deserialize.

Inventory Data Structure
The inventory info system is designed to support an unlimited number of inventory items. It is modular, and any desired features can be added through derived classes.
  - InventoryDataInfo/BaseInventoryDataInfo: It is and scriptable object that have Inventories' unique ids, icons, consume types, fallback rewards, etc.
  - Inventory that inherited InventoryDataInfo: Armor, Coin, Money, Item Point, Utility, Weapon
  - Inventory Item Catalog: Holds all inventory item infos.
  - InventoryItemID: Unique id struct. Defines all the inventory items seperately. It helps to save inventory progress easily.
  - IInventoryItemIDHolder: Interface that defines inventories that have unique id.


LIVE EVENT SERVICE

Live Event System: 
It initializes all Live Events that meet the required conditions for activation, and all Live Event management is handled from here.
Base Live Event: Abstract scriptable object class that holds the live events' base features such as Live Event Data. You can create live event controllers and initialize the event through deriving and also you can hold all the data that belong to live event.

WheelGameLiveEvent: Controls the Wheel of Fortune live event. Derives BaseLiveEvent.
Live Event Config: Config that holds dates, event type, activation infos, etc.
Live Event Catalog: Provides all the live events to Live Event System. This scriptable object holds live events that meet the required conditions for activation.

REWARD SYSTEM
It is designed to generate configs more efficiently by holding lighter data, enabling faster performance without causing memory issues. Unlike InventoryItemInfo, it does not store any objects such as sprites or prefabs. It only contains enums, and thanks to these enums, the configs we create hold much smaller data, through which we can access the item’s InventoryItemInfo. We can create a reward-type counterpart for all inventory items and use them in reward configs.

- Reward/BaseReward
- IAnimationRewardItemHolder


OBJECT POOL SYSTEM
Instead of instantiating and destroying objects repeatedly (which is costly in terms of CPU and memory), the object pool creates a set of objects in advance and reuses them when needed. When an object is no longer in use, it is returned to the pool rather than being destroyed. This reduces garbage collection overhead, improves runtime performance, and helps prevent memory spikes.

Pool Factory: Holds all the items that you want to pool.

IPoolable: It is an interface that allows the system to recognize that an object is a pooled object and enables deriving its properties.

UI SYSTEM
It is not the system I desired. There is primitive UI structure to play the game comfortably. Unfortunately, I have a limited time. In case of crafting real project, I would have designed an addressable structure that have load and unload pop ups and panels. Also I would have designed a command system that handle menu flows, collected flows, etc.

Menu Controller: It sends requests to LiveEventService and creates live event buttons that activated.

BaseMenuButton/WheelGameMenuButton: Buttons that can start the live event popups, offers, etc.

WheelGamePanel: Holds all the Wheel game ui items and manage them.
      WheelGameStepUIController: Manages step counter that top of the screen.
      GainedRewardPanel: Manages the gained reward in Wheel of fortune.
      WheelUIController: Handles animations, spin button, etc.

CollectedRewardsPanel: Appears if the user completes successfully and shows all the items that user gained.

FailBombPanel: Appears when the bomb chosen in Wheel of Fortune. You can revive or quit.

RECORDINGS

20:9
<img width="718" height="324" alt="20 9" src="https://github.com/user-attachments/assets/104f039a-0469-4616-9805-43abf4f51c07" />

16:9 

<img width="720" height="405" alt="16 9" src="https://github.com/user-attachments/assets/25bfb31b-7abf-4257-adc0-c8a03f1a4c06" />

4:3

<img width="715" height="541" alt="4 3" src="https://github.com/user-attachments/assets/28f494c6-a292-4541-b5c7-b1e557b23120" />

VIDEO

https://github.com/user-attachments/assets/9c0bb358-4ddd-4a5a-bcca-fe0a70a8e93e


