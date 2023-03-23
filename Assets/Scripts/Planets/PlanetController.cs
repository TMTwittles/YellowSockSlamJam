using UnityEngine;
using UnityEngine.Serialization;

public class PlanetController : MonoBehaviour
{
    [SerializeField] private PlanetInfoUIController planetUIController;
    [FormerlySerializedAs("shuttleController")] [SerializeField] private ShuttleRouteCreator shuttleRouteCreator;
    private PlanetData data;
    
    public void ConfigurePlanet(PlanetData _data)
    {
        data = _data;
        planetUIController.ConfigurePlanetInfoUIController(data);
        shuttleRouteCreator.ConfigureShuttleController(data);
    }

    public void Update()
    {
        if (data != null)
        {
            data.TickNaturalPlanetResources();   
            data.TickPlanetStructure();
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
