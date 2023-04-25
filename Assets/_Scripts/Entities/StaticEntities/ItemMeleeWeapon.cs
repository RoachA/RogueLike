namespace Game.Data
{
    public class ItemMeleeWeapon : Item
    {
        public ItemMeleeWeapon(MeleeWeaponData data, bool isContained)
        {
            _itemData = data;
            _isContained = isContained;
            GenerateHashId();
        }
    }
}
