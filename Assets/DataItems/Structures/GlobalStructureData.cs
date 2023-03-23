using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create GlobalStructureData", fileName = "GlobalStructureData", order = 0)]
public class GlobalStructureData : ScriptableObject
{
    [SerializeField] private List<StructureData> structuresInGame;
    public List<StructureData> StructuresInGame => structuresInGame;

    private Dictionary<string, StructureData> structureDict;
    private Dictionary<string, int> playerStructureCountDict;

    public void ConfigureGlobalStructureData()
    {
        structureDict = new Dictionary<string, StructureData>();
        playerStructureCountDict = new Dictionary<string, int>();
        foreach (StructureData structureData in structuresInGame)
        {
            structureDict.Add(structureData.StructureName, structureData);
            // TODO: This will probs need to be controlled somewhere.
            playerStructureCountDict.Add(structureData.StructureName, 1);
        }
    }

    public StructureData GetStructureData(string structureName)
    {
        return structureDict[structureName];
    }

    public int GetNumStructuresOwnedByPlayer(string structureName)
    {
        return playerStructureCountDict[structureName];
    }

    public void AddToStructureCount(string structureName, int amount = 1)
    {
        playerStructureCountDict[structureName] += 1;
    }

    public void DecreaseStructureCount(string structureName, int amount = 1)
    {
        int playerStructureAmount = playerStructureCountDict[structureName];
        if (playerStructureAmount > 0)
        {
            playerStructureAmount -= 1;   
        }
        playerStructureCountDict[structureName] = playerStructureAmount;
    }
}
