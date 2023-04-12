using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "TILE_DATA_SET", menuName = "Environment Data/TILE_DATA_SET", order = 1)]
public class TileTypeDataSet : ScriptableObject
{
    public string TileSetIdentifier = "";
    public List<TileTypeData> RegisteredFloorTypes;
    public List<TileTypeData> RegisteredWallTypes;
    public List<TileTypeData> RegisteredDoorTypes;

    public List<TileTypeData> GetTilesOfType(TileTypeEnum tileType)
    {
        List<TileTypeData> list = new List<TileTypeData>();

        switch (tileType)
        {
            case TileTypeEnum.Door:
                list = RegisteredDoorTypes;
                break;
            case TileTypeEnum.Floor:
                list = RegisteredFloorTypes;
                break;
            case TileTypeEnum.Wall:
                list = RegisteredWallTypes;
                break;
        }
        
        if (list.Count == 0)
            Debug.LogError("Registered tiles list contains no data.");

        return list;
    }

    [Button]
    private void ValidateRegistries()
    {
        int errors = 0;
        foreach (var floor in RegisteredFloorTypes)
        {
            if (floor.TileType != TileTypeEnum.Floor)
            {
                Debug.LogError(floor.name + " is not a floor type!");
                errors++;
            }
        }
        
        foreach (var wall in RegisteredWallTypes)
        {
            if (wall.TileType != TileTypeEnum.Wall)
            {
                Debug.LogError(wall.name + " is not a wall type!");
                errors++;
            }
        }
        
        foreach (var door in RegisteredDoorTypes)
        {
            if (door.TileType != TileTypeEnum.Door)
            {
                Debug.LogError(door.name + " is not a door type!");
                errors++;
            }
        }

        if (TileSetIdentifier == "" || TileSetIdentifier == default)
        {
            Debug.LogError("Tile set needs an identifier value!");
            errors++;
        }
        
        if (errors == 0)
            Debug.LogWarning("Tile set is valid.");
    }
}
