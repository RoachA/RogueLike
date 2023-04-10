using UnityEngine;
using Sirenix.OdinInspector;

namespace Game.UI
{
   public class UIManager : MonoBehaviour
   {
      [Header("HUDS")]
      [SerializeField] private RightHudView _rightHud;
      [Header("POP-UPS")]
      [SerializeField] private PopUpBase _genericPopUp;
      

      [Button]
      private void OpenSignalTest()
      {
         UIElement.OpenUiSignal?.Invoke(new PopUpBase());
         UIElement.CloseUiSignal?.Invoke(new RightHudView());
      }
   }

}
