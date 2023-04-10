using System;
using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public class UIElement : MonoBehaviour, IUIElement
    {
        public static Action<Type, UIProperties> OpenUiSignal; 
        public static Action<Type> CloseUiSignal;
        protected Sequence Seq;
        
        public void OnDestroy()
        {
            OpenUiSignal -= Open<UIElement, UIProperties>;
            CloseUiSignal -= Close<UIElement>;
        }

        Sequence IUIElement.Seq
        {
            get => Seq;
            set => Seq = value;
        }
        
        /// <summary>
        /// initializes UI with dummy data
        /// </summary>
        protected void Awake()
        {
            Init<UIProperties>();
        }

        /// <summary>
        /// Override this to trigger certain behaviors on start.
        /// </summary>
        /// <param name="uiProperties"></param>
        /// <typeparam name="T"></typeparam>
        public virtual void Init<T>(T uiProperties = default) where T : UIProperties
        {
            Seq = DOTween.Sequence();
            
            OpenUiSignal += Open<UIElement, UIProperties>;
            CloseUiSignal += Close<UIElement>;
        }
        
        /// <summary>
        /// Opens the ui element with its properties
        /// </summary>
        /// <param name="uiType"> what type of UI is to be operated?</param>
        /// <param name="property"> what data we want to feed? </param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        public virtual void Open<T, T1>(Type uiType, T1 property) where T : UIElement where T1 : UIProperties
        {
        }
        
        public virtual void Close<T>(Type uiType) where T : UIElement
        {
        }
    }
    
    /// <summary>
    /// base interface for ui behaviors
    /// </summary>
    public interface IUIElement
    {
        protected Sequence Seq { get; set; }

        public void Init<T>(T uiProperties) where T : UIProperties;
        
        public void Open<T, T1>(Type uiType, T1 property = default) where T : UIElement where T1 : UIProperties;
        
        public void Close<T>(Type uiType) where T : UIElement;
    }

    /// <summary>
    /// This is to be inherited by other ui properties, is used to feed a ui with the latest data when needed.
    /// Can be further extended to carry various delicate info.
    /// </summary>
    public class UIProperties
    {
    }
}