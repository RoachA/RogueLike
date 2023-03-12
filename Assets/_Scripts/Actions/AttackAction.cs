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
            //first check hit chance
            var attackerDEX = Entity_A.GetStats().DEX;
            var defenderDV = Entity_B.GetInventoryView().GetItemsDv();
            var defenderDex = Entity_B.GetStats().DEX;

            if (CombatHelper.DamageHitCheck(attackerDEX, defenderDV, defenderDex))
            {
                //check dmg/penetration output
            }
            else
            {
                Debug.Log("Attack misses!");
            }
            base.Do();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
