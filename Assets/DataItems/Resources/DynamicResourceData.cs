using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicResourceData : ScriptableObject
{
    private StaticResourceData data;
    private float elapsedTime;
    public float ElapsedTime => elapsedTime;
    private float amount;
    public float Amount => amount;

    public void PopulateDynamicResourceData(StaticResourceData _data)
    {
        data = _data;
        elapsedTime = 0.0f;
        amount = data.StartingResourceAmount;
    }

    public float NormalizedTimeReachNewResource()
    {
        return (elapsedTime / data.TimeSecondsGainResource);
    }

    public void Tick()
    {
        elapsedTime += Time.deltaTime * GameManager.Instance.TimeManager.TimeModifier;
        if (elapsedTime > data.TimeSecondsGainResource)
        {
            elapsedTime = 0.0f;
            amount += data.ResourceAmountGain;
        }
    }
}
