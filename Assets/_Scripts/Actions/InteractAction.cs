using System;
using Game.Interfaces;

namespace Game.Entites.Actions
{
    public class InteractAction<T> : IAction where T : EntityDynamic
    {
        private DateTime _actionTriggerTime;
        private EntityDynamic _actor;
        private IInteractable _target;

        public string ActionId { get; set; }
        public string ActionVerb { get; set; }
        public static Action<string> LoggedInteractionEvent;

        public InteractAction(EntityDynamic actor, IInteractable target)
        {
            _target = target;
            _actor = actor;

            Do();
        }

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
          var log = _target.InteractWithThis(); // what is the interaction? what happens? is there a result? what is the result?
          log = _actor.GetDefinitionData()._entityName + log;
          SendInteractionLog(log);
        }

        public void SendInteractionLog(string log)
        {
            ActionHelper.ActionLogEvent?.Invoke(log);
        }
    }
}
