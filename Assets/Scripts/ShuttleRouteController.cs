using UnityEngine;

public class ShuttleRouteController : MonoBehaviour
{
    [SerializeField] private GameObject shuttleGameObject;
    [SerializeField] private LineRenderer lineRenderer;
    private ShuttleRouteData data;
    private GameObject activeShuttleGameObject;
    private GameObject newShuttleRouteInfoDisplay;

    private void OnDrawGizmos()
    {
        if (data != null)
        {
            Gizmos.DrawLine(data.ShuttleRouteStartDestination, data.ShuttleRouteEndDestination);
            Gizmos.DrawSphere(data.ShuttleRouteStartDestination, 0.25f);
            Gizmos.DrawSphere(data.ShuttleRouteEndDestination, 0.25f);
        }
    }

    public void ConfigureShuttleRoute(ShuttleRouteData _data)
    {
        data = _data;
        data.ShuttleRouteComplete += OnShuttleRouteComplete;
        data.ShuttleRouteCanceled += OnShuttleRouteCanceled;
        activeShuttleGameObject = Instantiate(shuttleGameObject, data.ShuttleRouteStartDestination, Quaternion.identity, transform);
        activeShuttleGameObject.GetComponent<ShuttleController>().ConfigureShuttle(data);
        lineRenderer.SetPosition(0, data.StartPlanetData.PlanetPosition);
        lineRenderer.SetPosition(1, data.EndPlanetData.PlanetPosition);
    }

    private void Update()
    {
        if (data != null)
        {
            data.Tick();   
        }
    }

    private void OnShuttleRouteCanceled()
    {
        GameManager.Instance.StructureManager.GlobalStructureData.GetStructureData(StructureNames.Shuttle).AddToAmount();
        Destroy(activeShuttleGameObject);
        Destroy(data);
        Destroy(gameObject);
    }

    private void OnShuttleRouteComplete()
    {
        // This is bad code btw. 
        if (data.EndPlanetData.PlanetName.Contains("RatKing"))
        {
            GameManager.Instance.StateManager.AddTime(GameManager.Instance.ResourceManager.GetResourceData(data.ResourceToShipName).TimePreventDoomsday * data.Amount);
        }
        GameManager.Instance.StructureManager.GlobalStructureData.GetStructureData(StructureNames.Shuttle).AddToAmount();
        Destroy(activeShuttleGameObject);
        Destroy(data);
        Destroy(gameObject);
    }
}
