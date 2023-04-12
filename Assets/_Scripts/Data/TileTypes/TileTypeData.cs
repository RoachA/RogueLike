using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "TILE_DATA", menuName = "Environment Data/TILE_TYPE", order = 1)]
public class TileTypeData : ScriptableObject
{
    public string TileName;
    public TileTypeEnum TileType;
    public Sprite TileSprite_A;
    [ShowIf("TileType", TileTypeEnum.Door)]
    public Sprite TileSprite_B;
    [TextArea]
    [Space]
    public string TileDesc;

    public TileTypeData(TileTypeEnum tileType, Sprite tileSpriteA, string tileName, string tileDesc)
    {
        TileType = tileType;
        TileSprite_A = tileSpriteA;
        TileName = tileName;
        TileDesc = tileDesc;
    }
    
    public TileTypeData(TileTypeEnum tileType, Sprite tileSpriteA, Sprite tileSpriteB, string tileName, string tileDesc)
    {
        TileType = tileType;
        TileSprite_A = tileSpriteA;
        TileSprite_B = tileSpriteB;
        TileName = tileName;
        TileDesc = tileDesc;
    }
}

public enum TileTypeEnum
{
    Wall,
    Floor,
    Door,
}
