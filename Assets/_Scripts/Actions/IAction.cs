using System;

namespace Game.Entites.Actions
{
    /// <summary>
    /// base action class, this can be queued later on. because these are objects and can be registered in a dictionary etc
    /// for iterative resolve!
    /// </summary>
    public interface IAction
    {
        public string ActionId { get; set; }
        public string ActionVerb { get; set; }
        protected DateTime ActionTriggerTime { get; set; } //todo replace with my own time class
        protected EntityDynamic Actor { get; set; }

        /// <summary>
        /// you can use action helper to send logs, it is better.
        /// </summary>
        public void Do();
    }
    
    public static class ActionHelper
    {
        public static Action<string> ActionLogEvent;

        public static void SendActionLog(string msg)
        {
            ActionLogEvent?.Invoke(msg);
        }
    }
}
