using System;
using Game.Entites.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Entites
{
    public class EntityStatsView : MonoBehaviour
    {
        [SerializeField] public DynamicEntityStatsData _baseDynamicEntityStats;
        [SerializeField] public DynamicEntityDefinitionData _baseDynamicDefitinion;

        private EntityDynamic _entity;
        public static Action<EntityDynamic> _entityDiesEvent;
        public static Action<EntityDynamic, int> _entityHPUpdatedEvent;

        public void SetData(DynamicEntityScriptableData statsScriptableData)
        {
            _baseDynamicEntityStats = statsScriptableData._dynamicEntityStatsData;
        }

        public void SetData(BaseStatsData statsScriptableData, DynamicEntityDefinitionData definitionData)
        {
            _baseDynamicEntityStats = new DynamicEntityStatsData(statsScriptableData);
            _baseDynamicDefitinion = new DynamicEntityDefinitionData();
            _baseDynamicDefitinion = definitionData;
        }
        
        
        private void Start()
        {
            _entity = GetComponent<EntityDynamic>();
        }

        public int GetMaxHp()
        {
            return _baseDynamicEntityStats.BaseStats.HP;
        }
        
        public int GetHp()
        {
            return _baseDynamicEntityStats.BaseStats.HP;
        }
        
        [Button]
        public void AddHp(int amount)
        {
            if (amount + _baseDynamicEntityStats.BaseStats.HP > _baseDynamicEntityStats.BaseStats.MHP)
                _baseDynamicEntityStats.BaseStats.HP = _baseDynamicEntityStats.BaseStats.MHP;

            _baseDynamicEntityStats.BaseStats.HP += amount;
            _entityHPUpdatedEvent?.Invoke(_entity, amount);

            //it is either one inheritor or the other
            if (_baseDynamicEntityStats.BaseStats.HP <= 0)
                _entityDiesEvent?.Invoke(_entity.GetType() == typeof(EntityNpc) ? _entity as EntityNpc : _entity as EntityPlayer);
        }
    }
}
