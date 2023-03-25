using UnityEngine;

public class RatKingController : MonoBehaviour
{
    [SerializeField] private RatKingData data;
    [SerializeField] private RatKingUIInfoController uiController;

    public void ConfigureRatKing(RatKingData _data)
    {
        data = _data;
        uiController.ConfigureRatKingInfoUIController(data);
        
    }

    public RatKingData GetRatKingData()
    {
        return data;
    }
}
