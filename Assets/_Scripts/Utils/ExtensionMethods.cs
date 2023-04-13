using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Utils
{
   public static class ExtensionMethods
   {
      public static Vector3 ConvertVectorToVector3(this Vector2Int vector, int z)
      {
         return new Vector3(vector.x, vector.y, z);
      }
      
      public static TKey GetRandomKey<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
      {
         // Check if the dictionary is empty
         if (dictionary.Count == 0)
         {
            throw new InvalidOperationException("Dictionary is empty.");
         }

         // Create a random number generator
         System.Random random = new System.Random();

         // Generate a random index
         int index = random.Next(0, dictionary.Count);

         // Get the key at the random index
         TKey randomKey = dictionary.Keys.ElementAt(index);

         return randomKey;
      }
   }
}
