using Game.Dice;

public static class CombatHelper
{
    /// <summary>
    /// what should I give it? what should it give back? I believe I need
    /// to extract main attack-def stats and run a series of formulae with
    /// dices and return a final result basically.
    /// </summary>
    /// <returns></returns>
    public static int DamageCalculator()
    {
        return 0;
    }
    
    public static bool DamageHitCheck(int attacker_dex, int defender_dv, int defender_dex)
    {
        const int baseDefense = 6;
        //Get base DV of the target todo not implemented yet. Should add a field for base DV coming from the entity itself.
        var defenderDvSum = baseDefense + defender_dv + defender_dex; // todo currently base dex, must be the added value (20+1) etc.
        var attackerAASum = DiceRollHelper.RollRegularDice(new Dice(20, 1)) + attacker_dex; //++ any hitbonus? laters.
        
        bool hitLands = attackerAASum > defenderDvSum;

        return hitLands;
    }
}
