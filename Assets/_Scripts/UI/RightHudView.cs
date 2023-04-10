using UnityEngine;
using DG.Tweening;

namespace Game.UI
{
    public class RightHudView : UIElement
    {
        [SerializeField] private RectTransform _rectTransform;
        private readonly float _openPosX = 15f;
        private readonly float _closePosX = 500f;
        
        public override void Close<T>(T uiElement)
        {
            if (GetType() != uiElement.GetType())
                return;
            
            Seq?.Kill(true);
            Seq = DOTween.Sequence();
            
            Seq.Append(_rectTransform.DOAnchorPosX(_closePosX, 0.2f).SetEase(Ease.OutQuad));
        }

        public override void Open<T>(T uiElement)
        {
            if (GetType() != uiElement.GetType())
                return;
            
            Seq?.Kill(true);
            Seq = DOTween.Sequence();

            Seq.Append(_rectTransform.DOAnchorPosX(_openPosX, 0.2f).SetEase(Ease.InQuad));
        }
    }
}
