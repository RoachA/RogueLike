namespace Game.Entites.Actions
{
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
            Entity_A.GetEquippedWeapons();
            base.Do();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
