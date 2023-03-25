using System;
using UnityEngine;
using Object = System.Object;

namespace Game.Entites.Actions
{
    /// <summary>
    /// base action class, this can be queued later on. because these are objects and can be registered in a dictionary etc
    /// for iterative resolve!
    /// </summary>
    public interface IAction
    {
        public string ActionId{ get; set; }
        public string ActionVerb{ get; set; }
        protected DateTime ActionTriggerTime{ get; set; }
        protected EntityDynamic Actor { get; set; }
        protected Object Target{ get; set; }
   
        //cant allocate two times no? yes I need a different solution here. there can be one action at a time. hmm. perhaps make this a list.
        //or scatter actions through several frames... humhum. which would be more performance
        public static Action<IAction, EntityDynamic, Object, DateTime, string> _actionIsCompleteEvent;

        protected void Do();
        
        protected void ActionIsComplete<T>(T completedAction, EntityDynamic actor, Object target, string actionVerb) where T : IAction
        {
        }
    }
}
