using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PopUpBaseProperties : UIProperties
    {
        public String Header;
        public String Info;

        public PopUpBaseProperties(string header, string info)
        {
            Header = header;
            Info = info;
        }
    }
    
    public class PopUpBaseView : UIElement
    {
        [SerializeField] private RectTransform _popUpRect;
        [SerializeField] private TextMeshProUGUI _headerTxt;
        [SerializeField] private TextMeshProUGUI _infoTxt;
        [SerializeField] private Button _closeButton;
        
        private Vector2 _initRect;
        private Vector2 _initSize1;
        
        public override void Init<T>(T uiProperties = default)
        {
            base.Init(uiProperties);
            
            _initRect = _popUpRect.sizeDelta;
            _initSize1 = new Vector2(_initRect.x, 50);
            
            SetInactive();
        }

        public virtual void SetHeaderText(string txt)
        {
            _headerTxt.text = txt;
        }

        public virtual void SetInfoText(string txt)
        {
            _infoTxt.text = txt;
        }
        
        public virtual void SetInactive()
        {
            _popUpRect.sizeDelta = Vector2.zero;
            _closeButton.transform.localScale = Vector3.zero;

            Seq?.Kill(true);
            Seq = DOTween.Sequence();
            Seq.Append(_headerTxt.DOFade(0, 0));
            Seq.Append(_infoTxt.DOFade(0, 0));
        }

        public override void Open<T, T1>(Type uiType, T1 property)
        {
            if (GetType() != uiType)
                return;
            
            if (property is PopUpBaseProperties data)
            {
                SetHeaderText(data.Header);
                SetInfoText(data.Info);
            }
            else
            {
                Debug.LogError("wrong data was sent to " + name);
                return;
            }
            
            base.Open<T, T1>(uiType, property);
            
            _closeButton.onClick.AddListener(OnClose);
            
            SetInactive();
            
            Seq?.Kill(true);
            Seq = DOTween.Sequence();

            Seq.Append(_popUpRect.DOSizeDelta(_initSize1, 0.1f).SetEase(Ease.OutBack));
            Seq.Append(_popUpRect.DOSizeDelta(_initRect, 0.1f).SetEase(Ease.OutBack));
            Seq.Append(_closeButton.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.OutBack).SetDelay(0.05f));

            Seq.Append(_headerTxt.DOFade(1, 0.1f));
            Seq.Append(_infoTxt.DOFade(1, 0.1f));
        }


        public override void Close<T>(Type uiElement)
        {
            if (GetType() != uiElement)
                return;
            Seq?.Kill(true);
            Seq = DOTween.Sequence();

            Seq.Append(_headerTxt.DOFade(0, 0.1f));
            Seq.Append(_infoTxt.DOFade(0, 0.1f));
            Seq.Append(_closeButton.transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InBack));
            Seq.Append(_popUpRect.DOSizeDelta(_initSize1, 0.1f).SetEase(Ease.InBack));
            Seq.Append(_popUpRect.DOSizeDelta(Vector2.zero, 0.1f).SetEase(Ease.InBack));
            
            _closeButton.onClick.RemoveListener(OnClose);
        }
        
        protected void OnClose()
        {
            CloseUiSignal?.Invoke(typeof(PopUpBaseView));
        }
    }
}
