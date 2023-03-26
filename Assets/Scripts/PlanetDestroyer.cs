using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDestroyer : MonoBehaviour
{
    [SerializeField] private PlanetController planetController;
    private float prevNormalizedTime = 0.0f;

    void Update()
    {
        if (planetController == null)
        {
            planetController = GetComponentInParent<PlanetController>();
        }

        float normalizedTimeTillResourceDepleted = 1.0f;
        if (planetController.GetPlanetData() != null)
        {
            normalizedTimeTillResourceDepleted =
                planetController.GetPlanetData().GetNormalizedTimeTillAnyResourceDepleted();    
        }
        normalizedTimeTillResourceDepleted = Mathf.Clamp(normalizedTimeTillResourceDepleted, 0.6f, 1.0f);
        //normalizedTimeTillResourceDepleted = 0.6f + (Mathf.Abs(normalizedTimeTillResourceDepleted - 0.6f) / 0.6f);
        
        /*if (normalizedTimeTillResourceDepleted <= 0)
        {
            planetController.Kill();
        }*/
        if (normalizedTimeTillResourceDepleted != prevNormalizedTime)
        {
            transform.localScale *= normalizedTimeTillResourceDepleted;   
        }
        prevNormalizedTime = normalizedTimeTillResourceDepleted;
    }
}
