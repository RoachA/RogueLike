using UnityEngine;

namespace Game.Utils
{
   public static class ExtensionMethods
   {
      public static Vector3 ConvertVectorToVector3(this Vector2Int vector, int z)
      {
         return new Vector3(vector.x, vector.y, z);
      }
   }
}
