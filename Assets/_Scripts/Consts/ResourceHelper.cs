using System.Linq;
using Game.Entites;
using UnityEngine;

namespace Game.Managers
{
   public static class ResourceManager
   {
      public static string EntitiesPath = "Entities";
      public static string PlayerEntityPath = "Entities/PlayerEntity";
      public static string NpcEntityPath = "Entities/NpcEntity";
      public static string ScriptableNpcPath = "ScriptableObjects/NPC";
      public static string WeaponsPath = "ScriptableObjects/Items/Weapons/Meelee/";
      public static string SpritesPath = "Sprites";
      public static string PlayerSpriteTmp = "/game_sprites";

      public static Sprite GetPlayerSprite(int index)
      {
         var sprites = Resources.LoadAll<Sprite>(SpritesPath + PlayerSpriteTmp);
         return sprites.FirstOrDefault(s => s.name == "player_" + index);
      }
   }
}
