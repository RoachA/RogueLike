using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game.UI
{

    [RequireComponent(typeof(Button))]
    public class ButtonElement : MonoBehaviour
    {
        [SerializeField] protected Button _button;
        [SerializeField] protected TextMeshProUGUI _buttonText;
        [Header("Keyboard Input")]
        [SerializeField] protected bool _keyboardInput;
        [SerializeField] protected int _inputNum;

        protected Sequence _seq;
        
        protected virtual void Start()
        {
            SetKeyboardInput();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _seq?.Kill(true);
            _seq = DOTween.Sequence();

            transform.localScale = Vector3.one;
            _seq.Append(transform.DOScale(Vector3.one * 0.95f, 0.15f).SetEase(Ease.OutBack)
                .OnComplete(() => transform.localScale = Vector3.one));
        }

        protected virtual void SetKeyboardInput()
        {
            if (_keyboardInput == false)
                return;

            var text = _buttonText.text + " [" + _inputNum + "]";
            _buttonText.text = text;
        }
    }
}
