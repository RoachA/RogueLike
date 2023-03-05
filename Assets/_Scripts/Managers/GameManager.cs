using System;
using System.Security.Authentication;
using UnityEngine;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState
        {
            loadLevel,
            initializeLevel,
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
         
         HandleLoadLevel();
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

        private void HandleLoadLevel()
        {
            _entityManager.InstantiateEntity(EntityBase.EntityType.player, new Vector2(2, 2));
        }

        private void HandlePlayerTurn()
        {
            
        }

        private void HandleCPUTurn()
        {
            
        }
    }
}
