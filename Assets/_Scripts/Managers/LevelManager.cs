using System;
using Game.Tiles;
using UnityEngine;

namespace Game.Managers
{
    public class LevelManager : MonoBehaviour
    {
        //this would handle level loading but nothing else perhaps?
        //also changes to the level. like swap tiles etc.
        public static LevelManager Instance;

        [Header("Managers")]
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private EntityManager _entityManager;
        [SerializeField] private CameraManager _cameraManager;
        
        [Header("Views")]
        [SerializeField] private SelectionCursorView _cursor;
        

        private Game.Managers.GameManager _gameManager;
        private bool _lookAtActive = false;

        void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _gameManager = GameManager.Instance;
        }

        public void CreateLevel()
        {
            _gridManager.GenerateLevelGrid();
            
            // todo make a proper entity spawner in entity manager!
            _entityManager.InstantiateEntity(EntityBase.EntityType.player, new Vector2Int(2, 2));
            _entityManager.InstantiateEntity(EntityBase.EntityType.npc, new Vector2Int(3, 2));
            _entityManager.InstantiateEntity(EntityBase.EntityType.npc, new Vector2Int(0, 0));
        }

        public void MovePlayerTo(Vector2Int direction)
        {
            var player = _entityManager.GetPlayerEntity();
            var targetGridPos = player.GetEntityPos() + direction;

            if (_gridManager.CheckTileIfWalkable(targetGridPos.x, targetGridPos.y) == false)
            {
                //grid is inaccessible anyway
                return;
            }

            if (_gridManager.CheckTileIfHasEntity(targetGridPos.x, targetGridPos.y, out var entity))
            {
                //todo call battle state
                Debug.Log("Tile at " + targetGridPos + " has an entity type of : " + entity.name);
                Debug.Log("Combat starts!");
                _gameManager.UpdateGameState(GameManager.GameState.evaluate);
                return;
            }
            
            //if tile is walkable and if it is not occupied by an enemy >>> walk there! :)
            _entityManager.GetPlayerEntity().MoveEntity(direction);
            _cameraManager.SetCameraPosition(targetGridPos);
        }

        public void LookAtTile(Vector2Int target)
        {
            if (_lookAtActive == false)
                return;

            var currentCursorPos = _cursor.GetCurrentCursorPos();
            _cursor.MoveCursorTo(target + currentCursorPos);
            _cursor.SetCursorState(true);
            _cameraManager.SetCameraPosition(target + currentCursorPos);
            Debug.Log(_gridManager.GetTileData(target + currentCursorPos));
        }

        public void StartLookAt()
        {
            _lookAtActive = true;
            var posInit = _entityManager.GetPlayerEntity().GetEntityPos();
            _cursor.SetCursorState(true);
            _cursor.MoveCursorTo(posInit);
        }

        public void StopLookAtTile()
        {
            var posInit = _entityManager.GetPlayerEntity().GetEntityPos();
            _cursor.SetCursorState(false);
            _cursor.MoveCursorTo(posInit);
            _cameraManager.SetCameraPosition(posInit);
            _lookAtActive = false;
        }

        public TileBase GetTileAt(int cellX, int cellY)
        {
           return _gridManager.GetTile(cellX, cellY);
        }

        public void LoadLevel()
        {
        }
    }
}
