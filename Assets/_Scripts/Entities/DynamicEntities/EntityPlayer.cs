using Game.Entites.Data;

namespace Game.Entites
{
    public class EntityPlayer : EntityDynamic
    {
        protected PlayerEntityScriptableData PlayerScriptableData;

        public void SetPlayerData(PlayerEntityScriptableData scriptableData)
        {
            PlayerScriptableData = scriptableData;
        }
    }

}
