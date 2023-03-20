using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShuttleRouteInfoUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI shuttleRouteName;
    [SerializeField] private Image timeRemainingImage;
    [SerializeField] private TextMeshProUGUI resourceAmount;
    [SerializeField] private Button cancelButton;
    private ShuttleRouteData data;

    public void ConfigureShuttleRouteInfoController(ShuttleRouteData shuttleRouteData)
    {
        shuttleRouteName.text = shuttleRouteData.ShuttleRouteName;
        resourceAmount.text = $"{shuttleRouteData.Amount} {shuttleRouteData.ResourceToShipName}";
        cancelButton.onClick.AddListener(OnCancelButtonPressed);
        data = shuttleRouteData;
    }

    void OnCancelButtonPressed()
    {
        data.CancelShuttleRoute();
    }

    void Update()
    {
        if (data != null)
        {
            timeRemainingImage.fillAmount = data.GetNormalizedTimeTillShuttleRouteComplete();   
        }
    }
}
