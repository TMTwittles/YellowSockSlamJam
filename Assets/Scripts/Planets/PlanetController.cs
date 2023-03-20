using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] private PlanetInfoUIController planetUIController;
    
    private PlanetData data;
    
    public void ConfigurePlanet(PlanetData _data)
    {
        data = _data;
        planetUIController.ConfigurePlanetInfoUIController(data);
    }

    public void Update()
    {
        data.TickPlanetResources();
    }
}
