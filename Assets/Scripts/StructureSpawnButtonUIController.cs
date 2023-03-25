using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StructureSpawnButtonUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI structureNameTMP;
    [SerializeField] private TextMeshProUGUI structureAmountTMP;
    [SerializeField] private Image structureImage;
    private StructureData data;

    public void ConfigureStructureUIController(StructureData _data)
    {
        data = _data;
        structureImage.sprite = data.StructureIcon;
        structureAmountTMP.text = $"x{data.StartingAmount}";
    }

    public void Update()
    {
        structureAmountTMP.text = $"x{data.Amount}";
    }
}
