using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShuttleConfirmationPanel : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resourceDropDown;
    [SerializeField] private Slider amountSlider;
    [SerializeField] private TextMeshProUGUI amountTMP;
    [SerializeField] private Button letsPlayButton;
    [SerializeField] private Button cancelButton;
    private int amountToShip = 25;
    private Action<ShuttleRouteData> OnLetsPlayButtonPressedListener;
    private PlanetData startPlanet;
    private PlanetData endPlanet;
    
    
    public void ConfigureShuttleConfirmationPanel(PlanetData _startPlanet, PlanetData _endPlanet, Action<ShuttleRouteData> pleaseInvokeThis)
    {
        startPlanet = _startPlanet;
        endPlanet = _endPlanet;
        resourceDropDown.options.Clear();

        foreach (StaticResourceData staticResourceData in startPlanet.PlanetShippableResources)
        {
            resourceDropDown.options.Add(new TMP_Dropdown.OptionData(staticResourceData.ResourceName, staticResourceData.ResourceSprite));
        }
        
        amountToShip = 25;
        amountSlider.value = 1.0f;
        amountTMP.text = amountToShip.ToString();
        OnLetsPlayButtonPressedListener = pleaseInvokeThis;
        cancelButton.onClick.AddListener(OnCancelButtonPressed);
        letsPlayButton.onClick.AddListener(OnLetsPlayButtonPressed);   
    }

    public void Cleanup()
    {
        startPlanet = null;
        endPlanet = null;
        OnLetsPlayButtonPressedListener = null;
        letsPlayButton.onClick.RemoveListener(OnLetsPlayButtonPressed);
        cancelButton.onClick.RemoveListener(OnCancelButtonPressed);
    }

    void Update()
    {
        float shippingAmount = startPlanet.GetShippablePlanetResourceAmount(resourceDropDown.options[resourceDropDown.value].text);
        amountToShip = (int) (amountSlider.value * shippingAmount);
        amountTMP.text = $"x{amountToShip}";
    }

    private void OnLetsPlayButtonPressed()
    {
        ShuttleRouteData newShuttleRouteData = ScriptableObject.CreateInstance<ShuttleRouteData>();
        newShuttleRouteData.PopulateShuttleRouteData(null, startPlanet, endPlanet, startPlanet.PlanetShippableResources[resourceDropDown.value].ResourceName, amountToShip);
        OnLetsPlayButtonPressedListener.Invoke(newShuttleRouteData);
    }

    private void OnCancelButtonPressed()
    {
        GameManager.Instance.InvokeUserPlacingShuttle(false);
        GameManager.Instance.StructureManager.GlobalStructureData.GetStructureData(StructureNames.Shuttle).AddToAmount();
        Cleanup();
        gameObject.SetActive(false);
    }
}
