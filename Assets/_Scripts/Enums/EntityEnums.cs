using Unity.VisualScripting;

namespace Game.Entities
{
    public enum SkillTypes
    {
        //todo castable skills must have their own class to inherit from. But such skills can be 
        //categorized as ranged, tile-select, meelee etc.
        //should we call them skills? or actionables? or perks? or moves?
    }

    public enum EntityDemeanor
    {
        docile = 0,
        natural = 1,
        hostile = 2,
        friendly = 3,
    }

    public enum EntityBehaviorTypes
    {
        melee = 0,
        ranged = 1,
        frenzy = 2,
        defensive = 3,
        weak = 4,
    }

    public enum Races
    {
        turk = 0,
        other = 1,
    }

    public enum Genders
    {
        male = 0,
        female = 1,
        gay = 2,
    }

    public enum Clans
    {
        nosferatu = 0,
        klavian = 1,
        gorgonian = 2,
    }

    public enum Classes
    {
        dallama = 0,
    }

    public enum EntityEquipSlots
    {
        Head = 0,
        Body = 1,
        LeftHand = 2,
        RightHand = 3,
        Back = 4,
        Legs = 5,
        Feet = 6,
        Face = 7,
    }

    public enum EntityType
    {
        player = 0,
        npc = 1,
        item = 2,
        container = 3,
    }
    

    public enum InventoryItemTypes
    {
        MeleeWeapons,
        RangedWeapons,
        Gears,
        Tools,
        Consumable,
        Books,
        Misc,
    }

    public enum DamageTypes
    {
        slashing = 0,
        piercing = 1,
        blunt = 2,
        magic = 3,
    }

    public enum AmmoTypes
    {
        basic = 0,
        advanced = 1,
    }
}



