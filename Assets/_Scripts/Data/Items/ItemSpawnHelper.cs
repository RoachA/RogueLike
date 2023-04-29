using System.Collections.Generic;
using Game.Data;
public static class ItemSpawnHelper
{
    public static List<WearableItemDefinitionData> GetRandomWearableItems(int count)
    {
        List<WearableItemDefinitionData> itemData = DataManager.GetWearableItems();
        List<WearableItemDefinitionData> randomWearables = new List<WearableItemDefinitionData>();

        for (int i = 0; i < count; i++)
        {
            randomWearables.Add(itemData[UnityEngine.Random.Range(0, itemData.Count)]);
        }

        return randomWearables;
    }
}
