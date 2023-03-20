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
        data.TickPlanetResources();
    }

    public PlanetData GetPlanetData()
    {
        return data;
    }
}
