using System;
using Game.Entites;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
  public class EquipSlotView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
  {
    [SerializeField] private EntityEquipSlots _slot;
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

    private void Start()
    {
      InitEquipSlotView(); //debug
    }

    public void InitEquipSlotView()
    {
      SetLineConnection();
      SetItemView();
      SetSelectedState(false);
    }

    private void SetItemView()
    {
      _labelTxt.text = _slot.ToString();
      _itemView.sprite = null;
    }

    private void Update()
    {
      SetLineConnection();
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
