using System;
using System.Text;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Dice
{
    public static class DiceRollHelper
    {
        public static int RollRegularDice(Dice dice)
        {
            var sum = 0;
            var roll = 0;
            
            if (dice.D == 0 || dice.N == 0)
            {
                return 0;
            }
            
            for (int i = 0; i < dice.N; i++)
            {
                roll = Random.Range(1, dice.D + 1);
                sum += roll;
            }
            
            return sum;
        }

        /// <summary>
        /// This is a roll of two six-sided dice (2d6) but with an additional bonus:
        /// if any individual die roll is “6,” one is subtracted,
        /// and then that die is re-rolled and added to the result.
        /// This is referred to as an “open-ended” 2d6 roll.
        /// </summary>
        /// <returns></returns>
        public static int RollDRN()
        {
            var sum = 0;
            
            for (int i = 0; i < 2; i++)
            {
                var roll = 0;
                roll = RollRegularDice(new Dice(6, 1));
                sum += roll;

                while (roll == 6)
                {
                    Debug.Log("Critical Roll!");
                    roll = RollRegularDice(new Dice(6, 1));
                    sum = sum - 1 + roll;
                }
            }

            Debug.Log("DRM: " + sum);
            return sum;
        }

        public static string GetDiceAsString(Dice dice)
        {
            return dice.N + "d" + dice.D;
        }
    }

    /// <summary>
    /// Basic DRoll struct
    /// </summary>
    [Serializable]
    public class Dice
    {
        /// <summary>
        /// defines the type of dice.
        /// </summary>
        [BoxGroup("Dice")] public int D;
        
        /// <summary>
        /// defines a roll of N times.
        /// </summary>
        [BoxGroup("Dice")] public int N;
        
        public Dice(int d, int n)
        {
            D = d;
            N = n;
        }
    }
}
