using System;
using System.Collections.Generic;
using UnityEngine;

public class StructureSpawnButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject structureSpawnButton;
    [SerializeField] private Transform structureButtonListTransform;
    private List<StructureSpawnButtonController> spawnButtons;
    
    void Start()
    {
        OnGameConfigured();
    }

    private void OnDestroy()
    {
        //GameManager.Instance.GameConfigured -= OnGameConfigured;
    }

    void OnGameConfigured()
    {
        spawnButtons = new List<StructureSpawnButtonController>();
        foreach (StructureData structureData in GameManager.Instance.StructureManager.GetAllStructures())
        {
            GameObject structureButtonSpawnerGameObject = Instantiate(structureSpawnButton, structureButtonListTransform);
            structureButtonSpawnerGameObject.GetComponent<StructureSpawnButtonController>().ConfigureStructureSpawnButton(structureData, this);
            spawnButtons.Add(structureButtonSpawnerGameObject.GetComponent<StructureSpawnButtonController>());
        }
    }

    public void SetSpawnButtonInteractivity(bool interactive)
    {
        foreach (StructureSpawnButtonController spawnButtonController in spawnButtons)
        {
            spawnButtonController.SetButtonInteractivity(interactive);
        }
    }
}
