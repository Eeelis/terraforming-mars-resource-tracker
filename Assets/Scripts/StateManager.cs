using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{  
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

    private Stack<List<float>> stateStack = new Stack<List<float>>();

    public static StateManager stateManager;


    private void Awake()
    {
        stateManager = this;
    }

    private void Start()
    {
        StateManager.stateManager.StoreProgramState();
    }

    public void StoreProgramState()
    {
        List <float> valueFieldValues = new List<float>();

        valueFieldValues.Add(TFPAmount.GetValue());
        valueFieldValues.Add(megaBucksAmount.GetValue());
        valueFieldValues.Add(megaBucksProduction.GetValue());
        valueFieldValues.Add(steelAmount.GetValue());
        valueFieldValues.Add(steelProduction.GetValue());
        valueFieldValues.Add(titaniumAmount.GetValue());
        valueFieldValues.Add(titaniumProduction.GetValue());
        valueFieldValues.Add(plantsAmount.GetValue());
        valueFieldValues.Add(plantsProduction.GetValue());
        valueFieldValues.Add(energyAmount.GetValue());
        valueFieldValues.Add(energyProduction.GetValue());
        valueFieldValues.Add(heatAmount.GetValue());
        valueFieldValues.Add(heatProduction.GetValue());

        stateStack.Push(valueFieldValues);
    }

    public void Undo()
    {
        AkSoundEngine.PostEvent("IllegalAction", gameObject);
        if (stateStack.Count <= 1) { return; }

        stateStack.Pop();

        TFPAmount.UpdateValue(stateStack.Peek()[0]);
        megaBucksAmount.UpdateValue(stateStack.Peek()[1]);
        megaBucksProduction.UpdateValue(stateStack.Peek()[2]);
        steelAmount.UpdateValue(stateStack.Peek()[3]);
        steelProduction.UpdateValue(stateStack.Peek()[4]);
        titaniumAmount.UpdateValue(stateStack.Peek()[5]);
        titaniumProduction.UpdateValue(stateStack.Peek()[6]);
        plantsAmount.UpdateValue(stateStack.Peek()[7]);
        plantsProduction.UpdateValue(stateStack.Peek()[8]);
        energyAmount.UpdateValue(stateStack.Peek()[9]);
        energyProduction.UpdateValue(stateStack.Peek()[10]);
        heatAmount.UpdateValue(stateStack.Peek()[11]);
        heatProduction.UpdateValue(stateStack.Peek()[12]);
    }
}
