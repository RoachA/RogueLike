using System;
using System.Collections.Generic;
using Game.Entites;
using UnityEngine;

namespace Game.Rooms
{
    [CreateAssetMenu(fileName = "ROOM_DATA", menuName = "Environment Data/Room", order = 1)]
    public class RoomTypeData : ScriptableObject
    {
        public RoomTypeEnum RoomType;
        public RoomItem[] RoomItems;
        public List<TileTypeData> FloorTileTypes;

        public RoomTypeData(RoomTypeEnum roomType, RoomItem[] roomItems, List<TileTypeData> floorTileTypes)
        {
            RoomType = roomType;
            RoomItems = roomItems;
            FloorTileTypes = floorTileTypes;
        }
    }

    [Serializable]
    public struct RoomItem
    {
        public int MaxNumberOfDuplicates;
        public PropEntityData PropData;

        public RoomItem(PropEntityData propData, int maxNumberOfDuplicates)
        {
            PropData = propData;
            MaxNumberOfDuplicates = maxNumberOfDuplicates;
        }
    }
    
    public enum RoomTypeEnum
    {
        LivingRoom,
        Kitchen,
        Bedroom,
        Toilet,
        Hall,
    }
}
