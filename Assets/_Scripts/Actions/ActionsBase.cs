using System;
using UnityEngine;

namespace Game.Entites.Actions
{
    public class ActionsBase
    {
        protected string ActionId = "AttackAction";
        protected DateTime ActionTriggerTime;
   
        protected Action<ActionsBase, DateTime> _actionIsCompleteEvent;

        protected virtual void Do()
        {
            ActionIsComplete(this);
        }

        protected virtual void SetTriggerTime()
        {
            ActionTriggerTime = DateTime.Now;
        }
        
        protected virtual void ActionIsComplete<T>(T completedAction) where T : ActionsBase
        {
            var time = DateTime.Now;
            Debug.Log(completedAction.ActionId + " was complete! At time: " + time);
            _actionIsCompleteEvent?.Invoke(completedAction, time);
        }
    }
}
