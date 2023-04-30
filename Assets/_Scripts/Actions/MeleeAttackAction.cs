using UnityEngine;
using System;
using Game.Data;
using Game.Dice;

namespace Game.Entities.Actions
{
    /// <summary>
    /// Any melee attack falls into this category.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MeleeAttackAction<T> : IAction where T : EntityDynamic
    {
        public T Entity_A;
        public T Entity_B;
        private DateTime _actionTriggerTime;
        private EntityDynamic _actor;
        public static Action<EntityDynamic, Vector2Int> AttackHappenedEvent;
        public static Action<EntityDynamic, int> DamageDealtEvent;

        public MeleeAttackAction(T entity_a, T entity_b)
        {
            Entity_A = entity_a;
            Entity_B = entity_b;

            Do();
        }

        public string ActionId { get; set; }
        public string ActionVerb { get; set; }
        DateTime IAction.ActionTriggerTime
        {
            get => _actionTriggerTime;
            set => _actionTriggerTime = value;
        }
        EntityDynamic IAction.Actor
        {
            get => _actor;
            set => _actor = value;
        }
        
        public void Do()
        {
            //todo do a guaranteed hit check here: >> if target has specific afflictions such as stuck, overburdened etc.
            //todo can add bonus damage for attacking from behind. but this also requires implementation to show what direction an entity looks.
            //first check hit chance
            //todo these parts are a bit annoying to rewrite for each offensive skill.. maybe could use a moderator class to have it easier. idk. 
            //or a method here in attack action class would be inherited from other classes of offense. ?
            
            AttackHappenedEvent?.Invoke(Entity_A, Entity_B.GetEntityPos());
            
            var attackerStats = Entity_A.GetStats();
            var attackerDEX = attackerStats.DEX;
            var defenderDV = Entity_B.GetInventoryView().GetItemsDv();
            var defenderDex = Entity_B.GetStats().DEX;
            var attackerWeapons = Entity_A.GetEquippedWeapons();
            int appliedTotalDmg = 0;

            if (CombatHelper.DamageHitCheck(attackerDEX, defenderDV, defenderDex))
            {
                var attackerSTR = attackerStats.STR;
                //get attacker PV >>>
                //get defender AV >>>
                
                CombatHelper.DamageOutput appliedDmg_1 = CombatHelper.DamageCalculatorSingleMelee(2, attackerSTR, 10, attackerWeapons[0]);
                CombatHelper.DamageOutput appliedDmg_2 = CombatHelper.DamageCalculatorSingleMelee(2, attackerSTR, 2, attackerWeapons[1], 0.5f);
                appliedTotalDmg = appliedDmg_1.DamageTotal + appliedDmg_2.DamageTotal;
                
                if (attackerWeapons[0] != null)
                    SendAttackLog(Entity_A, Entity_B, appliedDmg_1.PenetrationTotal, appliedDmg_1.DamageTotal, attackerWeapons[0]);
                
                if (attackerWeapons[1] != null)
                    SendAttackLog(Entity_A, Entity_B, appliedDmg_2.PenetrationTotal, appliedDmg_2.DamageTotal, attackerWeapons[1]);
                
                //todo send this damage to the target, update HP. Kill if HP<0 ;
            }

            if (appliedTotalDmg > 0)
                ApplyDamageTo(Entity_B, appliedTotalDmg);
        }

        public void ApplyDamageTo(EntityDynamic target, int dmg)
        {
            DamageDealtEvent?.Invoke(target, dmg);
        }

        public void SendAttackLog(EntityDynamic attacker, EntityDynamic defender, int penetrationTimes, int damageOutput, IInventoryItem weaponScriptableItemData)
        {
            MeleeWeaponScriptableData weaponScriptableData = weaponScriptableItemData.GetItemData<MeleeWeaponScriptableData>();
            Game.Dice.Dice weaponDmg = weaponScriptableData.Stats.BaseDmg;
            string wpnDmg = DiceRollHelper.GetDiceAsString(weaponDmg);
            string penetrationTimesString = "(x" + penetrationTimes.ToString() + ")";
            string hitText = " hits ";
            string missText = " misses ";

            switch (penetrationTimes)
            {
                case 1:
                    penetrationTimesString = "<color=\"yellow\">" + penetrationTimesString + "</color>";
                    break;
                case 2:
                    penetrationTimesString = "<color=\"orange\">" + penetrationTimesString + "</color>";
                    break;
                case >=3:
                    penetrationTimesString = "<color=\"purple\">" + penetrationTimesString + "</color>";
                    break;
                default:
                    penetrationTimesString = penetrationTimesString;
                    break;
            }
           
            string attackerName = "<color=\"red\">" + attacker.GetDefinitionData()._entityName + "</color>";
            string defenderName = "<color=\"red\">" + defender.GetDefinitionData()._entityName + "</color>";

            if (attacker.GetType() == typeof(EntityPlayer))
            {
                attackerName = "<color=\"green\">You</color>";
                hitText = " hit ";
                missText = " miss ";
            }
            
            if (defender.GetType() == typeof(EntityPlayer))
                defenderName = "<color=\"green\">you</color>";

            string finalOutput = attackerName + missText + defenderName;

            if (penetrationTimes > 0)
            {
                finalOutput = attackerName + hitText + defenderName + " " + penetrationTimesString + " for " + damageOutput + " damage with a " +
                           weaponScriptableData._itemName + " ->" + weaponScriptableData.Stats.ArmorPenetration + " " + wpnDmg + "!";
            }
            
            ActionHelper.SendActionLog(finalOutput);
        }
    }
}
