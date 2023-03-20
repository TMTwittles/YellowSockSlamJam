using UnityEngine;

public class ShuttleController : MonoBehaviour
{
    [SerializeField] private ShuttleRouteInfoUIController shuttleRouteInfoUIController;
    [SerializeField] private GameObject shuttleMesh;
    private ShuttleRouteData data;
    private Vector3 startPosition;
    private Vector3 route;
    private GameObject activeShuttleRouteInfo;
    
    public void ConfigureShuttle(ShuttleRouteData _data)
    {
        data = _data;
        startPosition = data.ShuttleRouteStartDestination;
        route = data.ShuttleRouteEndDestination - data.ShuttleRouteStartDestination;
        shuttleMesh.transform.forward = route;
        shuttleRouteInfoUIController.ConfigureShuttleRouteInfoController(data);
    }

    void Update()
    {
        transform.position = startPosition + route * data.GetNormalizedTimeTillShuttleRouteComplete();
    }
}
