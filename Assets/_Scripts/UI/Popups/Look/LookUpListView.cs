using System.Collections.Generic;
using Game.UI;
using UnityEngine;

public class LookUpListView : MonoBehaviour
{
    [SerializeField] private LookUpListItemView _itemReference;
    [SerializeField] private Transform _itemsContainer;

    [SerializeField] private Dictionary<LookableType, LookUpListItemView> _currentItems;

    private LookPopup _lookPopup;

    private void Start()
    {
        _lookPopup = GetComponentInParent<LookPopup>() ?? _lookPopup;
    }

    public void InitLookUpItems(List<ILookable> items)
    {
        if (_currentItems == null)
            _currentItems = new Dictionary<LookableType, LookUpListItemView>();
        
        foreach (var item in items)
        {
            LookUpListItemView newView;
            if (CheckForExistingItem(item.MyLookableType, out var view))
            {
                newView = view;
            }
            else
            {
                newView = Instantiate(_itemReference, _itemsContainer);
                _currentItems.Add(item.MyLookableType, newView);
            }

            newView.InitView(item);
            newView.gameObject.SetActive(true);
        }
    }

    private bool CheckForExistingItem(LookableType type, out LookUpListItemView? existingItem)
    {
        existingItem = _currentItems.TryGetValue(type, out var item) ? item : null;
        return existingItem != null;
    }

    private void HideAllViews()
    {
        foreach (var itemView in _currentItems)
        {
            itemView.Value.gameObject.SetActive(false);
        }
    }
    
}
