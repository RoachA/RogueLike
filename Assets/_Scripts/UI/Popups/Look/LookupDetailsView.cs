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

      public void InitGenericView(Sprite sprite, string thisName, string desc, string weight = default)
      {
         _image.sprite = sprite;
         _nameTxt.text = thisName;
         _descTxt.text = desc;

         bool hasWeight = weight != default;
         _weightObject.SetActive(hasWeight);

         if (hasWeight)
            _weight.text = weight;
      }

      public void InitMeleeWeaponView(MeleeWeaponScriptableData data)
      {
         DisableAllViews();
         _meleeContainer.SetActive(true);
         
         var weightStr = _weight.ToSafeString();
         
         InitGenericView(data._itemSprite, data._itemName, data._itemDesc, weightStr);

         _dmgMeleeTxt.text = DiceRollHelper.GetDiceAsString(data.Stats.BaseDmg);
         _penetrationMeleeTxt.text = "->" + data.Stats.ArmorPenetration.ToString();
      }

      public void InitGearView(WearableScriptableItemData data)
      {
         DisableAllViews();
         _gearsContainer.SetActive(true);
         
         var weightStr = _weight.ToSafeString();
         
         InitGenericView(data._itemSprite, data._itemName, data._itemDesc, weightStr);

         _AVTxt.text = data.Stats.AV.ToString();
         _DVTxt.text = data.Stats.DV.ToString();
      }

      public void InitConsumableView()
      {
         DisableAllViews();
         _consumablesContainer.SetActive(true);
         //todo will make it when consumable data is defined. should be a scriptable item data type.
      }

      public void InitActorView(EntityDynamic entity)
      {
         DisableAllViews();
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

      private void DisableAllViews()
      {
         _meleeContainer.SetActive(false);
         _rangedContainer.SetActive(false);
         _gearsContainer.SetActive(false);
         _actorsContainer.SetActive(false);
         _consumablesContainer.SetActive(false);
      }
   }
}
