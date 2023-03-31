using System;
using Game.Tiles;

namespace Game.Entites.Actions
{
    public sealed class WalkAction<T> : IAction where T : EntityDynamic
    {
        public TileBase TargetTile;
        private DateTime _actionTriggerTime;
        private EntityDynamic _actor;
    
        public WalkAction(T entity, TileBase targetTile)
        {
            ActionId = "Walk";
            ActionVerb = "walks through";
            _actor = entity;
            TargetTile = targetTile;

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

        void IAction.Do()
        {
            Do();
        }

        private void Do()
        {
            _actor.MoveEntityToTile(TargetTile);
            _actor.SetOccupiedTile(TargetTile);
            var floor = (TileFloor) TargetTile;
            floor.AddEntityToTile(_actor);
        }
    }
}