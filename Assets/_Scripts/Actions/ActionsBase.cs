using System;
using UnityEngine;
using Object = System.Object;

namespace Game.Entites.Actions
{
    /// <summary>
    /// base action class, this can be queued later on. because these are objects and can be registered in a dictionary etc
    /// for iterative resolve!
    /// </summary>
    public class ActionsBase
    {
        protected string ActionId = "AttackAction";
        protected string ActionVerb = "";
        protected DateTime ActionTriggerTime;
        protected EntityDynamic Actor;
        protected Object Target;
   
        protected Action<ActionsBase, EntityDynamic, Object, DateTime> _actionIsCompleteEvent;

        protected virtual void Do()
        {
            ActionIsComplete(this, Actor, Target);
        }

        protected virtual void SetTriggerTime()
        {
            ActionTriggerTime = DateTime.Now;
        }
        
        protected virtual void ActionIsComplete<T>(T completedAction, EntityDynamic actor, Object target) where T : ActionsBase
        {
            var time = DateTime.Now;
            Debug.Log(actor.GetType().Name + ActionVerb + target.GetType().Name);
            _actionIsCompleteEvent?.Invoke(completedAction, actor, target, time);
        }
    }
}
