using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicResourceData : ScriptableObject
{
    private StaticResourceData data;
    public StaticResourceData Data => data;
    private float elapsedTime;
    public float ElapsedTime => elapsedTime;
    private float amount;
    public float Amount => amount;

    public void PopulateDynamicResourceData(StaticResourceData _data)
    {
        data = _data;
        elapsedTime = 0.0f;
        amount = data.StartingResourceAmount;
        GameManager.Instance.ResourceManager.AddToGlobalResourcesAmount(data.ResourceName, amount);
    }
    
    public void PopulateDynamicResourceData(StaticResourceData _data, float customAmount)
    {
        data = _data;
        elapsedTime = 0.0f;
        amount = customAmount;
        GameManager.Instance.ResourceManager.AddToGlobalResourcesAmount(data.ResourceName, amount);
    }

    public float NormalizedAmountResourceHasDepleted()
    {
        return (amount / data.StartingResourceAmount);
    }

    public float NormalizedTimeReachNewResource()
    {
        return (elapsedTime / data.TimeSecondsGainResource);
    }

    public void TickNaturalResource(float numHumans)
    {
        elapsedTime += Time.deltaTime * GameManager.Instance.TimeManager.TimeModifier;
        if (elapsedTime > data.TimeSecondsGainResource)
        {
            elapsedTime = 0.0f;
            amount -= data.StandardResourceDrain * (numHumans * 0.1f);
            GameManager.Instance.ResourceManager.AddToGlobalResourcesAmount(data.ResourceName, data.ResourceAmountGain);
        }
    }

    public void AddCustomAmount(float customAmount)
    {
        amount += customAmount;
        GameManager.Instance.ResourceManager.AddToGlobalResourcesAmount(data.ResourceName, customAmount);
    }

    public void RemoveCustomAmount(float customAmount, bool modifyGlobalResources)
    {
        amount -= customAmount;
    }

    public void SetCustomAmount(float customAmount)
    {
        amount = customAmount;
    }
}
