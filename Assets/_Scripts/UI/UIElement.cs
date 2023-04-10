using System;
using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public class UIElement : MonoBehaviour, IUIElement
    {
        public static Action<UIElement> OpenUiSignal; 
        public static Action<UIElement> CloseUiSignal;
        protected Sequence Seq;


        public virtual void Start()
        {
            Init();
        }

        public virtual void Init()
        {
            Seq = DOTween.Sequence();
            
            OpenUiSignal += Open;
            CloseUiSignal += Close;
        }
        
        public void OnDestroy()
        {
            OpenUiSignal -= Open;
            CloseUiSignal -= Close;
        }

        Sequence IUIElement.Seq
        {
            get => Seq;
            set => Seq = value;
        }
        public virtual void Open<T>(T uiType) where T : UIElement
        {
        }

        public virtual void Close<T>(T uiType) where T : UIElement
        {
        }
    }
    
    public interface IUIElement
    {
        protected Sequence Seq { get; set; }
        
        public abstract void Open<T>(T uiType) where T : UIElement;
        
        public abstract void Close<T>(T uiType) where T : UIElement;
    }
}