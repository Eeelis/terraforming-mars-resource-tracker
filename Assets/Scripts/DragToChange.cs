using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DragToChange : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private ValueField valueField;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TMP_Text indicator;

    private Vector2 mousePos;
    private float change;
    private bool dragging;
    private float initialValue;
    private int previousChange;
    private bool increasing;

    private float lastPlayBackTime;
    private float maxInterval = 0.045f;


    public void OnPointerDown(PointerEventData data)
    {
        initialValue = valueField.GetValue();
        valueField.HideText();
        indicator.transform.localScale = Vector3.zero;
        indicator.enabled = true;
        LeanTween.scale(indicator.gameObject, Vector3.one, 0.2f).setEaseOutBack();
        dragging = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (dragging)
        {
            dragging = false;
        }

        LeanTween.scale(indicator.gameObject, Vector3.zero, 0.2f).setEaseInBack().setOnComplete(UpdateValue);
        previousChange = 0;
        AkSoundEngine.PostEvent("ResetPitch", gameObject);
    }

    private void Update()
    {
        if (dragging)
        {
            int unscaledChange = (int)initialValue + (int)transform.position.y - (int)Input.mousePosition.y;
            change = Mathf.Clamp((-(int)(unscaledChange / 20)), -initialValue, valueField.maxValue);

            if (change != previousChange)
            {
                if (Time.time - lastPlayBackTime >= maxInterval)
                {
                    if (change > 0)
                    {   
                        previousChange = (int)change;
                        AkSoundEngine.PostEvent("Increment", gameObject);
                        AkSoundEngine.PostEvent("IncreasePitch", gameObject);
                        lastPlayBackTime = Time.time;
                    }
                    else if (change < 0)
                    {
                        previousChange = (int)change;
                        AkSoundEngine.PostEvent("Increment", gameObject);
                        AkSoundEngine.PostEvent("DecreasePitch", gameObject);
                        lastPlayBackTime = Time.time;
                    }
                }
            }

            if (change > 0)
            {
                indicator.text = initialValue + "+" + change.ToString();
            }
            else if (change < 0)
            {
                indicator.text = initialValue + change.ToString();
            }
            else if (change == 0)
            {
                indicator.text = initialValue + "-" + change.ToString();
            }
        }
    }

    private void UpdateValue()
    {
        valueField.UpdateValue(initialValue + change);

        StateManager.stateManager.StoreProgramState();

        if (change > 0)
        {
            EventScreenManager.eventScreenManager.AddLine("Increased the " + valueField.resourceType + " of " + valueField.resourceName + " by " + change + ".");
        }
        if (change < 0)
        {
            EventScreenManager.eventScreenManager.AddLine("Decreased the " + valueField.resourceType + " of " + valueField.resourceName + " by " + Mathf.Abs(change) + ".");
        }
        
        valueField.ShowText();
    }


}
