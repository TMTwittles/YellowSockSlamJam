using System.Collections.Generic;
using UnityEngine;

public abstract class StructureData : ScriptableObject
{
    [SerializeField] protected List<StaticResourceData> requiredResources;
    public List<StaticResourceData> RequiredResources => requiredResources;
    [SerializeField] protected float timeStructureGenerates;
    public float TimeStructureGenerates => timeStructureGenerates;
    [SerializeField] protected float resourceGain;
    public float ResourceGain => resourceGain;
    protected float elapsedTime = 0.0f;

    public abstract void Tick(PlanetData planetData);
}
