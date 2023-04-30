using Game.Entities.Data;
using UnityEngine;

namespace Game.Entities
{
    public class EntityPlayer : EntityDynamic
    {
        protected PlayerEntityData PlayerData;
        [SerializeField] private GameObject _playerLight; //todo things with this laters

        public void SetPlayerData(PlayerEntityData data)
        {
            PlayerData = data;
        }
    }

}
