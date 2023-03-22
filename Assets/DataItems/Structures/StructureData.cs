using System.Collections.Generic;
using UnityEngine;

public abstract class StructureData : ScriptableObject
{
    [SerializeField] private string structureName;
    public string StructureName => structureName;
    [SerializeField] protected List<StaticResourceData> requiredResources;
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

    public abstract void Tick(PlanetData planetData);
}
