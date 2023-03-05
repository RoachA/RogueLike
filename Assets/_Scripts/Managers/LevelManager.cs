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

        private void Awake()
        {
            Instance = this;
        }
        
        public void CreateLevel()
        {
            _gridManager.GenerateLevelGrid();
            
            _entityManager.InstantiateEntity(EntityBase.EntityType.player, new Vector2(2, 2));
            _entityManager.InstantiateEntity(EntityBase.EntityType.npc, new Vector2(3, 2));
        }

        public void MovePlayerTo(Vector2Int direction)
        {
            _entityManager.GetPlayerEntity().MoveEntity(direction);
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
