using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using DG.Tweening;

namespace Game.UI
{
    public struct InventoryDefinition
    {
        public string Name;
        public int Count;
        public Sprite Sprite;

        public InventoryDefinition(string name, int count, Sprite sprite)
        {
            Name = name;
            Count = count;
            Sprite = sprite;
        }
    }
    
    public class ContainerUIItemView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private Image _bg;
        [SerializeField] private Image _spriteImage;
        [SerializeField] private TextMeshProUGUI _nameTxt;
        [SerializeField] private TextMeshProUGUI _countTxt;
        [SerializeField] private GameObject _selectedIndicator;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _idleColor;

        private Sequence _seq;

        public void Init(InventoryDefinition data, bool isSelected = false)
        {
            _spriteImage.sprite = data.Sprite;
            _nameTxt.text = data.Name;
            _countTxt.text = data.Count.ToString();
            SetSelectedState(isSelected);
        }

        public void SetSelectedState(bool isSeleceted)
        {
            _selectedIndicator.SetActive(isSeleceted);
            _bg.color = isSeleceted ? _selectedColor : _idleColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _seq?.Kill(true);
            _seq = DOTween.Sequence();

            transform.localScale = Vector3.one;
            _seq.Append(transform.DOScale(Vector3.one * 0.95f, 0.15f).SetEase(Ease.OutBack)
                .OnComplete(() => transform.localScale = Vector3.one));
            
            Debug.Log("pressed item: " + _nameTxt);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            SetSelectedState(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetSelectedState(false);
        }
    }
}