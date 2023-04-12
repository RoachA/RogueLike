using System;
using Game.Managers;
using Game.Rooms;
using UnityEngine;

[CreateAssetMenu(fileName = "LEVE_BLUEPRINT", menuName = "Data Set/LEVEL_DATA", order = 1)]
[Serializable]
public class LevelBlueprint : ScriptableObject
{
   public string LevelIdentifier;
   public int PatternIndex;
   public GridData Dimensions;
   public int Width;
   public int Height;
   public int N;
   public bool PeriodicInput;
   public bool PeriodicOutput;
   public int Symmetry;
   public int Foundation;
   public TileTypeDataSet DefaultTileTypes;
   public LevelType Type;
   public RoomTypeData[] Rooms;
   public bool HasBorders;
   
   public LevelBlueprint(string levelIdentifier, int patternIndex, GridData dimensions, int n, bool periodicInput, bool periodicOutput, int symmetry, int foundation, bool hasBorders, LevelType levelType, TileTypeDataSet defaultTileTypes)
   {
      LevelIdentifier = levelIdentifier;
      PatternIndex = patternIndex;
      Dimensions = dimensions;
      N = n;
      PeriodicInput = periodicInput;
      PeriodicOutput = periodicOutput;
      Symmetry = symmetry;
      Foundation = foundation;
      HasBorders = hasBorders;
      Type = levelType;
      DefaultTileTypes = defaultTileTypes;
   }
   
   public LevelBlueprint(string levelIdentifier, int patternIndex, GridData dimensions, int n, bool periodicInput, bool periodicOutput, int symmetry, int foundation, bool hasBorders, LevelType levelType, RoomTypeData[] rooms, TileTypeDataSet defaultTileTypes)
   {
      LevelIdentifier = levelIdentifier;
      PatternIndex = patternIndex;
      Dimensions = dimensions;
      N = n;
      PeriodicInput = periodicInput;
      PeriodicOutput = periodicOutput;
      Symmetry = symmetry;
      Foundation = foundation;
      HasBorders = hasBorders;
      Type = levelType;
      Rooms = rooms;
      DefaultTileTypes = defaultTileTypes;
   }
   
   public enum LevelType
   {
      Interior,
      Exterior,
   }
}
