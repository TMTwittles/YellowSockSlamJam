using UnityEngine;

public class ShuttleRouteController : MonoBehaviour
{
    [SerializeField] private GameObject shuttleRouteInfoUIDisplay;
    [SerializeField] private GameObject shuttleGameObject;
    private ShuttleRouteData data;
    private GameObject activeShuttleGameObject;
    private GameObject newShuttleRouteInfoDisplay;

    private void OnDrawGizmos()
    {
        if (data != null)
        {
            Gizmos.DrawLine(data.StartPlanetData.PlanetPosition, data.EndPlanetData.PlanetPosition);
        }
    }

    public void ConfigureShuttleRoute(ShuttleRouteData _data)
    {
        data = _data;
        data.ShuttleRouteComplete += OnShuttleRouteComplete;
        data.ShuttleRouteCanceled += OnShuttleRouteComplete;
        newShuttleRouteInfoDisplay = Instantiate(shuttleRouteInfoUIDisplay);
        newShuttleRouteInfoDisplay.transform.position = (data.StartPlanetData.PlanetPosition + data.EndPlanetData.PlanetPosition) / 2;
        newShuttleRouteInfoDisplay.GetComponentInChildren<ShuttleRouteInfoUIController>().ConfigureShuttleRouteInfoController(data);
        //activeShuttleGameObject = Instantiate(shuttleGameObject, data.StartPlanetData.PlanetPosition, Quaternion.identity, transform);
    }

    private void Update()
    {
        if (data != null)
        {
            data.Tick();   
        }
    }

    private void OnShuttleRouteComplete()
    {
        Destroy(newShuttleRouteInfoDisplay);
        Destroy(data);
        Destroy(gameObject);
    }
}
