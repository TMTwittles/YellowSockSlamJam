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

    public Action ShuttleRouteComplete;
    public Action ShuttleRouteCanceled;

    public void PopulateShuttleRouteData(string _shuttleRouteName, PlanetData _startPlanetData, PlanetData _endPlanetData, string _resourceToShipName, float _amount)
    {
        shuttleRouteName = _shuttleRouteName;
        startPlanetData = _startPlanetData;
        endPlanetData = _endPlanetData;
        resourceToShipName = _resourceToShipName;
        amount = _amount;
        shuttleTravelDuration = Vector3.Distance(startPlanetData.PlanetPosition, endPlanetData.PlanetPosition) * 0.1f * amount;
    }

    public void Tick()
    {
        if (shuttleTravelProgress <= 0.0f)
        {
            startPlanetData.RemoveResource(resourceToShipName, amount);
        }
        
        shuttleTravelProgress += Time.deltaTime * GameManager.Instance.TimeManager.TimeModifier;
        if (shuttleTravelProgress >= shuttleTravelDuration)
        {
            endPlanetData.AddResource(resourceToShipName, amount);
            ShuttleRouteComplete.Invoke();
        }
    }

    public void CancelShuttleRoute()
    {
        startPlanetData.AddResource(resourceToShipName, amount);
        ShuttleRouteCanceled.Invoke();
    }
    
    public float GetNormalizedTimeTillShuttleRouteComplete()
    {
        return (shuttleTravelProgress / shuttleTravelDuration);
    }
}
