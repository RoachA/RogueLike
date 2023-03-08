using System;
using System.Collections.Generic;
using System.Linq;
using Game.Tiles;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// This algorithm is written for readability. Although it would be perfectly fine in 80% of games, please
    /// don't use this in an RTS without first applying some optimization mentioned in the video: https://youtu.be/i0x5fj4PqP4
    /// If you enjoyed the explanation, be sure to subscribe!
    ///
    /// Also, setting colors and text on each hex affects performance, so removing that will also improve it marginally.
    /// </summary>
    public static class Pathfinding 
    {
        private static readonly Color PathColor = new Color(0f, 1f, 0f);
        private static readonly Color OpenColor = new Color(0, 1, 1);
        private static readonly Color ClosedColor = new Color(1f, 0, 0.5f);
        
        public static List<TileBase> FindPath(TileBase startNode, TileBase targetNode) 
        {
            var toSearch = new List<TileBase>() {startNode};
            var processed = new List<TileBase>();

            while (toSearch.Any()) 
            {
                var current = toSearch[0];
                foreach (var t in toSearch) 
                    if (t.F < current.F || t.F == current.F && t.H < current.H) 
                        current = t;

                processed.Add(current);
                toSearch.Remove(current);
                
                current.SetColor(ClosedColor);

                if (current == targetNode) 
                {
                    var currentPathTile = targetNode;
                    var path = new List<TileBase>();
                    var count = 100;
                    while (currentPathTile != startNode) 
                    {
                        path.Add(currentPathTile);
                        currentPathTile = currentPathTile.Connection;
                        count--;
                        if (count < 0) throw new Exception();
                        Debug.Log("sdfsdf");
                    }
                    
                    foreach (var tile in path) tile.SetColor(PathColor);
                    startNode.SetColor(PathColor);
                    Debug.Log(path.Count);
                    return path;
                }

                foreach (var neighbor in current.Neighbors.Where(t => t.CheckIfWalkable() && !processed.Contains(t))) 
                {
                    var inSearch = toSearch.Contains(neighbor);

                    var costToNeighbor = current.G + current.GetDistance(neighbor);

                    if (!inSearch || costToNeighbor < neighbor.G) 
                    {
                        neighbor.SetG(costToNeighbor);
                        neighbor.SetConnection(current);

                        if (!inSearch) 
                        {
                            neighbor.SetH(neighbor.GetDistance(targetNode));
                            toSearch.Add(neighbor);
                            neighbor.SetColor(OpenColor);
                        }
                    }
                }
            }
            
            return null;
        }
    }
}