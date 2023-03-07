using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionCursorView : MonoBehaviour
{
    public enum CursorMode
    {
        normal = 0,
        hostile = 1,
        interest = 2,
    }
    
    [SerializeField] private SpriteRenderer _selectionCursor;
    [SerializeField] private Color[] _cursorColors;

    private Transform _transform;

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
        _selectionCursor.gameObject.SetActive(state);
    }

    public void SetCursorMode(CursorMode mode)
    {
        if ((int) mode < _cursorColors.Length)
            _selectionCursor.color = _cursorColors[(int) mode];
    }
}
