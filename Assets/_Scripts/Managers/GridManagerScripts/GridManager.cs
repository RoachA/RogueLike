using System.Collections.Generic;
using System.Linq;
using Game.Tiles;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class GridManager : MonoBehaviour
{
   [GUIColor(1, 0.9f, 0.9f)]
   [SerializeField] private List<Training> _trainingTemplates;

   [BoxGroup("Generate")]
   [PropertyOrder(3)]
   [PropertyRange(0, "_maxRange")]
   [SerializeField] private int _selectedIndex;
   private int _maxRange = 1;
   
   private OverlapWFC _levelGenerator;
   private GridManagerReferences _gridManagerReferences;
   public List<TileBase> _registeredTiles;

   public static GridManager Instance;

   private void Awake()
   {
      Instance = this;
   }
   
   private void OnValidate()
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
      _levelGenerator.training = _trainingTemplates[_selectedIndex];
      _levelGenerator.Generate();
      _levelGenerator.Run();
      
      RegisterTiles();
   }

   private void RegisterTiles()
   {
      _registeredTiles = _levelGenerator.GetComponentsInChildren<Game.Tiles.TileBase>().ToList();
   }

   [PropertyOrder(2)]
   [Button("Add New Template")]
   private void AddNewTemplate()
   {
      var lastTemplate = _trainingTemplates[^1];
      
      if (lastTemplate is not null)
      {
         var newTemplate = Instantiate(lastTemplate.transform.parent, lastTemplate.transform.position + Vector3.up * lastTemplate.depth * 1.5f, quaternion.identity);
         newTemplate.transform.SetParent(_gridManagerReferences._trainingTemplatesContainer.transform);
         var newTraining = newTemplate.GetComponentInChildren<Training>();
         _trainingTemplates.Add(newTraining);
         newTemplate.gameObject.name = "WFTCanvas_" + _trainingTemplates.Count;
         newTraining.gameObject.name = "template_" + _trainingTemplates.Count;
         _maxRange = _trainingTemplates.Count - 1;
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
               Debug.LogError("TRUE");
               return true;
            }
         }
      
      Debug.LogError("FALSE");
      return false;
   }

   [Button("CheckNeighbours")]
   public void GetNeighbours(int cellX, int cellY)
   {
      int x = cellX;
      int y = cellY;

      int W = _levelGenerator.depth;
      int H = _levelGenerator.width;

      List<GameObject> neighbors = new List<GameObject>();

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
                  neighbors.Add(_levelGenerator.rendering[nX, nY]);
               }
            }
         }
      }

      foreach (var neighbor in neighbors)
      {
         ;
         Debug.LogError(neighbor.GetComponent<TileBase>().GetTilePosId());
      }
   }
   
}