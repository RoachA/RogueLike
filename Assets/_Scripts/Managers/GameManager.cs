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

        public enum PlayerModes
        {
            normal,
            look,
            use,
        }

        public static event Action<GameState> OnGameStateChanged;
      
        private LevelManager _levelManager;
        private GameState _currentGameState;
        private PlayerModes _currentMode;
        private bool _movementActive = true;
        private bool _lookAtActive = false;
        private bool _useActive = false;

        public static Vector2 GridSize;
        

        private readonly KeyCode[] _keyCodes_osx =
        {
            KeyCode.Keypad0,
            KeyCode.Keypad1,
            KeyCode.DownArrow,
            KeyCode.Keypad3,
            KeyCode.LeftArrow,
            KeyCode.W,
            KeyCode.RightArrow,
            KeyCode.Keypad7,
            KeyCode.UpArrow,
            KeyCode.Keypad9,
        };
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
            {5, new Vector2Int(0, 0)},
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

        public void SetPlayerMode(PlayerModes mode)
        {
            _currentMode = mode;
        }

        public PlayerModes GetPlayerMode()
        {
            return _currentMode;
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
            UpdateGameState(GameState.evaluate);
        }
        
        private void HandleEvaluate()
        {
            //todo do calculations to determine who starts first, player or enemy? now start player
            Debug.Log("Evaluate...---------------------------->");
            _levelManager.UpdateLevelState();
            UpdateGameState(GameState.playerTurn);
        }

        private void HandlePlayerTurn()
        {
            Debug.Log("player's turn!---------------------------->");
            
        }

        private void HandleCPUTurn()
        {
        }
        
#endregion

#region INPUT-GAMELOOP

        //TODO HERE NEEDS URGENT REFACTOR!
        //NEEDS SOME PROPER INPUT MANAGER OF SORTS. 
        // CURSOR ACTIVE MODE SHOULD BE A GLOBAL FUNCTION BUT ACTIVE STATE SHOUDLD DEFINE WHAT CURSOR DOES
        //CURRENTLY WE IMPLEMENT SAME MOTION STUFFS IN TWO SEPARETE METHODS IN LEVEL MANAGER.
        //THIS IS STUPıd.!
        private void Update()
        {
            //todo here may cause issues if other conditions come.
            if (_currentGameState == GameState.playerTurn && Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.KeypadEnter)) 
            {
                if (_currentMode == PlayerModes.look)
                    return;
               // _levelManager.UpdateLevelState();
                SetPlayerMode(PlayerModes.look);
                _levelManager.StartLookAt();
            }

            if (_currentGameState == GameState.playerTurn && Input.GetKeyDown(KeyCode.Space))
            {
                if (_currentMode == PlayerModes.use)
                {
                    Debug.Log("Player uses item here.");
                    ResetToNormalMode();
                    return;
                }
                
                _levelManager.UpdateLevelState();
                SetPlayerMode(PlayerModes.use);
                _levelManager.StartUseAt();
            }

            if (_currentGameState == GameState.playerTurn && _currentMode != PlayerModes.normal && Input.GetKeyDown(KeyCode.Escape))
            {
                ResetToNormalMode();
            }
            
            ///movement related situations
            if (_currentGameState == GameState.playerTurn && _currentMode == PlayerModes.normal)
            {
                if (Application.platform == RuntimePlatform.WindowsEditor ||
                    Application.platform == RuntimePlatform.WindowsPlayer)
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
                else if (Application.platform == RuntimePlatform.OSXPlayer ||
                         Application.platform == RuntimePlatform.OSXEditor)
                {
                    for (int i = 0; i < _keyCodes_osx.Length; i++)
                    {
                        if (Input.GetKeyDown(_keyCodes_osx[i]))
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
            }
            
            if (_currentGameState == GameState.playerTurn && _currentMode != PlayerModes.normal)
            {
                for (int i = 0; i < _keyCodes.Length; i++)
                {
                    if (Input.GetKeyDown(_keyCodes[i]))
                    {
                        if (_movementVectors.TryGetValue(i, out var motionVector))
                        {
                            if (_currentMode == PlayerModes.look)
                                _levelManager.LookAtTile(motionVector);
                            
                            if (_currentMode == PlayerModes.use)
                                _levelManager.UseAtTile(motionVector);
                        }
                    }
                }
            }
        }

        private void ResetToNormalMode()
        {
            SetPlayerMode(PlayerModes.normal);
            _levelManager.ResetCursor();
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