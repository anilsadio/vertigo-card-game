namespace Gameplay.Data.Inventory
{
    public enum InventoryItemType
    {
        FailBomb = -1,
        Currency = 0,
        Weapon = 1,
        Utility = 2,
        Armor = 3,
        ItemPoint = 4
    }
    public enum CurrencyType
    {
        Coin = 0,
        Money = 1
    }
    public enum ItemPointType
    {
        RiflePoint = 0,
        ShotgunPoint = 1,
        SmgPoint = 2,
        SniperPoint = 3,
        PistolPoint = 4,
        MeleePoint = 5,
        ArmorPoint = 6,
    }
    public enum WeaponCategory
    {
        Primary = 0,
        Secondary = 1,
        Melee = 2,
    }
    public enum WeaponType
    {
        Rifle = 0,
        Shotgun = 1,
        Smg = 2,
        Sniper = 3,
        Pistol = 4,
        Melee = 5,
        Special = 6
    }

    public enum WeaponName
    {
        BayonetSummerVice = 0,
        Knife = 1,
        Galil = 2,
        P90 = 3,
        Mag7 = 4,
        Xm = 5,
        Awm = 6,
        BayonetEasterTime = 7
    }
    public enum UtilityType
    {
        HealthKit = 0,
        Explosive = 1,
        Tactical= 2
    }

    public enum UtilityName
    {
        M26 = 0,
        M67 = 1,
        Neurostim = 2,
        Regenerator = 3,
        Molotov = 4
    }
    public enum ArmorType
    {
        Chest = 0,
        Helmet = 1,
        Boots = 2,
        Gloves = 3
    }

    public enum ArmorName
    {
        Rayban = 0,
        BunnyHat = 1,
        HalloweenPumpkin = 2
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
