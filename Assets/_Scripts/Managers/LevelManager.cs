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

        [SerializeField] private GridManager _gridManager;
        [SerializeField] private EntityManager _entityManager;
        [SerializeField] private CameraManager _cameraManager;

        private Game.Managers.GameManager _gameManager;

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

        public TileBase GetTileAt(int cellX, int cellY)
        {
           return _gridManager.GetTile(cellX, cellY);
        }

        public void LoadLevel()
        {
            
        }
    }
}
