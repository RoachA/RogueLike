using Game.UI;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class LookUpListItemView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Color _activeColor, _inactiveColor;

    public void InitView(ILookable lookableItem)
    {
        //todo get helper's help and fill the view with proper data. we should also cache it here to use on detail view!
    }
}
