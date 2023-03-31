using UnityEngine;
using DG.Tweening;
using Sequence = DG.Tweening.Sequence;

public class SelectionCursorView : MonoBehaviour
{
    public enum CursorMode
    {
        normal = 0,
        hostile = 1,
        interest = 2,
        use = 3,
    }
    
    [SerializeField] private SpriteRenderer _selectionCursor;
    [SerializeField] private Color[] _cursorColors;

    private Transform _transform;
    private Sequence _seq;

    void Awake()
    {
        _transform = transform;
    }

    void Start()
    {
        SetCursorState(false);
        SetCursorMode(CursorMode.normal);
    }

    public Vector2Int GetCurrentCursorPos()
    {
        var localPos = _transform.localPosition;
        return new Vector2Int((int) localPos.x, (int) localPos.y);
    }

    public void MoveCursorTo(Vector2Int pos)
    {
        _transform.localPosition = new Vector3(pos.x, pos.y, _transform.localPosition.z);
    }

    public void SetCursorState(bool state)
    {
        if (state)
            ActivateCursorSeq();
        else
            RemoveCursorSeq();
    }

    public void SetCursorMode(CursorMode mode)
    {
        if ((int) mode < _cursorColors.Length)
            _selectionCursor.color = _cursorColors[(int) mode];
    }

    private void RemoveCursorSeq()
    {
        _seq?.Kill(true);
        _seq = DOTween.Sequence();
        
        _transform.localScale = Vector3.one;
        
        _seq.Insert(0, _selectionCursor.DOFade(0, 0.25f));
        _seq.Insert(0, _transform.DOScale(Vector3.zero, 0.25f));

        _seq.OnComplete(() => _selectionCursor.gameObject.SetActive(false));
    }

    private void ActivateCursorSeq()
    {
        _seq?.Kill(true);
        _seq = DOTween.Sequence();
        _selectionCursor.gameObject.SetActive(true);

        _transform.localScale = Vector3.zero;
        
        _seq.Insert(0, _selectionCursor.DOFade(1, 0.25f));
        _seq.Insert(0, _transform.DOScale(Vector3.one, 0.25f));
    }
}
