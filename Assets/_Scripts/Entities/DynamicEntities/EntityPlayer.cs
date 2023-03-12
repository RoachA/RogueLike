using Game.Entites.Data;

namespace Game.Entites
{
    public class EntityPlayer : EntityDynamic
    {
        protected PlayerEntityData PlayerData;

        public void SetPlayerData(PlayerEntityData data)
        {
            PlayerData = data;
        }
    }

}
