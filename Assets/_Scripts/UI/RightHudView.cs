using UnityEngine;
using DG.Tweening;
using System;

namespace Game.UI
{
    public class RightHudView : UIElement
    {
        [SerializeField] private RectTransform _rectTransform;
        private readonly float _openPosX = 15f;
        private readonly float _closePosX = 500f;
        
        public override void Close<T>(Type uiElement)
        {
            if (GetType() != uiElement)
                return;
            
            Seq?.Kill(true);
            Seq = DOTween.Sequence();
            
            Seq.Append(_rectTransform.DOAnchorPosX(_closePosX, 0.2f).SetEase(Ease.OutQuad));
        }

        public override void Open<T, T1>(Type uiElement, T1 property)
        {
            if (GetType() != uiElement)
                return;
            
            Seq?.Kill(true);
            Seq = DOTween.Sequence();

            Seq.Append(_rectTransform.DOAnchorPosX(_openPosX, 0.2f).SetEase(Ease.InQuad));
        }
    }
}
