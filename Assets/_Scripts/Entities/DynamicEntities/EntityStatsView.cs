using System;
using Game.Entites.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Entites
{
    public class EntityStatsView : MonoBehaviour
    {
        [SerializeField] public DynamicEntityStatsData _stats;
        [SerializeField] public DynamicEntityDefinitionData _definition;

        private EntityDynamic _entity;
        public static Action<EntityDynamic> _entityDiesEvent;
        public static Action<EntityDynamic, int> _entityHPUpdatedEvent;

        public void SetData(DynamicEntityScriptableData statsScriptableData)
        {
            _stats = statsScriptableData._dynamicEntityStatsData;
        }

        public void SetData(BaseStatsData statsScriptableData, DynamicEntityDefinitionData definitionData)
        {
            _stats = new DynamicEntityStatsData(statsScriptableData);
            _definition = new DynamicEntityDefinitionData(definitionData);
           // _definition = definitionData;
        }
        
        private void Start()
        {
            _entity = GetComponent<EntityDynamic>();
        }

        public BaseStatsData GetBaseStats()
        {
            return _stats.BaseStats;
        }
        
        [Button]
        public void AddHp(int amount)
        {
            if (amount + _stats.BaseStats.HP > _stats.BaseStats.MHP)
                _stats.BaseStats.HP = _stats.BaseStats.MHP;

            _stats.BaseStats.HP += amount;
            _entityHPUpdatedEvent?.Invoke(_entity, amount);

            //it is either one inheritor or the other
            if (_stats.BaseStats.HP <= 0)
                _entityDiesEvent?.Invoke(_entity.GetType() == typeof(EntityNpc) ? _entity as EntityNpc : _entity as EntityPlayer);
        }
        
        public int GetMaxHp()
        {
            return _stats.BaseStats.HP;
        }
        
        public int GetHp()
        {
            return _stats.BaseStats.HP;
        }
    }
}
