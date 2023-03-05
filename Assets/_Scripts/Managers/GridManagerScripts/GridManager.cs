using System;
using System.Collections.Generic;
using System.Linq;
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

   public static GridManager Instance;

   private void Awake()
   {
      Instance = this;
   }

   private void Start()
   {
      GetReferences();
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
   
}
