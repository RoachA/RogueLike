using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PopUpBase : UIElement
    {
        [SerializeField] private RectTransform _popUpRect;
        [SerializeField] private TextMeshProUGUI _headerTxt;
        [SerializeField] private TextMeshProUGUI _infoTxt;
        [SerializeField] private Button _closeButton;
        
        private Vector2 _initRect;
        private Vector2 _initSize1;

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
        
        public override void Init()
        {
            base.Init();
            _initRect = _popUpRect.sizeDelta;
            _initSize1 = new Vector2(_initRect.x, 50);
            
            SetInactive();
        }

        public override void Open<T>(T uiElement)
        {
            if (GetType() != uiElement.GetType())
                return;
            
            _closeButton.onClick.AddListener(OnClose);
            
            base.Open(uiElement);
            SetInactive();
            
            Seq?.Kill(true);
            Seq = DOTween.Sequence();

            Seq.Append(_popUpRect.DOSizeDelta(_initSize1, 0.1f).SetEase(Ease.OutBack));
            Seq.Append(_popUpRect.DOSizeDelta(_initRect, 0.1f).SetEase(Ease.OutBack));
            Seq.Append(_closeButton.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.OutBack).SetDelay(0.05f));

            Seq.Append(_headerTxt.DOFade(1, 0.1f));
            Seq.Append(_infoTxt.DOFade(1, 0.1f));
        }


        public override void Close<T>(T uiElement)
        {
            if (GetType() != uiElement.GetType())
                return;
            base.Close(uiElement);
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
            CloseUiSignal?.Invoke(new PopUpBase());
        }
    }
}
