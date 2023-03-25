using UnityEngine;

public class ShuttleRouteCreator : MonoBehaviour
{
    [SerializeField] private GameObject shuttleRouteGameObject;
    [SerializeField] private LineRenderer lineRenderer;
    private InfoCanvasController infoCanvasController;
    private bool userMakingShuttleRoute;
    private PlanetData data;
    private PlanetData endPlanetData;
    private void OnDrawGizmos()
    {
        if (userMakingShuttleRoute)
        {
            Vector3 screenPosition = Input.mousePosition;
            screenPosition.z = Camera.main.nearClipPlane + 1;
            Vector3 mousePositionNotClamped = Camera.main.ScreenToWorldPoint(screenPosition);
            Gizmos.DrawLine(data.PlanetPosition, mousePositionNotClamped);
        }
    }

    public void ConfigureShuttleController(PlanetData _data)
    {
        //infoCanvasController = GetComponent<InfoCanvasController>();
        data = _data;
    }

    public void OnShuttleButtonPressed()
    {
        GameManager.Instance.InvokeUserPlacingShuttle(true);
        lineRenderer.enabled = true;
        userMakingShuttleRoute = true;
    }

    private void ClearLineRenderer()
    {
        lineRenderer.enabled = false;
    }

    private void SetLineRenderer(bool useMousePosition, Vector3 endPosition)
    {
        if (useMousePosition)
        {
            Vector3 screenPosition = Input.mousePosition;
            Ray screenRay = Camera.main.ScreenPointToRay(screenPosition);
            screenPosition.z = Camera.main.nearClipPlane + 1;                                                           
            screenPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            endPosition = screenPosition + (screenRay.direction * (Camera.main.transform.position.y + 25.0f));
        }
        lineRenderer.SetPosition(0, data.PlanetPosition);
        lineRenderer.SetPosition(1, endPosition);
    }

    private void Update()
    {
        if (userMakingShuttleRoute)
        {
            SetLineRenderer(true, Vector3.zero);
            if (Input.GetButton("Fire1"))
            {
                Vector3 screenPosition = Input.mousePosition;
                Ray hitPlanetRay = Camera.main.ScreenPointToRay(screenPosition);
                RaycastHit hit;
                bool hitPlanet = Physics.Raycast(hitPlanetRay, out hit);
                if (hitPlanet)
                {
                    // Ensure we are hitting a planet and not the same planet this shuttle is launching from.
                    if ((hit.collider.gameObject.GetComponentInParent<PlanetController>()) && hit.collider.gameObject.transform.position != data.PlanetPosition)
                    {
                        SetLineRenderer(false, hit.point);
                        endPlanetData = hit.collider.gameObject.GetComponentInParent<PlanetController>().GetPlanetData();
                        DisplayShuttleRouteConfirmationPanel();
                        userMakingShuttleRoute = false;
                    }
                    else if (hit.collider.gameObject.GetComponentInParent<RatKingController>())
                    {
                        SetLineRenderer(false, hit.point);
                        endPlanetData = hit.collider.gameObject.GetComponentInParent<RatKingController>().GetRatKingData();
                        DisplayShuttleRouteConfirmationPanel();
                        userMakingShuttleRoute = false;
                    }
                }
            }
            
            if (Input.GetButton("Fire2"))
            {
                ClearLineRenderer();
                GameManager.Instance.InvokeUserPlacingShuttle(false);
                GameManager.Instance.StructureManager.GlobalStructureData.GetStructureData(StructureNames.Shuttle).AddToAmount();
                userMakingShuttleRoute = false;
            }
        }
    }

    private void DisplayShuttleRouteConfirmationPanel()
    {
        ShuttleConfirmationPanel shuttleConfirmationPanel = GameManager.Instance.UIManager.ShuttleConfirmationPanelGameObject.GetComponent<ShuttleConfirmationPanel>();
        shuttleConfirmationPanel.ConfigureShuttleConfirmationPanel(data, endPlanetData, OnShuttleConfirmationConfirmed);
        GameManager.Instance.UIManager.ShuttleConfirmationPanelGameObject.SetActive(true);
    }

    private void OnShuttleConfirmationConfirmed(ShuttleRouteData shuttleRouteData)
    {
        ClearLineRenderer();
        GameManager.Instance.InvokeUserPlacingShuttle(false);
        GameManager.Instance.UIManager.ShuttleConfirmationPanelGameObject.GetComponent<ShuttleConfirmationPanel>().Cleanup();
        GameManager.Instance.UIManager.ShuttleConfirmationPanelGameObject.SetActive(false);
        GameObject newShuttleRouteGameObject = Instantiate(shuttleRouteGameObject);
        newShuttleRouteGameObject.GetComponentInChildren<ShuttleRouteController>().ConfigureShuttleRoute(shuttleRouteData);
    }
}
