using UnityEngine;

namespace Game.Entites.Actions
{
    /// <summary>
    /// Any melee attack falls into this category.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AttackAction<T> : ActionsBase where T : EntityDynamic
    {
        public T Entity_A;
        public T Entity_B;
        
        public AttackAction(T entity_a, T entity_b)
        {
            ActionId = "AttackAction";
            Entity_A = entity_a;
            Entity_B = entity_b;

            Do();
        }

        protected override void Do()
        {
            //todo do a guaranteed hit check here: >> if target has specific afflictions such as stuck, overburdened etc.
            //todo can add bonus damage for attacking from behind. but this also requires implementation to show what direction an entity looks.
            //first check hit chance
            var attackerStats = Entity_A.GetStats();
            var attackerDEX = attackerStats.DEX;
            var defenderDV = Entity_B.GetInventoryView().GetItemsDv();
            var defenderDex = Entity_B.GetStats().DEX;

            if (CombatHelper.DamageHitCheck(attackerDEX, defenderDV, defenderDex))
            {
                var attackerSTR = attackerStats.STR;
                //get attacker PV
                //get defender AV
              var appliedDmg = CombatHelper.DamageCalculator(2, attackerSTR, 2);
              
              //todo send this damage to the target, update HP. Kill if HP<0 ;
            }
            else
            {
                Debug.Log("Attack misses!");
            }
            
            base.Do();
        }
    }
}
