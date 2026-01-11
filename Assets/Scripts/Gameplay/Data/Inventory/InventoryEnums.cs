namespace Gameplay.Data.Inventory
{
    public enum InventoryItemType
    {
        Currency = 0,
        Weapon = 1,
        Utility = 2,
        Armor = 3,
        ItemPoint = 4
    }
    public enum CurrencyType
    {
        Coin = 0
    }
    public enum ItemPointType
    {
        WeaponPoint = 0,
        ArmorPoint = 1,
    }
    public enum WeaponCategory
    {
        Primary = 0,
        Secondary = 1,
        Melee = 2,
    }
    public enum PrimaryWeaponType
    {
        Rifle = 0,
        Shotgun = 1,
        Smg = 2,
        Sniper = 3,
        Special = 4
    }
    public enum SecondaryWeaponType
    {
        Pistol = 0,
        Special = 1
    }
    public enum MeleeWeaponType
    {
        Knife = 0,
        Axe = 1,
        Special = 2
    }
    public enum UtilityType
    {
        HealthKit = 0,
        Explosive = 1,
        Tactical= 2
    }
    public enum ArmorType
    {
        Chest = 0,
        Helmet = 1,
        Boots = 2,
        Gloves = 3
    }

    public enum InventoryIconType
    {
        GameIcon = 0,
        AnimationIcon = 1
    }

    public enum ItemClass
    {
        Rare = 0,
        Legendary = 1
    }
}
