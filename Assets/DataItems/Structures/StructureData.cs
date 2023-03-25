using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class StructureData : ScriptableObject
{
    [SerializeField] private Sprite structureIcon;
    public Sprite StructureIcon => structureIcon;
    
    
    [SerializeField] private string structureName;
    public string StructureName => structureName;
    [SerializeField] protected List<StaticResourceData> requiredResources;
    [SerializeField] protected List<StaticResourceData> outputResources;

    [SerializeField] private float requiredResourceAmount;
    public float RequiredResourceAmount => requiredResourceAmount;
    
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

    [SerializeField, TextArea(15, 20)] private string structureDescription;

    [SerializeField] private int startingAmount;
    public int StartingAmount => startingAmount;

    [FormerlySerializedAs("startingAmount")] [SerializeField]
    private int amount = 0;
    public int Amount => amount;
    public string StructureDescription => structureDescription;
    
    public Dictionary<string, StaticResourceData> OutputResourceDict => outputResourcesDict;

    public abstract void ConfigureStructureData(PlanetData planetData);

    public float GetNormalizedTimeNextResourceGain()
    {
        return (elapsedTime / timeStructureGenerates);
    }

    public abstract void Tick(PlanetData planetData);

    public void SetAmount()
    {
        amount = startingAmount;
    }

    public void AddToAmount()
    {
        amount += 1;
    }

    public void DecreaseAmount()
    {
        amount -= 1;
    }
}
