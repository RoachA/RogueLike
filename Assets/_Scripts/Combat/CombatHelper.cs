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
        var avRollTotal = 0;
        var pvOutput = 0;
        var AvDice = new Dice(defender_av, 1);
        var penetrationTimes = 0;
        var tripletHits = 0;

        tripletHits = RollTriplets();

        int RollTriplets(int deduction = 0)
        {
            tripletHits = 0;
            var pvRollTotal = 0;
            
            for (int i = 0; i < rolls; i++) // i = singlet
            {
                avRollTotal += DiceRollHelper.RollRegularDice(AvDice);

                var pvDice = new Dice(atacker_pv - deduction, 1);
                
                var pvRoll = DiceRollHelper.RollRegularDice(pvDice) - pvConstSubtract;
                pvRollTotal += pvRoll;
                
                while (pvRoll == pvDice.D - 2)
                {
                    pvRoll = DiceRollHelper.RollRegularDice(pvDice) - pvConstSubtract; //subtract here again? it affects PV rates greatly.
                    pvRollTotal += pvRoll;
                }

                if (pvRollTotal > avRollTotal)
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

        while (tripletHits == 3) // this would be called only on time extra
        {
            tripletHits = RollTriplets(-2);
            if (tripletHits > 0) penetrationTimes++;
        }
        
        //todo add base AV and PV to this equation.
        return penetrationTimes;
    }
    
    /// <summary>
    /// DONT USE FOR NOW xxxxxxxxxxxxxxxxxxxxxx
    /// </summary>
    /// <returns></returns>
    public static int DamageCalculatorDualMelee(int defender_av, int attacker_str, int attacker_pv, ItemMeleeWeaponEntity[] attacker_weapons)
    {
        const float secondHandDmgReduction = 0.5f;
     
        var penetrationTimes = CalculatePenetration(defender_av, attacker_pv);
        Vector2Int weapon_base_dmg_1 = new Vector2Int(0, 0);
        Vector2Int weapon_base_dmg_2 = new Vector2Int(0, 0);

        if (attacker_weapons[0] != null) //todo bare hand dmg should have something.
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
    
    public static DamageOutput DamageCalculatorSingleMelee(int defender_av, int attacker_str, int attacker_pv, ItemMeleeWeaponEntity attacker_weapons, float multiplier = 1)
    {
        //get defenders armor value >> all equipped AV, additonal AV if any <auras etc>, shield bonus AV
        //get attackers penetration value >> comes from str and weapon bonus 
        //(weapon bonus has a str multiplier)!!! can be done here. get STR ???.
        //+1 for sharp bonus if weapon has one
        //+4 if target is sleeping
        //additional pv bonus from skill usage <charge, lunge etc.> right?

        var penetrationTimes = 0;
        int dmg_output = 0;
        Vector2Int weapon_base_dmg_1 = new Vector2Int(0, 0);

        if (attacker_weapons != null) //todo bare hand dmg should have something.
        {
            var weaponStats = attacker_weapons.GetItemData<MeleeWeaponData>().Stats;
            weapon_base_dmg_1 = new Vector2Int(weaponStats.BaseDmg.D, weaponStats.BaseDmg.N);
            penetrationTimes = CalculatePenetration(defender_av, attacker_pv + (weaponStats.ArmorPenetration - 4));
        }
        
        for (int i = 0; i < penetrationTimes; i++)
        {
            dmg_output += (int) (DiceRollHelper.RollRegularDice(new Dice(weapon_base_dmg_1.x, weapon_base_dmg_1.y)) * multiplier);
        }
        
        DamageOutput output = new DamageOutput(dmg_output, penetrationTimes);
        
        return output;
    }

    public struct DamageOutput
    {
        public int DamageTotal;
        public int PenetrationTotal;

        public DamageOutput(int damageTotal, int penetrationTotal)
        {
            DamageTotal = damageTotal;
            PenetrationTotal = penetrationTotal;
        }
    }
}
