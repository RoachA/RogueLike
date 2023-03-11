using Unity.VisualScripting;

namespace Game.Entites
{

    public enum BaseStatTypes
    {
        LVL = 0, // Level
        EXP = 1, // Experience
        HP = 2, // Hit Points
        MHP = 3, // Max Hit Points
        ATK = 4, // Physical Attack
        AV = 5, // Armor Value
        MAT = 6, // Magic Attack
        MDF = 7, // Magic Defense
        DV = 8, // DodgeValue
        RES = 9, // Status Resistance
        SPD = 10, // Speed
        MOV = 11, // Movement Speed
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

    public enum EntityEquipSlots
    {
        head = 0,
        body = 1,
        leftHand = 2,
        rightHand = 3,
        back = 4,
        legs = 5,
        feet = 6,
    }

    public enum EntityType
    {
        player,
        npc,
        item,
        container,
    }
}



