using Game.Data;
using Game.Tiles;
using Game.Entites;
using Game.Entites.Actions;
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
        [SerializeField] private SelectionCursorView _cursor; // todo can be instantiated instead.
        
        private Game.Managers.GameManager _gameManager;

        [Header("Debug")]
        [SerializeField] private float viewDistanceTest = 10f;

        void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _gameManager = GameManager.Instance;
        }
     
        public void UpdateLevelState()
        {
            _entityManager.PlayEntityOffense();
            SetFieldOfVision(_entityManager.GetPlayerEntity().GetEntityPos());
        }

        public void CreateLevel()
        {
            _gridManager.GenerateLevelGrid();
            _cameraManager.SetMapSize(new Vector2Int(16, 16));
            
            // todo make a proper entity spawner in entity manager! with parameters etc
            _entityManager.InstantiatePlayerEntity(new Vector2Int(2, 2), DataManager.GenerateStarterPlayerData());
            _cameraManager.SetCameraPosition(_entityManager.GetPlayerEntity().GetEntityPos());
            
            //todo remove npc for now
            _entityManager.InstantiateNpcEntity(new Vector2Int(10, 10), _entityManager.GetEntityDataWithIndex(1));
           // _entityManager.InstantiateEntity(EntityBase.EntityType.npc, new Vector2Int(0, 0));
        }

        public void MovePlayerTo(Vector2Int direction) //todo should this really be here??
        {
            var player = _entityManager.GetPlayerEntity();
            var targetGridPos = player.GetEntityPos() + direction;
            
            //Checks Walkability States
            if (_gridManager.CheckTileIfHasEntity(targetGridPos.x, targetGridPos.y, out var entity)) //todo this should be moved to walk action
            { 
                //entity doesn't register itself to the tile! npc problem
                if (entity.GetType() != typeof(EntityNpc)) return;
                var npc = (EntityNpc) entity;
                if (npc.GetDemeanor() != EntityDemeanor.hostile) return;

                var attack = new MeleeAttackAction<EntityDynamic>(player, (EntityDynamic) entity);
                _gameManager.UpdateGameState(GameManager.GameState.evaluate);
                return;
            }
            
            if (_gridManager.CheckTileIfWalkable(targetGridPos.x, targetGridPos.y) == false) //todo this should be moved to walk action
            {
                //grid is inaccessible anyway
                return;
            }
            
            _cameraManager.SetCameraPosition(targetGridPos);
            var moveAction = new WalkAction<EntityPlayer>(player, _gridManager.GetTileAtPosition(targetGridPos));
        }

        private void SetFieldOfVision(Vector2Int origin)
        {
            DetectionSystems.ShadowCast.ComputeVisibility(_gridManager, origin, viewDistanceTest); //todo view radius is fake for now
        }

        public void LookAtTile(Vector2Int target)
        {
            if (_gameManager.GetPlayerMode() != GameManager.PlayerModes.look)
                return;
            
            var currentCursorPos = _cursor.GetCurrentCursorPos();
            _cursor.MoveCursorTo(target + currentCursorPos);
            _cameraManager.SetCameraPosition(target + currentCursorPos);
        }

        /// todo without telepathy it can be used only on player's tile or adjecents.
        public void UseAtTile(Vector2Int target)
        {
            if (_gameManager.GetPlayerMode() != GameManager.PlayerModes.use)
                return; 
            
            var playerPos = _entityManager.GetPlayerEntity().GetEntityPos();
            _cursor.MoveCursorTo(target + playerPos);
        }

        public void StartLookAt()
        {
            _cursor.SetCursorMode(SelectionCursorView.CursorMode.interest);
            var posInit = _entityManager.GetPlayerEntity().GetEntityPos();
            _cursor.SetCursorState(true);
            _cursor.MoveCursorTo(posInit);
        }

        public void StartUseAt()
        {
            _cursor.SetCursorMode(SelectionCursorView.CursorMode.use);
            var PosInit = _entityManager.GetPlayerEntity().GetEntityPos();
            _cursor.SetCursorState(true);
            _cursor.MoveCursorTo(PosInit);
        }
        
        public void ResetCursor()
        {
            var posInit = _entityManager.GetPlayerEntity().GetEntityPos();
            _cursor.SetCursorState(false);
            _cameraManager.SetCameraPosition(posInit);
        }

        public void InteractWithObject()
        {
            var interactionCursorPos = _cursor.GetCurrentCursorPos();
            var interactionItems = _gridManager.GetTile(interactionCursorPos.x, interactionCursorPos.y).GetInteractables();

            if (interactionItems == null) return;
            if (interactionItems.Count == 0) return;

            var interaction = new InteractAction<EntityDynamic>(_entityManager.GetPlayerEntity(), interactionItems[0]); //get the first item for now to test.
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
