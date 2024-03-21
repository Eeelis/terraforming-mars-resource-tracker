using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Coffee.UIExtensions;

public class UIShineManager : MonoBehaviour
{
    [SerializeField] private Image blur;

    [SerializeField] private ShinyEffectForUGUI TFR;
    [SerializeField] private ShinyEffectForUGUI megaBucks;
    [SerializeField] private ShinyEffectForUGUI steel;
    [SerializeField] private ShinyEffectForUGUI titanium;
    [SerializeField] private ShinyEffectForUGUI plants;
    [SerializeField] private ShinyEffectForUGUI energy;
    [SerializeField] private ShinyEffectForUGUI heat;


    private Color blurred = new Color(180f, 180f, 180f, 1f);
    private Color transparent = new Color(180f, 180f, 180f, 0f);

    public static UIShineManager uiShineManager;

    private void Awake()
    {
        uiShineManager = this;

        TFR.location = 1;
        megaBucks.location = 1;
        steel.location = 1;
        titanium.location = 1;
        plants.location = 1;
        energy.location = 1;
        heat.location = 1;
    }

    

    public void UnBlurBackground(float time)
    {
        LeanTween.color(blur.rectTransform, transparent, time);
    }

    public void ShowMainMenu()
    {
        
    }

    public void Highlight()
    {
        StartCoroutine(HighlightIcons(1.5f, 0.7f));
    }

    public IEnumerator HighlightIcons(float time, float initialDelay)
    {
        yield return new WaitForSeconds(initialDelay);

        while (TFR.location != 0)
        {
            TFR.location = Mathf.MoveTowards(TFR.location, 0, time * Time.deltaTime);
            megaBucks.location = Mathf.MoveTowards(TFR.location, 0, time * Time.deltaTime);
            steel.location = Mathf.MoveTowards(TFR.location, 0, time * Time.deltaTime);
            titanium.location = Mathf.MoveTowards(TFR.location, 0, time * Time.deltaTime);
            plants.location = Mathf.MoveTowards(TFR.location, 0, time * Time.deltaTime);
            energy.location = Mathf.MoveTowards(TFR.location, 0, time * Time.deltaTime);
            heat.location = Mathf.MoveTowards(TFR.location, 0, time * Time.deltaTime);
            yield return null;
        }

        TFR.location = 1;
        megaBucks.location = 1;
        steel.location = 1;
        titanium.location = 1;
        plants.location = 1;
        energy.location = 1;
        heat.location = 1;
        
       
    }
}
