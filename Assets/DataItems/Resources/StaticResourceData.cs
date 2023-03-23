using UnityEngine;

[CreateAssetMenu(menuName = "Create ResourceData", fileName = "ResourceData", order = 0)]
public class StaticResourceData : ScriptableObject
{
    [SerializeField] private string resourceName;
    public string ResourceName => resourceName;
    [SerializeField] private Sprite resourceSprite;
    public Sprite ResourceSprite => resourceSprite;
    [SerializeField] private float timeSecondsGainResource;
    public float TimeSecondsGainResource => timeSecondsGainResource;
    [SerializeField] private float startingResourceAmount;
    public float StartingResourceAmount => startingResourceAmount;
    [SerializeField] private float standardResourceAmountDrain;
    public float StandardResourceDrain => standardResourceAmountDrain;
    [SerializeField] private float resourceAmountGain;
    public float ResourceAmountGain => resourceAmountGain;
}
