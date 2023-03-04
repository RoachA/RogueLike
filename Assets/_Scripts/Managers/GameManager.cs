using UnityEngine;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        void Start()
        {
          RoguelikeGeneratorPro.RoguelikeGeneratorPro.Instance.RigenenerateLevel();
        }

        void Update()
        {
        }
    }
}
