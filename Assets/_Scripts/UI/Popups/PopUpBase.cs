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
        public Sprite SpriteImage;
        
        public PopUpBaseProperties(string header, string info, Sprite spriteImage = null)
        {
            Header = header;
            Info = info;
            SpriteImage = spriteImage;
        }
    }
    
    public class PopUpBase : UIElement
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private RectTransform _popUpRect;
        [SerializeField] private TextMeshProUGUI _headerTxt;
        [SerializeField] private TextMeshProUGUI _infoTxt;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Image _image;
        
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

        public virtual void SetSprite(Sprite sprite)
        {
            _image.enabled = true;
            
            if (sprite == null)
            {
                _image.enabled = false;
                return;
            }

            _image.sprite = sprite;
        }
        
        public virtual void SetInactive()
        {
            Seq?.Kill(true);
            Seq = DOTween.Sequence();
            
            _container.SetActive(false);
            _popUpRect.sizeDelta = Vector2.zero;
            _closeButton.transform.localScale = Vector3.zero;
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
            _container.SetActive(true);

            Seq.Append(_popUpRect.DOSizeDelta(_initSize1, 0.1f).SetEase(Ease.OutBack));
            Seq.Append(_popUpRect.DOSizeDelta(_initRect, 0.1f).SetEase(Ease.OutBack));
            Seq.Append(_closeButton.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.OutBack).SetDelay(0.05f));

            Seq.Append(_headerTxt.DOFade(1, 0.1f));
            Seq.Append(_infoTxt.DOFade(1, 0.1f));
            Seq.OnComplete(() => SetSprite(data.SpriteImage));
        }


        public override void Close<T>(Type uiElement)
        {
            if (GetType() != uiElement)
                return;
            
            _image.enabled = false;
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
            CloseUiSignal?.Invoke(typeof(PopUpBase));
        }
    }
}