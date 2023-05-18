using System;
using Game.Data;
using Game.Dice;
using Game.Entities;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
   public class LookupDetailsView : MonoBehaviour
   {
      [SerializeField] private TextMeshProUGUI _nameTxt;
      [SerializeField] private TextMeshProUGUI _descTxt;
      [SerializeField] private Image _image;
      [SerializeField] private GameObject _container;

      [Header("Shared")]
      [SerializeField] private GameObject _weightObject;
      [SerializeField] private TextMeshProUGUI _weight;

      [Header("Melee-Weapon View")]
      [SerializeField] private GameObject _meleeContainer;
      [SerializeField] private TextMeshProUGUI _dmgMeleeTxt;
      [SerializeField] private TextMeshProUGUI _penetrationMeleeTxt;

      [Header("Ranged-Weapon View")]
      [SerializeField] private GameObject _rangedContainer;

      [Header("Gear View")]
      [SerializeField] private GameObject _gearsContainer;
      [SerializeField] private TextMeshProUGUI _AVTxt;
      [SerializeField] private TextMeshProUGUI _DVTxt;
      [SerializeField] private TextMeshProUGUI _bonusesTxt;

      [Header("Consumable View")]
      [SerializeField] private GameObject _consumablesContainer;
      [SerializeField] private TextMeshProUGUI _effectsTxt;

      [Header("Actor View")]
      [SerializeField] private GameObject _actorsContainer;
      [SerializeField] private TextMeshProUGUI _conditionTxt;
      [SerializeField] private TextMeshProUGUI _equippedItemsTxt;
      [SerializeField] private TextMeshProUGUI _demeanorTxt;

      private void Start()
      {
         DisableAllViews();
         LookUpListItemView._lookActor += InitActorView;
         LookUpListItemView._lookGear += InitGearView;
         LookUpListItemView._lookTile += InitTileView;
         LookUpListItemView._lookProp += InitPropView;
         LookUpListItemView._lookMeleeWeapon += InitMeleeWeaponView;
      }

      public void InitGenericView(Sprite sprite, string thisName, string desc, string weight = default)
      {
         DisableAllViews();
         _container.SetActive(true);
         
         _image.sprite = sprite;
         _image.preserveAspect = true;
         _nameTxt.text = thisName;
         _descTxt.text = desc;

         bool hasWeight = weight != default;
         _weightObject.SetActive(hasWeight);

         if (hasWeight)
            _weight.text = weight;
      }

      public void InitMeleeWeaponView(MeleeWeaponScriptableData data)
      {
         var weightStr = _weight.ToSafeString();
         
         InitGenericView(data._itemSprite, data._itemName, data._itemDesc, weightStr);
         _meleeContainer.SetActive(true);
         
         _dmgMeleeTxt.text = DiceRollHelper.GetDiceAsString(data.Stats.BaseDmg);
         _penetrationMeleeTxt.text = "->" + data.Stats.ArmorPenetration.ToString();
      }

      public void InitGearView(WearableScriptableItemData data)
      {
         var weightStr = _weight.ToSafeString();
         InitGenericView(data._itemSprite, data._itemName, data._itemDesc, weightStr);
         _gearsContainer.SetActive(true);
         _AVTxt.text = data.Stats.AV.ToString();
         _DVTxt.text = data.Stats.DV.ToString();
      }

      public void InitTileView(TileTypeData data)
      {
         InitGenericView(data.TileSprite_A, data.TileName, data.TileDesc);
      }

      public void InitPropView(PropEntityData data)
      {
         InitGenericView(data.Sprite[0], data.name, data.Desc);
      }

      public void InitConsumableView()
      {
         _consumablesContainer.SetActive(true);
         //todo will make it when consumable data is defined. should be a scriptable item data type.
      }

      public void InitActorView(EntityDynamic entity)
      {
         _actorsContainer.SetActive(true);
         
         //todo later on we may show race, gender etc as well.
         var definitionData = entity.GetDefinitionData();
         var equippedItems = entity.GetEquippedItems();
         var statsData = entity.GetStats();
         string equippedItemsString = "";
         
         InitGenericView(definitionData.Sprite, definitionData._entityName, definitionData.Description, default);

         foreach (var item in equippedItems)
         {
            equippedItemsString += item.Value.GetItemData<ScriptableItemData>()._itemName + ", ";
         }

         _equippedItemsTxt.text = equippedItemsString;

         if (entity.GetType() == typeof(EntityNpc))
         {
            EntityNpc npc;
            npc = entity as EntityNpc;
            _demeanorTxt.text = npc.GetDemeanor().ToString();
         }
         else
            _demeanorTxt.text = "";

         string condition = GetHealthStatus(statsData.MHP, statsData.HP);
         _conditionTxt.text = condition;
      }

      private string GetHealthStatus(int maxHp, int hp)
      {
         string status = hp < maxHp * 0.1f ? "severely wounded" :
            hp < maxHp * 0.3f ? "badly wounded" :
            hp < maxHp * 0.5f ? "wounded" :
            hp < maxHp * 0.7f ? "slightly wounded" :
            "healthy";

         return status;
      }

      private void OnDisable()
      {
         DisableAllViews();
      }

      private void DisableAllViews()
      {
         _container.SetActive(false);
         _meleeContainer.SetActive(false);
         _rangedContainer.SetActive(false);
         _gearsContainer.SetActive(false);
         _actorsContainer.SetActive(false);
         _consumablesContainer.SetActive(false);
      }
   }
}
