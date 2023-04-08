using System;
using System.Collections.Generic;
using System.Linq;
using Game.Tiles;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using Game.Entites;
using Game.Rooms;

namespace Game.Managers
{
   [Serializable]
   public class GridData
   {
      public int Seed;
      public Vector2Int GridSize;
		
      public GridData(Vector2Int gridSize, int seed = 0)
      {
         Seed = seed;
         GridSize = gridSize;
      }
   }
   
   public class GridManager : MonoBehaviour
   {
      [GUIColor(1, 0.9f, 0.9f)] [SerializeField]
      private List<Training> _trainingTemplates;

      [BoxGroup("Generate")] [PropertyOrder(3)] [PropertyRange(0, "_maxRange")] [SerializeField]
      private int _selectedIndex;

      private int _maxRange = 1;

      private OverlapWFC _levelGenerator;
      private GridManagerReferences _gridManagerReferences;
      public Dictionary<Vector2Int, TileBase> _registeredTiles;
      public List<Room> RegisteredRooms;

      public static GridManager Instance;

      [Header("Level Data")]
      [SerializeField] private LevelBlueprint _levelBlueprint;


      void Awake()
      {
         Instance = this;
      }

      void OnValidate()
      {
         GetReferences();
         _maxRange = _trainingTemplates.Count - 1;
      }

      private void GetReferences()
      {
         _gridManagerReferences = GetComponent<GridManagerReferences>();
         _levelGenerator = _gridManagerReferences._levelGenerator;
      }

      [PropertyOrder(1)]
      [Button("Get All Templates")]
      private void GetAllTemplates()
      {
         _trainingTemplates.Clear();
         _trainingTemplates = GetComponentsInChildren<Training>().ToList();
         _maxRange = _trainingTemplates.Count - 1;
      }

      [BoxGroup("Generate")]
      [PropertyOrder(4)]
      [GUIColor(0.9f, 1, 0.9f)]
      [Button("Generate Level")]
      public void GenerateLevelGrid()
      {
         SetGridData();
         _levelGenerator.Clear();
         _levelGenerator.Generate();
         _levelGenerator.Run();

         RegisterTiles();
         CacheNeighboursOfEachTile();
         HandleRooms();
      }

      private void HandleRooms()
      {
         RegisteredRooms = RoomsHelper.FindRooms(this, 10, 10);

         for (var i = 0; i < RegisteredRooms.Count; i++)
         {
            if (i >= _levelBlueprint.Rooms.Length)
               return;
            
            var availableRoom = RegisteredRooms[i];
            Room room = availableRoom;
            room.RoomData = _levelBlueprint.Rooms[i];
            room.InitRoom();
         }
      }

      private void SetGridData()
      {
         //validate the data
         if (_levelBlueprint == null)
         {
            Debug.LogError("No blueprints to read from!");
            return;
         }
         
         if (_levelBlueprint.Dimensions.GridSize.x == 0 || _levelBlueprint.Dimensions.GridSize.y == 0)
         {
            Debug.LogError("Grid size is 0,0 ! Cannot generate a level with this blueprint!");
            return;
         }

         if (_levelBlueprint.N == 0)
         {
            Debug.LogError("sample dimension is 0 N , cannot generate a level with this saple size!");
            return;
         }

         //use data
         _levelGenerator.training = _trainingTemplates[_levelBlueprint.PatternIndex];
         _levelGenerator.SetGridData(_levelBlueprint.Dimensions);
         _levelGenerator.N = _levelBlueprint.N;
         _levelGenerator.periodicInput = _levelBlueprint.PeriodicInput;
         _levelGenerator.periodicOutput = _levelBlueprint.PeriodicOutput;
         _levelGenerator.foundation = _levelBlueprint.Foundation;
         _levelGenerator.symmetry = _levelBlueprint.Symmetry;
         _levelGenerator.hasBorder = _levelBlueprint.HasBorders;
      }

      private void CacheNeighboursOfEachTile()
      {
         foreach (var tile in _registeredTiles)
         {
            tile.Value.CacheNeighbors();
         }
      }

      private void RegisterTiles()
      {
         _registeredTiles = _levelGenerator.Tiles
            .ToDictionary(t => t.GetTilePosId(), t => t);
      }

      [PropertyOrder(2)]
      [Button("Add New Template")]
      private void AddNewTemplate()
      {
         var lastTemplate = _trainingTemplates[^1];

         if (lastTemplate is not null)
         {
            var newTemplate = Instantiate(lastTemplate.transform.parent,
               lastTemplate.transform.position + Vector3.up * lastTemplate.depth * 1.5f, quaternion.identity);
            newTemplate.transform.SetParent(_gridManagerReferences._trainingTemplatesContainer.transform);
            var newTraining = newTemplate.GetComponentInChildren<Training>();
            _trainingTemplates.Add(newTraining);
            newTemplate.gameObject.name = "WFTCanvas_" + _trainingTemplates.Count;
            newTraining.gameObject.name = "template_" + _trainingTemplates.Count;
            _maxRange = _trainingTemplates.Count - 1;
         }
      }

      //todo this method is a bit ugly... how to make it better?
      public bool CheckTileIfHasEntity(int cellX, int cellY, out EntityBase entity)
      {
         if (CheckPosInBounds(cellX, cellY) == false)
         {
            Debug.LogError("The target tile doesn't exist!");
            goto SkipToEnd;
         }

         if (_registeredTiles.TryGetValue(new Vector2Int(cellX, cellY), out TileBase tile))
         {
            if (tile.CheckIfWalkable() == false)
               goto SkipToEnd;

            if (tile.CheckIfWalkable(out var floor))
            {
               if (floor.CheckIfHasEnemy(out var enemy) == false)
                  goto SkipToEnd;

               entity = enemy;
               return true;
            }
         }

         SkipToEnd:
         entity = null;
         return false;
      }

      public string GetTileData(Vector2Int target) //todo later on this will return tile data instead
      {
         //todo also check if tile has an entity read the entity instead
         // > or allow player to iterate through the stack of entities and the tile

         if (_registeredTiles.TryGetValue(target, out TileBase tile))
         {
            return tile.GetTileType();
         }

         return "";
      }

      public LevelBlueprint GetGridBlueprint()
      {
         return _levelBlueprint;
      }

      public TileBase GetTileAtPosition(Vector2Int pos)
      {
         if (_registeredTiles.TryGetValue(pos, out TileBase tile))
         {
            return tile;
         }
         
         return null;
      }

      public bool CheckTileIfHasEntity(int cellX, int cellY)
      {
         return false;
      }

      public bool CheckTileIfWalkable(int cellX, int cellY)
      {
         return GetTile(cellX, cellY).CheckIfWalkable();
      }

      /// <summary>
      /// Gets the tile at a given coordinate.
      /// </summary>
      /// <param name="cellX"></param>
      /// <param name="cellY"></param>
      /// <returns></returns>
      public TileBase GetTile(int cellX, int cellY)
      {
         if (CheckPosInBounds(cellX, cellY) == false)
            Debug.LogError("tile is not within bounds!");

         if (_registeredTiles.TryGetValue(new Vector2Int(cellX, cellY), out var tile) != false)
         {
            return tile;
         }
         else
         {
            Debug.LogError("No tile was found at given position");
            return null;
         }
      }
      
      //need a way to check neighbours.

      /// <summary>
      /// checks the given position in the map returns true if it is within the bounds, false if not.
      /// </summary>
      /// <param name="cellX"></param>
      /// <param name="cellY"></param>
      /// <returns></returns>
      public bool CheckPosInBounds(int cellX, int cellY)
      {
         var array = _levelGenerator.rendering;

         for (int i = -1; i <= 1; ++i)
         for (int j = -1; j <= 1; ++j)
            if ((i != 0) && (j != 0))
            {
               int x = cellX + i;

               if ((x < 0) || (x >= array.GetLength(0)))
                  continue;

               int y = cellY + j;

               if ((y < 0) || (y >= array.GetLength(1)))
                  continue;

               if (array[x, y])
               {
                  return true;
               }
            }

         return false;
      }

      /// <summary>
      /// Returns a list of neighbouring TileBases.
      /// </summary>
      /// <param name="cellX"></param>
      /// <param name="cellY"></param>
      ///
      public List<TileBase> GetNeighbours(int cellX, int cellY)
      {
         int x = cellX;
         int y = cellY;

         int W = _levelGenerator.height;
         int H = _levelGenerator.width;

         List<TileBase> neighbors = new List<TileBase>();

         for (int a = -1; a < 2; a++)
         {
            for (int b = -1; b < 2; b++)
            {
               if (!(a == 0 && b == 0))
               {
                  var nX = x + a;
                  var nY = y + b;
                  if ((nX >= 0 && nX < W) && (nY >= 0 && nY < H))
                  {
                     Vector2Int key = new Vector2Int(nX, nY);

                     if (_registeredTiles.TryGetValue(key, out var tileItem))
                     {
                        neighbors.Add(tileItem);
                     }
                  }
               }
            }
         }

         return neighbors;
      }
      
      [SerializeField] private bool _drawConnections;
      private void OnDrawGizmos() 
      {
         if (!Application.isPlaying || !_drawConnections) return;
         Gizmos.color = Color.red;
         foreach (var tile in _registeredTiles) 
         {
            if (tile.Value.Connection == null) continue;

            Vector2Int tilePos = tile.Key;
            Vector2Int connectionPos = tile.Value.Connection.Coords.Pos;

            Vector3 tilePosv3 = new Vector3(tilePos.x, tilePos.y, -2);
            Vector3 tileConnectionPosV3 = new Vector3(connectionPos.x, connectionPos.y, -2);
            Gizmos.DrawLine(tilePosv3, tileConnectionPosV3);
         }
      }
   }
}
