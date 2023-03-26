using Game.Interfaces;
using UnityEngine;

namespace Game.Tiles
{
    public class TileDoor : TileWall, IInteractable
    {
        [Header("DoorTiles")]
        [SerializeField] private Sprite[] _closedDoors;
        [SerializeField] private Sprite[] _openDoors;

        private bool _isOpen;
        private bool _isLocked;
        private Sprite _closedDoorSprite;
        private Sprite _openDoorSprite;

        public void OnDoorUse()
        {
            if (_isLocked)
            {
                Debug.LogWarning("the door is locked!");
                return;
            }
            
            _isOpen = !_isOpen;
            SetWalkable(_isOpen);

            _spriteRenderer.sprite = _isOpen ? _openDoorSprite : _closedDoorSprite;
        }

        public bool CheckIfLocked()
        {
            return _isLocked && _isOpen;
        }

        public void InteractWithThis()
        {
        }
    }
}
