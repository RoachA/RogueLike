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
        public string ActionId = "AttackAction";
        public string ActionVerb = "attacks";
        protected DateTime ActionTriggerTime;
        protected EntityDynamic Actor;
        protected Object Target;
   
        //cant allocate two times no? yes I need a different solution here. there can be one action at a time. hmm. perhaps make this a list.
        //or scatter actions through several frames... humhum. which would be more performance
        public static Action<ActionsBase, EntityDynamic, Object, DateTime, string> _actionIsCompleteEvent;

        public ActionsBase()
        {
        }

        protected virtual void Do()
        {
            ActionIsComplete(this, Actor, Target, ActionVerb);
        }

        protected virtual void SetTriggerTime()
        {
            ActionTriggerTime = DateTime.Now;
        }
        
        protected virtual void ActionIsComplete<T>(T completedAction, EntityDynamic actor, Object target, string actionVerb) where T : ActionsBase
        {
            var time = DateTime.Now;
            Debug.Log(actor.GetType().Name + ActionVerb + target.GetType().Name);
            _actionIsCompleteEvent?.Invoke(completedAction, actor, target, time, actionVerb);
        }
    }
}
