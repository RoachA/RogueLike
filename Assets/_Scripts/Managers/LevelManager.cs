using System;
using Game.Tiles;
using UnityEngine;

namespace Game.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;

        [SerializeField] private GridManager _gridManager;
        [SerializeField] private EntityManager _entityManager;

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

        public TileBase GetTileAt(int cellX, int cellY)
        {
           return _gridManager.GetTile(cellX, cellY);
        }

        public void LoadLevel()
        {
            
        }
    }
}
