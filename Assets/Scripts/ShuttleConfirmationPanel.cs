using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShuttleConfirmationPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deliveryPanelTitle;
    [SerializeField] private TMP_Dropdown resourceDropDown;
    [SerializeField] private Slider amountSlider;
    [SerializeField] private TextMeshProUGUI amountTMP;
    [SerializeField] private Button letsPlayButton;
    private int amountToShip = 25;
    private Action<ShuttleRouteData> OnLetsPlayButtonPressedListener;
    private PlanetData startPlanet;
    private PlanetData endPlanet;
    
    
    public void ConfigureShuttleConfirmationPanel(PlanetData _startPlanet, PlanetData _endPlanet, Action<ShuttleRouteData> pleaseInvokeThis)
    {
        startPlanet = _startPlanet;
        endPlanet = _endPlanet;
        deliveryPanelTitle.text = $"Deliver from {startPlanet.PlanetName} to {endPlanet.PlanetName}";
        resourceDropDown.options.Clear();

        foreach (StaticResourceData staticResourceData in startPlanet.PlanetResources)
        {
            resourceDropDown.options.Add(new TMP_Dropdown.OptionData(staticResourceData.ResourceName, staticResourceData.ResourceSprite));
        }
        
        amountToShip = 25;
        amountSlider.value = 1.0f;
        amountTMP.text = amountToShip.ToString();
        OnLetsPlayButtonPressedListener = pleaseInvokeThis;
        letsPlayButton.onClick.AddListener(OnLetsPlayButtonPressed);   
    }

    public void Cleanup()
    {
        startPlanet = null;
        endPlanet = null;
        OnLetsPlayButtonPressedListener = null;
        letsPlayButton.onClick.RemoveListener(OnLetsPlayButtonPressed);
    }

    void Update()
    {
        amountToShip = (int) (amountSlider.value * 24.0f) + 1;
        amountTMP.text = amountToShip.ToString();
    }

    private void OnLetsPlayButtonPressed()
    {
        ShuttleRouteData newShuttleRouteData = ScriptableObject.CreateInstance<ShuttleRouteData>();
        newShuttleRouteData.PopulateShuttleRouteData(deliveryPanelTitle.text, startPlanet, endPlanet, startPlanet.PlanetResources[resourceDropDown.value].ResourceName, amountToShip);
        OnLetsPlayButtonPressedListener.Invoke(newShuttleRouteData);
    }
}
