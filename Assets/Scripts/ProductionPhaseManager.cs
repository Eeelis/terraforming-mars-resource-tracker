using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProductionPhaseManager : MonoBehaviour
{
    public static ProductionPhaseManager productionPhaseManager;

    [SerializeField] private ValueField TFPAmount;

    [SerializeField] private ValueField megaBucksAmount;
    [SerializeField] private ValueField megaBucksProduction;
    
    [SerializeField] private ValueField steelAmount;
    [SerializeField] private ValueField steelProduction;

    [SerializeField] private ValueField titaniumAmount;
    [SerializeField] private ValueField titaniumProduction;
    
    [SerializeField] private ValueField plantsAmount;
    [SerializeField] private ValueField plantsProduction;
    
    [SerializeField] private ValueField energyAmount;
    [SerializeField] private ValueField energyProduction;

    [SerializeField] private ValueField heatAmount;
    [SerializeField] private ValueField heatProduction;

    [SerializeField] private ProductionPhaseSlider productionPhaseSlider;

    private int step = 0;
    private int generation = 1;
    

    private void Awake()
    {
        productionPhaseManager = this;
    }

    public void RunProductionPhase()
    {
        StateManager.stateManager.StoreProgramState();
        EventScreenManager.eventScreenManager.AddLine("Production phase. Generation " + generation + " started.");
        generation++;
        ConvertEnergyToHeat();
    }

    private void ConvertEnergyToHeat()
    {
        AkSoundEngine.PostEvent("ConvertEnergyToHeat", gameObject);
        heatAmount.AddToValue(energyAmount.GetValue(), 0.5f, true);
        energyAmount.SetValue(0, 0.5f, false);
    }

    public void Proceed()
    {
        step += 1;

        switch(step)
        {
            case 1:
                AkSoundEngine.PostEvent("StartMegaBucksProduction", gameObject);
                megaBucksAmount.AddToValue(megaBucksProduction.GetValue() + TFPAmount.GetValue(), 0.5f, true);
                break;
            case 2:
                if (steelProduction.GetValue() < 5)
                {
                    AkSoundEngine.PostEvent("StartSteelProduction_1", gameObject);
                }
                else if (steelProduction.GetValue() < 10)
                {
                    AkSoundEngine.PostEvent("StartSteelProduction_2", gameObject);
                }
                else if (steelProduction.GetValue() == 10)
                {
                    AkSoundEngine.PostEvent("StartSteelProduction_3", gameObject);
                }
                steelAmount.AddToValue(steelProduction.GetValue(), 0.5f, true);
                break;
            case 3:
                if (titaniumProduction.GetValue() < 5)
                {
                    AkSoundEngine.PostEvent("StartTitaniumProduction_1", gameObject);
                }
                else if (titaniumProduction.GetValue() < 10)
                {
                    AkSoundEngine.PostEvent("StartTitaniumProduction_2", gameObject);
                }
                else if (titaniumProduction.GetValue() == 10)
                {
                    AkSoundEngine.PostEvent("StartTitaniumProduction_3", gameObject);
                }
                titaniumAmount.AddToValue(titaniumProduction.GetValue(), 0.5f, true);
                break;
            case 4:
                AkSoundEngine.PostEvent("StartPlantProduction", gameObject);
                plantsAmount.AddToValue(plantsProduction.GetValue(), 0.5f, true);
                break;
            case 5:
                AkSoundEngine.PostEvent("StartEnergyProduction", gameObject);
                energyAmount.AddToValue(energyProduction.GetValue(), 0.5f, true);
                break;
            case 6:
                AkSoundEngine.PostEvent("StartHeatProduction", gameObject);
                heatAmount.AddToValue(heatProduction.GetValue(), 0.5f, true);
                break;
            case 7:
                productionPhaseSlider.ShowBar();
                step = 0;
                UIShineManager.uiShineManager.Highlight();
                break;
        }
    }

    public void PrepareToProceed()
    {
        AkSoundEngine.PostEvent("StopCurrentProduction", gameObject);
    }


}
