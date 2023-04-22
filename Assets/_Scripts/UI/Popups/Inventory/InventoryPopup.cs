using System;
using System.Collections;
using System.Collections.Generic;
using Game.UI;
using UnityEngine;

namespace Game.UI
{
    public class InventoryUIProperties : UIProperties
    {
        public InventoryUIProperties()
        {
        }
    }
    
    public class InventoryPopup : UIElement
    {
        public override void Open<T, T1>(Type uiType, T1 property)
        {
            base.Open<T, T1>(uiType, property);
            
            if (property is InventoryUIProperties data)
            {
                
            }
            //todo make unique sequence
        }

        public override void Close<T>(Type uiElement)
        {
           //todo make unique sequence
            base.Close<T>(uiElement);
        }

    }
}
