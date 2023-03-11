using System;
using System.Collections.Generic;
using Game.Dice;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static Game.Managers.GameManager Instance;
        
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
        private bool _lookAtActive = false;

        public static Vector2 GridSize;
        
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

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            _levelManager = LevelManager.Instance;

            UpdateGameState(GameState.loadLevel);
        }

#region STATES
        
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
            
            Debug.Log("Evaluate!");
            UpdateGameState(GameState.evaluate);
        }
        
        private void HandleEvaluate()
        {
            //todo do calculations to determine who starts first, player or enemy? now start player
            Debug.Log("Evaluate...");
            _levelManager.UpdateLevelState();
            UpdateGameState(GameState.playerTurn);
        }

        private void HandlePlayerTurn()
        {
            Debug.Log("player turn!");
        }

        private void HandleCPUTurn()
        {
        }
        
#endregion

#region INPUT-GAMELOOP

        //todo if else if etc a bit ugly.
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.KeypadEnter)) //todo here may cause issues if other conditions come.
            {
                _levelManager.UpdateLevelState(); // todo delete laters this is for test
                
                _lookAtActive = !_lookAtActive;
                _movementActive = !_movementActive;
                
                if (_lookAtActive) _levelManager.StartLookAt();
                else _levelManager.StopLookAtTile();
            }
            
            if (_currentGameState == GameState.playerTurn && _movementActive && _lookAtActive == false)
            {
                for (int i = 0; i < _keyCodes.Length; i++)
                {
                    if (Input.GetKeyDown(_keyCodes[i]))
                    {
                        if (i == 5)
                        {
                            UpdateGameState(GameState.evaluate);
                        }
                        
                        if (_movementVectors.TryGetValue(i, out var motionVector))
                        {
                            _levelManager.MovePlayerTo(motionVector);
                        }
                    }
                }
            }
            else if (_currentGameState == GameState.playerTurn && _movementActive == false && _lookAtActive)
            {
                for (int i = 0; i < _keyCodes.Length; i++)
                {
                    if (Input.GetKeyDown(_keyCodes[i]))
                    {
                        if (_movementVectors.TryGetValue(i, out var motionVector))
                        {
                            _levelManager.LookAtTile(motionVector);
                        }
                    }
                }
            }
        }

        [BoxGroup("debug")]
        [Button]
        public void RollDice(Dice.Dice dice)
        {
            DiceRollHelper.RollRegularDice(dice);
        }

        [Button]
        public void RollDRM()
        {
            DiceRollHelper.RollDRN();
        }

#endregion
        }
    }