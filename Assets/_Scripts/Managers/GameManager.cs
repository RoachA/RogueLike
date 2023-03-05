using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Managers
{
    //
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
        private GameState _currentGameState;
        private bool _movementActive = true;
        
        private readonly KeyCode[] _keyCodes =
        {
            KeyCode.Keypad0,
            KeyCode.Keypad1,
            KeyCode.Keypad2,
            KeyCode.Keypad3,
            KeyCode.Keypad4,
            KeyCode.Keypad5,
            KeyCode.Keypad6,
            KeyCode.Keypad7,
            KeyCode.Keypad8,
            KeyCode.Keypad9,
        };

        private Dictionary<int, Vector2Int> _movementVectors = new Dictionary<int, Vector2Int>
        {
            {1, new Vector2Int(-1, -1)},
            {2, new Vector2Int(0, -1)},
            {3, new Vector2Int(1, -1)},
            {4, new Vector2Int(-1, 0)},
            {6, new Vector2Int(1, 0)},
            {7, new Vector2Int(-1, 1)},
            {8, new Vector2Int(0, 1)},
            {9, new Vector2Int(1, 1)},
        };
        
        void Start()
        {
            _levelManager = LevelManager.Instance;

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

        #region GameLoopElements

        private void Update()
        {
            if (_currentGameState == GameState.playerTurn && _movementActive)
            {
                for (int i = 0; i < _keyCodes.Length; i++)
                {
                    if (Input.GetKeyDown(_keyCodes[i]))
                    {
                        if (_movementVectors.TryGetValue(i, out var motionVector))
                        {
                            _levelManager.MovePlayerTo(motionVector);
                            Debug.Log("pressed: " + i);
                            Debug.Log("moving to" + motionVector);
                        }
                    }
                }
            }
        }

        #endregion
        }
    }