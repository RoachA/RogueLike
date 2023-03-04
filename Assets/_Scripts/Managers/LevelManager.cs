using UnityEngine;

namespace Game.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}
