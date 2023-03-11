using System;
using Game.Entites.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Entites
{
    public class EntityStatsView : MonoBehaviour
    {
        [SerializeField] public StatsData _baseStats;

        private EntityDynamic _entity;
        public static Action<EntityDynamic> _entityDiesEvent;
        public static Action<EntityDynamic, int> _entityHPUpdatedEvent;

        public void SetData(DynamicEntityData statsData)
        {
            _baseStats = statsData.StatsData;
        }

        private void Start()
        {
            _entity = GetComponent<EntityDynamic>();
        }

        public int GetMaxHp()
        {
            return _baseStats.BaseStats.HP;
        }
        
        public int GetHp()
        {
            return _baseStats.BaseStats.HP;
        }
        
        [Button]
        public void AddHp(int amount)
        {
            if (amount + _baseStats.BaseStats.HP > _baseStats.BaseStats.MHP)
                _baseStats.BaseStats.HP = _baseStats.BaseStats.MHP;

            _baseStats.BaseStats.HP += amount;
            _entityHPUpdatedEvent?.Invoke(_entity, amount);

            //it is either one inheritor or the other
            if (_baseStats.BaseStats.HP <= 0)
                _entityDiesEvent?.Invoke(_entity.GetType() == typeof(EntityNpc) ? _entity as EntityNpc : _entity as EntityPlayer);
        }
    }
}
