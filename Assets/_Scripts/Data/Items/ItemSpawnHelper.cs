using System.Collections.Generic;
using Game.Data;
public static class ItemSpawnHelper
{
    public static List<WearableItemData> GetRandomWearableItems(int count)
    {
        List<WearableItemData> itemData = DataManager.GetWearableItems();
        List<WearableItemData> randomWearables = new List<WearableItemData>();

        for (int i = 0; i < count; i++)
        {
            randomWearables.Add(itemData[UnityEngine.Random.Range(0, itemData.Count)]);
        }

        return randomWearables;
    }
}
