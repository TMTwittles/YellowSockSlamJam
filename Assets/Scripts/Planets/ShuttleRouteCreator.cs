using System;

using UnityEngine;
using UnityEngine.UI;

public class ShuttleRouteCreator : MonoBehaviour
{
    [SerializeField] private GameObject shuttleRouteGameObject;
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
        userMakingShuttleRoute = true;
    }

    private void Update()
    {
        if (userMakingShuttleRoute)
        {
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
                        endPlanetData = hit.collider.gameObject.GetComponentInParent<PlanetController>().GetPlanetData();
                        DisplayShuttleRouteConfirmationPanel();
                        userMakingShuttleRoute = false;
                    }
                    else if (hit.collider.gameObject.GetComponentInParent<RatKingController>())
                    {
                        endPlanetData = hit.collider.gameObject.GetComponentInParent<RatKingController>().GetRatKingData();
                        DisplayShuttleRouteConfirmationPanel();
                        userMakingShuttleRoute = false;
                    }
                }
            }
            
            if (Input.GetButton("Fire2"))
            {
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
        GameManager.Instance.UIManager.ShuttleConfirmationPanelGameObject.GetComponent<ShuttleConfirmationPanel>().Cleanup();
        GameManager.Instance.UIManager.ShuttleConfirmationPanelGameObject.SetActive(false);
        GameObject newShuttleRouteGameObject = Instantiate(shuttleRouteGameObject);
        newShuttleRouteGameObject.GetComponentInChildren<ShuttleRouteController>().ConfigureShuttleRoute(shuttleRouteData);
    }
}
