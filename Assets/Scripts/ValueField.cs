using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValueField : MonoBehaviour
{
    public string resourceName;
    public string resourceType;

    [SerializeField] private TMP_Text text;
    [SerializeField] private int initialValue;
    [SerializeField] public int minValue;
    [SerializeField] public int maxValue;

    private Vector3 initialScale;
    private Vector2 initialPosition;
    private float currentValue;
    private float value;
    private int previousValue;

    private void Awake()
    {
        value = initialValue;
        text.text = value.ToString();
    }

    private void Start()
    {
        initialScale = transform.localScale;
        initialPosition = transform.position;
    }

    public void AddToValue(float newValue, float duration, bool proceed)
    {
        StartCoroutine(CountTo(value + newValue, duration, proceed));
    }

    public float GetValue()
    {
        return value;
    }

    public void SetValue(float newValue, float duration, bool proceed)
    {
        StartCoroutine(CountTo(newValue, duration, proceed));

    }

    public void Increment()
    {
        RestoreSizeAndPosition();

        

        if (value < maxValue)
        {
            AkSoundEngine.PostEvent("Increment", gameObject);
            value += 1;
            text.text = value.ToString();
            StateManager.stateManager.StoreProgramState();

            if (resourceName == "Terraform Rating")
            {
                EventScreenManager.eventScreenManager.AddLine("Increased Terraform Rating by 1.");
            }
            else 
            {
                EventScreenManager.eventScreenManager.AddLine("Increased the " + resourceType + " of " + resourceName + " by 1.");
            }
        }
        else 
        {
            AkSoundEngine.PostEvent("IllegalAction", gameObject);
            // Shake the valuefield to indicate illegal action
            LeanTween.moveX(gameObject, gameObject.transform.position.x + 5, 0.5f).setEase(LeanTween.punch);

        }
    }

    public void UpdateValue(float newValue)
    {
        value = newValue;
        text.text = value.ToString();
    }

    public void HideText()
    {
        text.enabled = false;
    }

    public void ShowText()
    {
        text.enabled = true;
    }

    IEnumerator CountTo(float targetValue, float duration, bool proceed)
    {
        var rate = Mathf.Abs(targetValue - value / duration);

        if (targetValue > maxValue) { targetValue = maxValue; }
        else if (targetValue < minValue) { targetValue = minValue; }

        while (value != targetValue)
        {
            value = Mathf.MoveTowards(value, targetValue, 20 * Time.deltaTime);
            text.text = ((int)value).ToString();

            if ((int)value != previousValue && targetValue >= value)
            {
                AkSoundEngine.PostEvent("Increment", gameObject);
                previousValue = (int)value;
            }

            yield return null;
        }


        LeanTween.scale(gameObject, new Vector2(1f, 1f), 0.5f).setEase(LeanTween.punch);

        if (proceed)
        {
            ProductionPhaseManager.productionPhaseManager.PrepareToProceed();
            yield return new WaitForSeconds(0.5f);
            ProductionPhaseManager.productionPhaseManager.Proceed();
        }
    }

    public void Decrement()
    {
        RestoreSizeAndPosition();

        if (value > minValue)
        {
            AkSoundEngine.PostEvent("Increment", gameObject);
            value -= 1;
            text.text = value.ToString();
            StateManager.stateManager.StoreProgramState();

            if (resourceName == "Terraform Rating")
            {
                EventScreenManager.eventScreenManager.AddLine("Decreased Terraform Rating by 1.");
            }
            else 
            {
                EventScreenManager.eventScreenManager.AddLine("Decreased the " + resourceType + " of " + resourceName + " by 1.");
            }
        }
        else 
        {
            AkSoundEngine.PostEvent("IllegalAction", gameObject);
            // Shake the valuefield to indicate illegal action
            LeanTween.moveX(gameObject, gameObject.transform.position.x + 5, 0.5f).setEase(LeanTween.punch);
        }
    }

    private void RestoreSizeAndPosition()
    {
        LeanTween.cancel(gameObject);
        transform.localScale = initialScale;
        transform.position = initialPosition;
    }
}
