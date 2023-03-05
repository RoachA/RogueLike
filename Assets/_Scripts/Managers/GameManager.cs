using System;
using System.Security.Authentication;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState
        {
            loadLevel,
            evaluate,
            playerTurn,
            cpuTurn,
        }

        public static event Action<GameState> OnGameStateChanged;
      
        private LevelManager _levelManager;
        private EntityManager _entityManager;
        
        private GameState _currentGameState;

        void Start()
        {
         _levelManager = LevelManager.Instance;
         _entityManager = EntityManager.Instance;
         
         UpdateGameState(GameState.loadLevel);
        }

        public GameState GetGameState()
        {
            return _currentGameState;
        }

        public void UpdateGameState(GameState newState)
        {
            _currentGameState = newState;

            switch (newState)
            {
                case GameState.loadLevel:
                    HandleLoadLevel();
                    break;
                case GameState.evaluate:
                    HandleEvaluate();
                    break;
                case GameState.playerTurn:
                    HandlePlayerTurn();
                    break;
                case GameState.cpuTurn:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
            
            OnGameStateChanged?.Invoke(newState);
        }
        
        [Button("Generate Level")]
        private void HandleLoadLevel()
        {
            _levelManager.CreateLevel();
            
            _entityManager.InstantiateEntity(EntityBase.EntityType.player, new Vector2(2, 2));
            _entityManager.InstantiateEntity(EntityBase.EntityType.npc, new Vector2(3, 2));
            
            Debug.Log("level is Ready!");
            UpdateGameState(GameState.evaluate);
        }
        
        private void HandleEvaluate()
        {
            //todo do calculations to determine who starts first, player or enemy? now start player
            UpdateGameState(GameState.playerTurn);
        }

        private void HandlePlayerTurn()
        {
            Debug.Log("player turn!");
        }

        private void HandleCPUTurn()
        {
        }
    }
}
