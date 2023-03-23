using System.Collections.Generic;
using UnityEngine;

public abstract class StructureData : ScriptableObject
{
    [SerializeField] private string structureName;
    public string StructureName => structureName;
    [SerializeField] protected List<StaticResourceData> requiredResources;
    [SerializeField] protected List<StaticResourceData> outputResources;
    public List<StaticResourceData> RequiredResources => requiredResources;
    [SerializeField] protected float timeStructureGenerates;
    public float TimeStructureGenerates => timeStructureGenerates;
    [SerializeField] protected float resourceGain;
    public float ResourceGain => resourceGain;
    [SerializeField] protected float requiredHumans;
    public float RequiredHumans => requiredHumans;
    protected float elapsedTime = 0.0f;
    [SerializeField] private GameObject structureGameObject;
    public GameObject StructureGameObject => structureGameObject;
    protected Dictionary<string, StaticResourceData> outputResourcesDict;
    
    public Dictionary<string, StaticResourceData> OutputResourceDict => outputResourcesDict;

    public abstract void ConfigureStructureData(PlanetData planetData);

    public float GetNormalizedTimeNextResourceGain()
    {
        return (elapsedTime / timeStructureGenerates);
    }

    public abstract void Tick(PlanetData planetData);
}
