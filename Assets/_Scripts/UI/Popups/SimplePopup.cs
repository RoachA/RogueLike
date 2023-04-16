using System;
using UnityEngine;

namespace Game.UI
{
    public class SimplePopupProperties : UIProperties
    {
        public String Header;
        public String Info;
        public Sprite SpriteImage;
        
        public SimplePopupProperties(string header, string info, Sprite spriteImage = null)
        {
            Header = header;
            Info = info;
            SpriteImage = spriteImage;
        }
    }
    
    public class SimplePopup : PopUpBase
    {
        public override void Open<T, T1>(Type uiType, T1 property)
        {
            base.Open<T, T1>(uiType, property);
            
            if (property is SimplePopupProperties data)
            {
                SetHeaderText(data.Header);
                SetInfoText(data.Info);
                SetSprite(data.SpriteImage);
            }
        }
    }
}