using System.Collections.Generic;
using System.Linq;
using Game.Data;
using Game.Entites;
using Game.Managers;
using Game.Tiles;
using UnityEngine;

namespace Game.Rooms
{
    public class Room
    {
        public List<TileBase> RoomTiles;
        public RoomTypeData RoomData;

        public Room(List<TileBase> roomtiles, RoomTypeData roomData = null)
        {
            RoomTiles = roomtiles;
            RoomData = roomData;
        }

        #region RoomHelpers

        public bool GetTileAtPos(int x, int y, out TileBase tile)
        {
            tile = null;
            foreach (var roomtile in RoomTiles)
            {
                if (roomtile.GetTilePosId().x == x && roomtile.GetTilePosId().y == y)
                {
                    tile = roomtile;
                    return true;
                }
            }

            return false;
        }

        public void InitRoom()
        {
            InstantiatePropsFromRoomData(RoomData.RoomItems);
        }

        private void InstantiatePropsFromRoomData(RoomItem[] propList)
        {
            var propsData = DataManager.GetAllPropData();
            var propEntityTemplate = DataManager.GetPropEntity();

            var requiredProps = propList.Where(x => propsData.Contains(x.PropData)).ToList();
            
            foreach (var prop in requiredProps)
            {
                var rndCount = Random.Range(1, prop.MaxNumberOfDuplicates + 1); //at least one but max duplicates vary.
                
                for (int i = 0; i < rndCount; i++)
                {
                    var newProp = Object.Instantiate(propEntityTemplate, RoomTiles[0].transform.parent);

                    TileBase rndTile = RoomTiles[Random.Range(0, RoomTiles.Count)];
                    
                    while (CheckTileValidity(rndTile, prop.PropData.OrientationType == PropOrientationType.WallBound) == false) //risky business
                    {
                        rndTile = RoomTiles[Random.Range(0, RoomTiles.Count)];
                    }
                    
                    rndTile.SetWalkable(false);
                    var rndPos = rndTile.GetTilePosId();
                
                    newProp.Data = prop.PropData;
                    if (prop.PropData.AdditionalItem != null)
                    {
                        var child = Object.Instantiate(prop.PropData.AdditionalItem, newProp.transform);
                        child.transform.localPosition = Vector3.zero;
                    }
                    
                    newProp.InitProp();
                    newProp.SetOccupiedTile(rndTile);
                    newProp.transform.position = new Vector3(rndPos.x, rndPos.y, 1);
                    rndTile.AddEntityToTile(newProp);
                }
            }
        }

        private bool CheckTileValidity(TileBase tile, bool isWallBound)
        {
            var neighbours = tile.GetCardinalNeighbours();
            bool hasNeighborWall = false;
           
            if (tile.CheckIfWalkable())
            {
                foreach (var neighbour in neighbours)
                {
                    if (neighbour.GetType() == typeof(TileDoor))
                        return false;

                    if (neighbour.GetType() == typeof(TileWall) && isWallBound)
                        hasNeighborWall = true;
                }
            }
            else
            {
                return false;
            }

            if (hasNeighborWall == false && isWallBound)
                return false;
            
            return true;
        }
        
        public bool CheckRoomValidity()
        {
            if (RoomTiles == null) return false;
            if (RoomTiles.Count == 0) return false;
            return true;
        }

        public bool GetUpTile(TileBase tile, out TileBase neighbor)
        {
            neighbor = null;
            
            foreach (var roomTile in RoomTiles)
            {
                if (roomTile.GetTilePosId().y == tile.GetTilePosId().y + 1)
                {
                    neighbor = roomTile;
                    return true;
                }
            }

            return false;
        }
        
        public bool GetRightTile(TileBase tile, out TileBase neighbor)
        {
            neighbor = null;
            
            foreach (var roomTile in RoomTiles)
            {
                if (roomTile.GetTilePosId().x == tile.GetTilePosId().x + 1)
                {
                    neighbor = roomTile;
                    return true;
                }
            }

            return false;
        }
        
/// <summary>
/// returns dimensions of the room definition ->
/// x : minX , y : minY, z : maxX, v : maxY 
/// </summary>
/// <returns></returns>
        public Vector4 GetRoomDimensions()
        {
            return new Vector4(GetMaxTileX(), GetMinTileY(), GetMaxTileX(), GetMinTileY());
        }

        public int GetMaxTileX()
        {
            int max = 0;

            foreach (var tile in RoomTiles)
            {
                var tilePos = tile.GetTilePosId();
                
                if (tilePos.x > max)
                    max = tilePos.x;
            }

            Debug.LogWarning("max X tile is: " + max);
            return max;
        }
        
        public int GetMaxTileY()
        {
            int max = 0;

            foreach (var tile in RoomTiles)
            {
                var tilePos = tile.GetTilePosId();
                
                if (tilePos.y > max)
                    max = tilePos.y;
            }
            
            return max;
        }
        
        public int GetMinTileX()
        {
            int min = 10000;

            foreach (var tile in RoomTiles)
            {
                var tilePos = tile.GetTilePosId();
                
                if (tilePos.x < min)
                    min = tilePos.x;
            }

            Debug.LogWarning("min X tile is: " + min);
            return min;
        }
        
        public int GetMinTileY()
        {
            int min = 10000;

            foreach (var tile in RoomTiles)
            {
                var tilePos = tile.GetTilePosId();
                
                if (tilePos.y < min)
                    min = tilePos.y;
            }

            Debug.LogWarning("min Y tile is: " + min);
            return min;
        }


        public int GetRoomHeight()
        {
            int max = 0;
            int min = 10000;

            foreach (var tile in RoomTiles)
            {
                var tilePos = tile.GetTilePosId();

                if (tilePos.y < min)
                    min = tilePos.y;
                if (tilePos.y > max)
                    max = tilePos.y;
            }

            return max - min + 1;
        }

        public int GetRoomWidth()
        {
            int max = 0;
            int min = 10000;

            foreach (var tile in RoomTiles)
            {
                var tilePos = tile.GetTilePosId();

                if (tilePos.x < min)
                    min = tilePos.y;
                if (tilePos.x > max)
                    max = tilePos.y;
            } 
            
            return max - min + 1;
        }
        
        #endregion  
    }
    
    public static class RoomsHelper
    {
        //todo all this should be done before entities are generated.
        public static List<Room> FindRooms(GridManager grid, int startRow, int startColumn)
        {
            var foundRooms = new List<Room>();

            while (SearchUnmarkedTile(grid, out var pos) == true)
            {
                if (FloodFill(grid, pos.x, pos.y, out var room))
                {
                    foundRooms.Add(room);
                }
            }
            
            //get this new room and find rects:
            //DivideIntoRectRooms(newRoom, 3, 3);

            return foundRooms;
        }
        
        private static bool FloodFill(GridManager grid, int startRow, int startColumn, out Room room)
        {
            //todo this method marks tiles as 'marked' so if we iterate through the grid AGAIN and start another floodFill from the next unmarked and walkable tile, 
            //todo it means we start another floodfill in another room and grab it all. This must be registered as a second Room to be fed to the rect detector.
            //todo rect detector would further populate the detected floods by assigning sub-rooms of rectangle form.
            // todo then we grab those, assign context and use to spawn items.
            Stack<TileBase> stack = new Stack<TileBase>();
            List<TileBase> roomTiles = new List<TileBase>();

            var startCell = SafeStart(grid.GetTile(startRow, startColumn));
            room = new Room(null);

            if (startCell == null)
                return false;
            
            stack.Push(startCell);
            startCell.SetMarkedState(true);

            while (stack.Count > 0)
            {
                TileBase cell = stack.Pop();
                Vector2Int tilePos = cell.GetTilePosId();
                // process the cell here as needed
                for (int r = -1; r <= 1; r++)
                {
                    for (int c = -1; c <= 1; c++)
                    {
                        if (r == 0 && c == 0) continue; // skip the current cell
                        //if x+1 x-1 or y+1 y-1 are walls > stop
                        TileBase neighbor = grid.GetTile(cell.GetTilePosId().x + r, cell.GetTilePosId().y + c); 
                        
                        //a bit expensive no? >> checks if the pass is narrow and if so, skips further check towards it
                        
                        //actually each tile already holds data about their neighbours. We could use that list instead?
                        if (neighbor != null && neighbor.CheckIfWalkable() == cell.CheckIfWalkable() && !neighbor.GetMarkedState())
                        {
                            //ShouldSkipCell(grid, cell, 1);

                            if (r == -1 && grid.GetTile(tilePos.x, tilePos.y - 1)?.CheckIfWalkable() == false && grid.GetTile(tilePos.x, tilePos.y + 1)?.CheckIfWalkable() == false) continue;
                            if (r == 1 && grid.GetTile(tilePos.x, tilePos.y - 1)?.CheckIfWalkable() == false && grid.GetTile(tilePos.x, tilePos.y + 1)?.CheckIfWalkable() == false) continue;
                            if (c == -1 && grid.GetTile(tilePos.x - 1, tilePos.y)?.CheckIfWalkable() == false && grid.GetTile(tilePos.x + 1, tilePos.y)?.CheckIfWalkable() == false) continue;
                            if (c == 1 && grid.GetTile(tilePos.x - 1, tilePos.y)?.CheckIfWalkable() == false && grid.GetTile(tilePos.x + 1, tilePos.y)?.CheckIfWalkable() == false) continue;
                            
                            neighbor.SetMarkedState(true);
                            //somehow check narrow points.
                            roomTiles.Add(neighbor);
                            stack.Push(neighbor);
                        }
                    }
                }
            }
            
            if (roomTiles.Count < 16)
            {
                return false;
            }
            
            /*foreach (var member in roomTiles) // DEBUG OPERATION
            {
                member.transform.localScale = Vector3.one * 0.85f;
            }*/
            
            room = new Room(roomTiles);
            return true;
        }

        private static bool SearchUnmarkedTile(GridManager grid, out Vector2Int umarkedTile)
        {
            umarkedTile = Vector2Int.zero;
            
            foreach (var tile in grid._registeredTiles)
            {
                if (tile.Value.GetMarkedState() == false && tile.Value.CheckIfWalkable())
                {
                    umarkedTile = tile.Value.GetTilePosId();
                    return true;
                }
            }

            return false;
        }

        //todo kinda works but sadly
        private static List<Room> DivideIntoRectRooms(Room floodFillRoom, int minX = 4, int minY = 4)
        {
            //get x min
            //iterate as follows
            // x1 >>> y1 y2 y3 y4 stop when reaches an obstacle
            // do the same with x2 until no xn remains
            //n must be greater than parameter minX minY >>> if not get xn + 2 start all over again. if no more x on the grid, go last y max + 2 and start again.
            //keep min N index for y > once xn is reached discard extra y's from each x list
            //now you have a series of lists with columns keeping rect tiles
            //combine all into one great list and call it a room then add to the rooms list
            // next?
            
            // get the xMax of the previously registered room. iterate x++ until an unmarked floor tile is found
            //repeat the process above.
            
            //go y++ find first available tile repeat the process. first y++ then x++
            
            List<Room> rooms = new List<Room>();
            List<TileBase> rectRoomTiles = new List<TileBase>();
            List<TileBase> cachedTiles = new List<TileBase>();
            
            var firstTileX = floodFillRoom.GetMinTileX();
            var firstTileY = floodFillRoom.GetMinTileY();
            int xCheck = 0;
            
            for (int i = firstTileX; i <= floodFillRoom.GetMaxTileX(); i++)
            {
                xCheck++;

                for (int j = firstTileY; j <= minY; j++)
                {
                    if (floodFillRoom.GetTileAtPos(i, j, out var tile))
                    {
                        cachedTiles.Add(tile);
                    }
                }
                
                if (cachedTiles.Count == minY)
                {   
                    foreach (var cache in cachedTiles)
                    {
                        rectRoomTiles.Add(cache);
                    }
                }
                else
                {
                    rectRoomTiles.Clear();
                    xCheck = 0;
                }
                
                cachedTiles.Clear();
            }

            foreach (var rect in rectRoomTiles)
            {
                rect.transform.localScale = Vector3.one * 0.5f;
            }

            rooms.Add(new Room(rectRoomTiles));
            return rooms;
        }

        private static TileBase SafeStart(TileBase startCell)
        {
            TileBase newTile = startCell;
            if (startCell.CheckIfWalkable())
                return newTile;

            foreach (var neighbor in startCell.Neighbors)
            {
                if (neighbor.CheckIfWalkable() == false) continue;
                
                newTile = neighbor;
            }

            return newTile;
        }
        
        private static bool ShouldSkipCell(GridManager grid, TileBase tile, int gap)
        {
            Vector2Int tilePos = tile.GetTilePosId();
            for (int r = -1; r <= 1; r++)
            {
                for (int c = -1; c <= 1; c++)
                {
                    if (r == 0 && c == 0) continue;
                    if (r == -c || r == c) continue; // diagonal neighbor
                    
                    TileBase neighbor = grid.GetTile(tilePos.x + r, tilePos.y + c);
                    
                    if (neighbor != null && !neighbor.CheckIfWalkable())
                    {
                        int count = 1;
                        while (count <= gap)
                        {
                            if (r == 0)
                            {
                                if (grid.GetTile(tilePos.x - count, tilePos.y)?.CheckIfWalkable() == false) return true;
                                if (grid.GetTile(tilePos.x + count, tilePos.y)?.CheckIfWalkable() == false) return true;
                            }
                            else if (c == 0)
                            {
                                if (grid.GetTile(tilePos.x, tilePos.y - count)?.CheckIfWalkable() == false) return true;
                                if (grid.GetTile(tilePos.x, tilePos.y + count)?.CheckIfWalkable() == false) return true;
                            }
                            
                            count++;
                        }
                    }
                }
            }
            
            return false;
        }
    }
}
