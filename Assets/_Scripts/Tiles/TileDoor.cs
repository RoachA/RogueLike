using System.Collections.Generic;
using Game.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Game.Tiles
{
    public class TileDoor : TileFloor, IInteractable
    {
        [Header("DoorTiles")]
        [SerializeField] private Sprite[] _closedDoors;
        [SerializeField] private Sprite[] _openDoors;
        [SerializeField] private ShadowCaster2D _shadowCaster;

        [SerializeField] private bool _isOpen;
        [SerializeField] private bool _isLocked;
        [SerializeField] private bool _levelExit;
        private Sprite _closedDoorSprite;
        private Sprite _openDoorSprite;
        
        public string InteractionResultLog { get; set; }
        
        [Button("Use Door")]
        public bool TryDoorOpen()
        {
            ///todo activate deactivate the shadow caster! will be cool
            if (_isLocked)
            {
                return true;
            }

            _shadowCaster.enabled = !_shadowCaster.enabled;
            _isOpen = !_isOpen;
            SetWalkable(_isOpen);

            SetDoorSprite();
            return false;
        }

        public void SetLevelExitState(bool isExit)
        {
            _levelExit = isExit;
        }
        
        public bool GetLevelExitState()
        {
            return _levelExit;
        }
        
        public bool CheckIfLocked()
        {
            return _isLocked && !_isOpen;
        }
        
        public string InteractWithThis()
        {
            var isLocked = TryDoorOpen();
            
            if (isLocked)
            {
                InteractionResultLog = "The " + "_tileTypeName" + " is locked and cannot be opened!";
                //todo if lockpicking is a possibility. prompt that.
                //todo if player uses a key or something prompt that and open the door.
            }
            else
            {
                var state = _isOpen ? " opens " : " closes ";
                InteractionResultLog = state + "the " + "_tileTypeName";
            }
            
            return InteractionResultLog;
        }
        
        private void SetDoorSprite()
        {
            _closedDoorSprite = _closedDoors[0];
            _openDoorSprite = _openDoors[0];
            
            _spriteRenderer.sprite = _isOpen ? _openDoorSprite : _closedDoorSprite;
        }

        public override void Init(ICoords coords, List<TileTypeData> data)
        {
            base.Init(coords, data);
            SetDoorSprite();
        }
    }
}
