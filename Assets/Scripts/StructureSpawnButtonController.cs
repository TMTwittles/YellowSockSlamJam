using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StructureSpawnButtonController : MonoBehaviour
{
    [SerializeField] private StructureSpawnButtonUIController uiController;
    [SerializeField] private Button button;
    private StructureSpawnButtonManager spawnButtonManager;
    private GameObject spawnedStructureGameObject;
    private bool spawnStructure = false;
    private StructureData structureData;
    private bool userIsPlacingSomething = false;

    public void SetButtonInteractivity(bool buttonInteractivity)
    {
        button.interactable = buttonInteractivity;
    }
    
    public void ConfigureStructureSpawnButton(StructureData _structureData, StructureSpawnButtonManager _spawnButtonManager)
    {
        spawnButtonManager = _spawnButtonManager;
        structureData = _structureData;
        button.interactable = structureData.StartingAmount > 0;
        button.onClick.AddListener(OnClick);
        uiController.ConfigureStructureUIController(structureData);
        GameManager.Instance.UserPlacingShuttle += OnUserPlacingShuttle;
        GameManager.Instance.UserPlacingStructure += OnUserPlacingStructure;
    }

    private void OnDestroy()
    {
        GameManager.Instance.UserPlacingShuttle -= OnUserPlacingShuttle;
        GameManager.Instance.UserPlacingStructure -= OnUserPlacingStructure;
    }

    private void OnUserPlacingStructure(bool placingStructure)
    {
        if (placingStructure)
        {
            userIsPlacingSomething = true;
        }
        else
        {
            userIsPlacingSomething = false;
        }
    }

    private void OnUserPlacingShuttle(bool placingShuttle)
    {
        if (placingShuttle)
        {
            userIsPlacingSomething = true;
        }
        else
        {
            userIsPlacingSomething = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        button.interactable = userIsPlacingSomething == false && structureData.Amount > 0;
        
        if (spawnStructure)
        {
            if (Input.GetButton("Fire2"))
            {
                GameManager.Instance.InvokeUserPlacingStructure(false);
                spawnStructure = false;
                Destroy(spawnedStructureGameObject);
                spawnButtonManager.SetSpawnButtonInteractivity(true);
            }
            else
            {
                Vector3 screenPosition = Input.mousePosition;
                Ray screenRay = Camera.main.ScreenPointToRay(screenPosition);
                RaycastHit hit;
                bool hitPlanet = Physics.Raycast(screenRay, out hit);
                if (hitPlanet && hit.collider.transform.parent.gameObject.CompareTag("Planet"))
                {
                    if (structureData.StructureName != StructureNames.Shuttle &&
                        hit.collider.GetComponentInParent<PlanetController>().HasStructure() == false)
                    {
                        Vector3 newStructurePosition = hit.collider.gameObject.transform.position +
                                                       new Vector3(0.0f, 9.0f, 0.0f);
                        spawnedStructureGameObject.transform.position = newStructurePosition;
                        
                        if (Input.GetButton("Fire1"))
                        {
                            hit.collider.transform.GetComponentInParent<PlanetController>().AddStructure(structureData);
                            GameObject spawnedStructure = Instantiate(structureData.StructureGameObject, newStructurePosition, Quaternion.identity,
                                hit.collider.transform.parent.GetChild(1));
                            spawnedStructure.transform.localPosition = new Vector3(0.0f, 0.02f, 0.0f);
                            spawnedStructure.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                            spawnedStructure.GetComponent<PlanetRotator>().enabled = false;
                            spawnStructure = false;
                            Destroy(spawnedStructureGameObject);
                            structureData.DecreaseAmount();
                            GameManager.Instance.InvokeUserPlacingStructure(false);
                            spawnButtonManager.SetSpawnButtonInteractivity(true);   
                        }
                    }
                    else 
                    {
                        Vector3 newStructurePosition = hit.collider.gameObject.transform.position +
                                                       new Vector3(0.0f, 10, 0.0f);
                        spawnedStructureGameObject.transform.position = newStructurePosition;
                        
                        if (Input.GetButton("Fire1"))
                        {
                            GameManager.Instance.InvokeUserPlacingStructure(false);
                            spawnButtonManager.ShuttleRouteCreator.ConfigureShuttleController(hit.collider.gameObject.GetComponentInParent<PlanetController>().GetPlanetData());
                            spawnButtonManager.ShuttleRouteCreator.OnShuttleButtonPressed();
                            spawnStructure = false;
                            Destroy(spawnedStructureGameObject);
                            structureData.DecreaseAmount();
                            spawnButtonManager.SetSpawnButtonInteractivity(true);
                        }
                    }
                }
                else
                {
                    screenPosition.z = Camera.main.nearClipPlane + 1;                                                           
                    screenPosition = Camera.main.ScreenToWorldPoint(screenPosition);                                            
                    Vector3 finalPosition = screenPosition + (screenRay.direction * (Camera.main.transform.position.y + 25.0f));
                    spawnedStructureGameObject.transform.position = finalPosition;                                              
                }
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

    private void OnClick()
    {
        if (userIsPlacingSomething == false && spawnStructure == false && structureData.Amount > 0)
        {
            GameManager.Instance.InvokeUserPlacingStructure(true);
            spawnButtonManager.SetSpawnButtonInteractivity(false);
            spawnedStructureGameObject = Instantiate(structureData.StructureGameObject, Input.mousePosition, Quaternion.identity);
            spawnStructure = true;   
        }
    }
}
