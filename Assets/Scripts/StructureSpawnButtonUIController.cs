using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StructureSpawnButtonUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI structureNameTMP;
    [SerializeField] private TextMeshProUGUI structureAmountTMP;
    private StructureData data;
    
    public void ConfigureStructureUIController(StructureData _data)
    {
        data = _data;
        structureNameTMP.text = data.StructureName;
        structureAmountTMP.text = GameManager.Instance.StructureManager.GetPlayerStructureCount(data.StructureName).ToString();
    }
}
