namespace Game.Interfaces
{
     public interface IInteractable
     {
          public string InteractionResultLog { set; }
          
          /// <summary>
          /// use interactable helper to call a log request with a string.
          /// this method should return the resulting interaction as a string.
          /// also if something happens it must be logged and given back to the
          /// action.
          /// </summary>
          string InteractWithThis();
     }
}