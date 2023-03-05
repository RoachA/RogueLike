using System;
using UnityEngine;

namespace Game.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;

        private GridManager _gridManager;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _gridManager = GridManager.Instance;
        }

        public void CreateLevel()
        {
            _gridManager.GenerateLevelGrid();
        }

        public void LoadLevel()
        {
            
        }
    }
}
