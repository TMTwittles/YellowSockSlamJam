using System;
using System.Collections.Generic;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    [SerializeField] private GlobalStructureData globalStructureData;

    public Action PlayerAcquiredNewStructure;
    
    public void ConfigureStructureManager()
    {
        globalStructureData.ConfigureGlobalStructureData();
    }

    public List<StructureData> GetAllStructures()
    {
        return globalStructureData.StructuresInGame;
    }

    public int GetPlayerStructureCount(string structureName)
    {
        return globalStructureData.GetNumStructuresOwnedByPlayer(structureName);
    }
}
