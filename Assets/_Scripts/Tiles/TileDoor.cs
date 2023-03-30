using System;
using Game.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Tiles
{
    public class TileDoor : TileFloor, IInteractable
    {
        [Header("DoorTiles")]
        [SerializeField] private Sprite[] _closedDoors;
        [SerializeField] private Sprite[] _openDoors;

        [SerializeField] private bool _isOpen;
        [SerializeField] private bool _isLocked;
        private Sprite _closedDoorSprite;
        private Sprite _openDoorSprite;
        
        [Button("Use Door")]
        public void OnDoorUse()
        {
            if (_isLocked)
            {
                Debug.LogWarning("the door is locked!");
                return;
            }
            
            _isOpen = !_isOpen;
            SetWalkable(_isOpen);

            SetDoorSprite();
        }

        private void SetDoorSprite()
        {
            _spriteRenderer.sprite = _isOpen ? _openDoorSprite : _closedDoorSprite;
        }

        public bool CheckIfLocked()
        {
            return _isLocked && _isOpen;
        }

        public void InteractWithThis()
        {
        }

        protected override void Start()
        {
            _closedDoorSprite = _closedDoors[0];
            _openDoorSprite = _openDoors[0];
            SetDoorSprite();
        }

        private void OnValidate()
        {
            SetDoorSprite();
        }
    }
}
