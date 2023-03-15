using Game.Dice;
using Game.Entites;
using Game.Entites.Data;
using UnityEngine;

/// <summary>
/// https://www.notion.so/Combat-System-Examples-e766b21b971a493da01bdcbdb1f52e17
/// </summary>
public static class CombatHelper
{
    public static bool DamageHitCheck(int attacker_dex, int defender_dv, int defender_dex)
    {
        const int baseDefense = 6;
        //Get base DV of the target todo not implemented yet. Should add a field for base DV coming from the entity itself.
        var defenderDvSum = baseDefense + defender_dv + defender_dex; // todo currently base dex, must be the added value (20+1) etc.
        var attackerAASum = DiceRollHelper.RollRegularDice(new Dice(20, 1)) + attacker_dex; //++ any hitbonus? laters.
        
        bool hitLands = attackerAASum > defenderDvSum;
        Debug.LogWarning(hitLands ? "hit lands!" : "miss!");

        return hitLands;
    }
    
    /// <summary>
    /// Return physical penetration value of a meelee attack depending on AV and PV of the initiators.
    /// Base dmg of a weapon is applied x times, where x is the penetration output of this method.
    /// </summary>
    /// <param name="defender_av">defender's base armor value</param>
    /// <param name="atacker_pv">atacker's base penetration value</param>
    /// <returns></returns>
    public static int CalculatePenetration(int defender_av, int atacker_pv)
    {
        const int rolls = 3;
        const int pvConstSubtract = 2;
        var avOutput = 0;
        var pvOutput = 0;
        var dice = new Dice(10, 1);
        var penetrationTimes = 0;
        var tripletHits = 0;

        RollTriplets();

        int RollTriplets(int deduction = 0)
        {
            tripletHits = 0;
             
            for (int i = 0; i < rolls; i++)
            {
                avOutput += DiceRollHelper.RollRegularDice(dice);

                var extraPvRolls = 0;
                var pvRoll = DiceRollHelper.RollRegularDice(dice) - pvConstSubtract;
                pvRoll -= deduction;

                while (pvRoll == dice.D)
                {
                    extraPvRolls +=
                        DiceRollHelper.RollRegularDice(dice) -
                        pvConstSubtract; //subtract here again? it affects PV rates greatly.
                    
                    pvRoll -= deduction;
                }

                pvOutput = extraPvRolls + pvRoll;

                if (pvOutput > avOutput)
                    tripletHits++;
            }

            return tripletHits;
        }

        if (tripletHits > 0)
        {
            penetrationTimes++;
        }

        if (tripletHits == 0)
        {
            return penetrationTimes;
        }

        if (tripletHits == 3)
        {
            RollTriplets(-2);
        }


        return penetrationTimes;
    }
    
    /// <summary>
    /// what should I give it? what should it give back? I believe I need
    /// to extract main attack-def stats and run a series of formulae with
    /// dices and return a final result basically.
    /// </summary>
    /// <returns></returns>
    public static int DamageCalculator(int defender_av, int attacker_str, int attacker_pv, ItemMeleeWeaponEntity[] attacker_weapons)
    {
        //get defenders armor value >> all equipped AV, additonal AV if any <auras etc>, shield bonus AV
        //get attackers penetration value >> comes from str and weapon bonus 
        //(weapon bonus has a str multiplier)!!! can be done here. get STR ???.
        //+1 for sharp bonus if weapon has one
        //+4 if target is sleeping
        //additional pv bonus from skill usage <charge, lunge etc.> right?
        const float secondHandDmgReduction = 0.5f;
     
        var penetrationTimes = CalculatePenetration(defender_av, attacker_pv);
        Vector2Int weapon_base_dmg_1 = new Vector2Int(0, 0);
        Vector2Int weapon_base_dmg_2 = new Vector2Int(0, 0);

        if (attacker_weapons[0] != null)
        {
            weapon_base_dmg_1 = new Vector2Int(attacker_weapons[0].GetItemData<MeleeWeaponData>().Stats.BaseDmg.D, attacker_weapons[0].GetItemData<MeleeWeaponData>().Stats.BaseDmg.N);
        }

        if (attacker_weapons[1] != null)
        {
            weapon_base_dmg_2 =
                new Vector2Int(attacker_weapons[1].GetItemData<MeleeWeaponData>().Stats.BaseDmg.D,
                    attacker_weapons[1].GetItemData<MeleeWeaponData>().Stats.BaseDmg.N);
        }

        var weapon_dmg_1 = DiceRollHelper.RollRegularDice(new Dice(weapon_base_dmg_1.x, weapon_base_dmg_1.y + penetrationTimes));
        var weapon_dmg_2 = DiceRollHelper.RollRegularDice(new Dice(weapon_base_dmg_2.x, weapon_base_dmg_2.y + penetrationTimes)) * secondHandDmgReduction;

        int dmg_output = weapon_dmg_1 + (int) weapon_dmg_2;
        
        Debug.LogWarning(dmg_output + " dmg was dealt!");
        return dmg_output;
    }
}
