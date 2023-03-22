using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class StructureSpawnButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject structureGameObject;
    private GameObject spawnedStructureGameObject;
    private Button button;
    private bool spawnStructure = false;
    
    private void OnDrawGizmos()
    {
        if (spawnStructure)
        {
            Vector3 screenPosition = Input.mousePosition;
            screenPosition.z = Camera.main.nearClipPlane + 1;
            Vector3 mousePositionNotClamped = Camera.main.ScreenToWorldPoint(screenPosition);
            Gizmos.DrawLine(Vector3.zero, mousePositionNotClamped);
        }
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnStructure)
        {
            if (Input.GetButton("Fire1"))
            {
                Vector3 screenPosition = Input.mousePosition;
                Ray screenRay = Camera.main.ScreenPointToRay(screenPosition);
                screenPosition.z = Camera.main.nearClipPlane + 1;
                screenPosition = Camera.main.ScreenToWorldPoint(screenPosition);
                Vector3 finalPosition = screenPosition + (screenRay.direction * (Camera.main.transform.position.y + 25.0f));
                //finalPosition.y = 0.0f;
                spawnedStructureGameObject.transform.position = finalPosition;    
            }
            else
            {
                spawnStructure = false;
                Destroy(spawnedStructureGameObject);
            }
            
            /*Ray hitPlanetRay = Camera.main.ScreenPointToRay(screenPosition);
            RaycastHit hit;
            bool hitPlanet = Physics.Raycast(hitPlanetRay, out hit);
            if (hitPlanet)
            {
                // Ensure we are hitting a planet and not the same planet this shuttle is launching from.
                if (hit.collider.gameObject.GetComponentInParent<PlanetController>() &&
                    hit.collider.gameObject.transform.position != data.PlanetPosition)
                {
                }
            }*/
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (spawnStructure == false)
        {
            spawnedStructureGameObject = Instantiate(structureGameObject, Input.mousePosition, Quaternion.identity);
            spawnStructure = true;   
        }
    }
}
