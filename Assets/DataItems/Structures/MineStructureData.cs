using UnityEngine;


[CreateAssetMenu(menuName = "Create MineStructureData", fileName = "MineStructureData", order = 0)]
public class MineStructureData : StructureData
{
    public override void Tick(PlanetData planetData)
    {
        if (planetData.PlanetNaturalResources.Count > 0.0f)
        {
            return;
        }
        
        elapsedTime += Time.deltaTime * GameManager.Instance.TimeManager.TimeModifier;

        if (elapsedTime > timeStructureGenerates)
        {
            foreach (StaticResourceData staticResourceData in planetData.PlanetNaturalResources)
            {
                planetData.AddShippableResource(staticResourceData.ResourceName, resourceGain);
                planetData.RemoveNaturalResource(staticResourceData.ResourceName, resourceGain);
            }
            elapsedTime = 0.0f;
        }
    }
}