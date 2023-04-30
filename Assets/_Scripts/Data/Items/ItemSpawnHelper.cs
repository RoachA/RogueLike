using System.Collections.Generic;
using Game.Data;
public static class ItemSpawnHelper
{
    public static List<WearableScriptableItemData> GetRandomWearableItems(int count)
    {
        List<WearableScriptableItemData> itemData = DataManager.GetWearableItems();
        List<WearableScriptableItemData> randomWearables = new List<WearableScriptableItemData>();

        for (int i = 0; i < count; i++)
        {
            randomWearables.Add(itemData[UnityEngine.Random.Range(0, itemData.Count)]);
        }

        return randomWearables;
    }
}
