namespace Game.Entites
{
    public class ItemMeleeWeaponEntity : ItemEntity
    {
        public ItemMeleeWeaponEntity(ItemEntity init)
        {
            _itemData = init.GetItemData<ItemData>();
            _isContained = init.GetIsContained();
        }
    }
}
