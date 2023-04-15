using UnityEngine;
using Sirenix.OdinInspector;

namespace Game.UI
{
   public class UIManager : MonoBehaviour
   {
      //todo make a background fader here. this manager can control overall ui stuffs. And debug things
      [Button]
      private void OpenSignalTest()
      {
         UIElement.CloseUiSignal(typeof(RightHudView));
         UIElement.OpenUiSignal(typeof(PopUpBase), new PopUpBaseProperties("amcık", "ne dirsen yaragim"));
      }
   }

}
