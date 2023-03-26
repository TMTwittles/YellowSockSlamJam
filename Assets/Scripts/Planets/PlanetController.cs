using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlanetController : MonoBehaviour
{
    [SerializeField] private PlanetInfoUIController planetUIController;
    [FormerlySerializedAs("shuttleController")] [SerializeField] private ShuttleRouteCreator shuttleRouteCreator;
    private PlanetData data;

    public void OnDestroy()
    {
        ScriptableObject.Destroy(data);
        data = null;
        Destroy(gameObject);
    }

    public void Kill()
    {
        data.Kill();
        OnDestroy();
    }

    public void ConfigurePlanet(PlanetData _data)
    {
        data = _data;
        planetUIController.ConfigurePlanetInfoUIController(data);
    }

    public void Update()
    {
        if (data != null)
        {
            data.TickNaturalPlanetResources();   
            data.TickPlanetStructure();
            
            if (data.GetNormalizedTimeTillAnyResourceDepleted() <= 0.0f)
            {
                Kill();
            }
        }
    }

    public bool HasStructure()
    {
        return data.PlanetStructure != null;
    }

    public void AddStructure(StructureData structureData)
    {
        data.AddStructure(structureData);
    }

    public PlanetData GetPlanetData()
    {
        return data;
    }
}
