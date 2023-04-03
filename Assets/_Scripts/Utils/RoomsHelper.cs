using System.Collections.Generic;
using Game.Managers;
using Game.Tiles;
using UnityEngine;

namespace Game.Rooms
{
    public class Room
    {
        public List<TileBase> RoomTiles;

        public Room(List<TileBase> roomtiles)
        {
            RoomTiles = roomtiles;
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
        
        public bool CheckValidity()
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
            Room newRoom = FloodFill(grid, startRow, startColumn);
            //get this new room and find rects:
            DivideIntoRectRooms(newRoom, 3, 3);

            return foundRooms;
        }
        
        private static Room FloodFill(GridManager grid, int startRow, int startColumn)
        {
            //todo this method marks tiles as 'marked' so if we iterate through the grid AGAIN and start another floodFill from the next unmarked and walkable tile, 
            //todo it means we start another floodfill in another room and grab it all. This must be registered as a second Room to be fed to the rect detector.
            //todo rect detector would further populate the detected floods by assigning sub-rooms of rectangle form.
            // todo then we grab those, assign context and use to spawn items.
            Stack<TileBase> stack = new Stack<TileBase>();
            List<TileBase> roomTiles = new List<TileBase>();

            var startCell = SafeStart(grid.GetTile(startRow, startColumn));

            if (startCell == null)
                return new Room(null);
            
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

            foreach (var member in roomTiles) // DEBUG OPERATION
            {
                member.transform.localScale = Vector3.one * 0.85f;
            }
            
            return new Room(roomTiles);
        }

        //todo kinda works but sadly
        private static List<Room> DivideIntoRectRooms(Room room, int minX = 4, int minY = 4)
        {
            List<Room> rooms = new List<Room>();
            List<TileBase> rectRoomTiles = new List<TileBase>();
            List<TileBase> cachedTiles = new List<TileBase>();
            
            var firstTileX = room.GetMinTileX();
            var firstTileY = room.GetMinTileY();
            int xCheck = 0;
            
            for (int i = firstTileX; i <= room.GetMaxTileX(); i++)
            {
                xCheck++;

                for (int j = firstTileY; j <= minY; j++)
                {
                    if (room.GetTileAtPos(i, j, out var tile))
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
            //take the smallest xy tile of the room.
            //add +minY to it and see if this tile exists.
            //if not, move next x, try again.
            //do until it works
            //after it works add all the tiles to a list.
            //try the next x and keep doing until no more x or y fit is found
            //return the list.
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
