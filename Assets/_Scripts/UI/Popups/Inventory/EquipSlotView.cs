using System;
using Game.Data;
using Game.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
  public class EquipSlotView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
  {
    [SerializeField] public EntityEquipSlots Slot;
    [SerializeField] private Button _button;
    [SerializeField] private Image _itemView;
    [SerializeField] private Image _frame;
    [SerializeField] private TextMeshProUGUI _labelTxt;
    [Header("Line")]
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform[] _linePoints;
    [Header("Interaction Parameters - Frame")]
    [SerializeField] private Color _idleColor;
    [SerializeField] private Color _selectedColor;
    [Header("Interaction Parameters Label")]
    [SerializeField] private Color _labelIdleColor;
    [SerializeField] private Color _labelSelectedColor;
    
    public void InitEquipSlotView(ScriptableItemData data = null)
    {
      SetLineConnection();
      SetItemView();
      SetSelectedState(false);
      
      if (data != null)
      {
        _itemView.enabled = true;
        _itemView.sprite = data._itemSprite;
      }
    }

    private void SetItemView()
    {
      _button.onClick.AddListener(OnButtonClicked);
      _labelTxt.text = Slot.ToString();
      _itemView.enabled = false;
      _itemView.sprite = null;
    }

    private void Update()
    {
      SetLineConnection();
    }

    private void OnButtonClicked()
    {
      //todo popup for the item info and interaction options.
    }

    private void SetSelectedState(bool isSelected)
    {
      var colorFrame = isSelected ? _selectedColor : _idleColor;
      var colorLabel = isSelected ? _labelSelectedColor : _labelIdleColor;
      _frame.color = colorFrame;
      _lineRenderer.startColor = colorFrame;
      _labelTxt.color = colorLabel;
    }

    private void SetLineConnection()
    {
      _lineRenderer.positionCount = 3;
      _lineRenderer.SetPosition(0, _linePoints[0].transform.position);
      _lineRenderer.SetPosition(1, _linePoints[1].transform.position);
      _lineRenderer.SetPosition(2, _linePoints[2].transform.position);
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
