using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShuttleRouteData : ScriptableObject
{
    private string shuttleRouteName;
    public string ShuttleRouteName => shuttleRouteName;
    private PlanetData startPlanetData;
    public PlanetData StartPlanetData => startPlanetData;
    private PlanetData endPlanetData;
    
    public PlanetData EndPlanetData => endPlanetData;
    private string resourceToShipName;
    public string ResourceToShipName => resourceToShipName;
    private float amount;
    public float Amount => amount;
    private float shuttleTravelDuration;
    public float ShuttleTravelDuration => shuttleTravelDuration;
    private float shuttleTravelProgress = 0.0f;

    private Vector3 shuttleRouteStartDestination;
    public Vector3 ShuttleRouteStartDestination => shuttleRouteStartDestination;
    private Vector3 shuttleRouteEndDestination;
    public Vector3 ShuttleRouteEndDestination => shuttleRouteEndDestination;

    public Action ShuttleRouteComplete;
    public Action ShuttleRouteCanceled;

    public void PopulateShuttleRouteData(string _shuttleRouteName, PlanetData _startPlanetData, PlanetData _endPlanetData, string _resourceToShipName, float _amount)
    {
        shuttleRouteName = _shuttleRouteName;
        startPlanetData = _startPlanetData;
        endPlanetData = _endPlanetData;
        resourceToShipName = _resourceToShipName;
        amount = _amount;
        // the magic number of 1.75f is there to make shuttles start a bit further away from the planet to prevent clipping. - Arvie
        shuttleRouteStartDestination = startPlanetData.PlanetPosition + (endPlanetData.PlanetPosition - startPlanetData.PlanetPosition).normalized * startPlanetData.PlanetRadius * 1.75f;
        shuttleRouteEndDestination = endPlanetData.PlanetPosition + (startPlanetData.PlanetPosition - endPlanetData.PlanetPosition).normalized * endPlanetData.PlanetRadius * 1.75f;
        // Rhys, I removed this value. Soz. Need quicker debugging.
        shuttleTravelDuration = Vector3.Distance(shuttleRouteStartDestination, shuttleRouteEndDestination) * 0.5f;
    }

    public void Tick()
    {
        if (shuttleTravelProgress <= 0.0f)
        {
            if (startPlanetData != null)
            {
                startPlanetData.RemoveShippableResource(resourceToShipName, amount);   
            }
        }
        
        shuttleTravelProgress += Time.deltaTime * GameManager.Instance.TimeManager.TimeModifier;
        if (shuttleTravelProgress >= shuttleTravelDuration)
        {
            if (EndPlanetData.PlanetName.Contains("RatKing"))
            {
                GameManager.Instance.StateManager.AddTime(GameManager.Instance.ResourceManager.GetResourceData(resourceToShipName).TimePreventDoomsday * amount);
            }
            
            if (endPlanetData != null)
            {
                endPlanetData.AddShippableResource(resourceToShipName, amount, EndPlanetData.PlanetName.Contains("RatKing"));    
            }
            else
            {
                GameManager.Instance.ResourceManager.RemoveFromGlobalResourcesAmount(resourceToShipName, amount);
            }
            
            ShuttleRouteComplete.Invoke();
        }
    }

    public void CancelShuttleRoute()
    {
        if (startPlanetData != null)
        {
            startPlanetData.AddShippableResource(resourceToShipName, amount, false, false);    
        }
        else
        {
            GameManager.Instance.ResourceManager.RemoveFromGlobalResourcesAmount(resourceToShipName, amount);
        }
        
        ShuttleRouteCanceled.Invoke();
    }
    
    public float GetNormalizedTimeTillShuttleRouteComplete()
    {
        return (shuttleTravelProgress / shuttleTravelDuration);
    }
}
