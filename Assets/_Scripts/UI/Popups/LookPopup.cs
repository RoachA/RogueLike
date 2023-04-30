using UnityEngine;

namespace Game.UI
{
    public class LookPopupProperties : UIProperties
    {
        public string Header;
        public string Info;
        public Sprite SpriteImage;

        public LookPopupProperties(string header, string info, Sprite spriteImage = null)
        {
            Header = header;
            Info = info;
            SpriteImage = spriteImage;
        }
    }

    public class LookPopup : MonoBehaviour
    {


    }
}
