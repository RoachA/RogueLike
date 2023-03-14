using System;
using UnityEngine;

namespace Game.Entites.Actions
{
    /// <summary>
    /// base action class, this can be queued later on. because these are objects and can be registered in a dictionary etc
    /// for iterative resolve!
    /// </summary>
    public class ActionsBase
    {
        protected string ActionId = "AttackAction";
        protected DateTime ActionTriggerTime;
        protected EntityDynamic Actor;
   
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
