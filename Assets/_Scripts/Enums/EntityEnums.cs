using Unity.VisualScripting;

namespace Game.Entites
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
        meelee = 0,
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

    public enum classes
    {
        dallama = 0,
    }

    public enum EntityEquipSlots
    {
        head = 0,
        body = 1,
        leftHand = 2,
        rightHand = 3,
        back = 4,
        legs = 5,
        feet = 6,
        face = 7,
    }

    public enum EntityType
    {
        player = 0,
        npc = 1,
        item = 2,
        container = 3,
    }

    public enum ItemTypes
    {
        meeleeWeapons = 0,
        rangedWeapons = 1,
        wearable = 2,
        consumable = 3,
        throwable = 4,
        misc = 5,
        readable = 6,
        currency = 7,
        tool = 8,
        furniture = 9,
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



