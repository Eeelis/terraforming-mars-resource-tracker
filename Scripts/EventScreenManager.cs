using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventScreenManager : MonoBehaviour
{
    public static EventScreenManager eventScreenManager;

    [SerializeField] private GameObject eventScreenCanvas;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text animatedText;
    [SerializeField] private Image blur;
    
    private List<string> lines = new List<string>();
    private Color blurred = new Color(180f, 180f, 180f, 1f);
    private Color transparent = new Color(180f, 180f, 180f, 0f);
    private Color textColor = new Color(236, 236, 236, 1f);
    private Color transparentTextColor = new Color(236, 236, 236, 0f);
    private Vector2 initialTextPosition;
    private string events = "";

    
    private void Awake()
    {
        eventScreenManager = this;
    }

    private void Start()
    {
        initialTextPosition = text.transform.localPosition;
    }

    public void AddLine(string newText)
    {
        lines.Add(newText);
        lines.Reverse();

        events = "";

        foreach(string line in lines)
        {
            events += line + '\n';
        }

        lines.Reverse();

        text.text = events;
    }

    public void RemoveLine()
    {
        if (lines.Count == 0) { return; }

        int index = events.IndexOf('\n');

        if (index >= 0)
        {
            AnimateRemovedLine(events.Substring(0, index));
            text.text = '\n' + events.Substring(index + 1);
            events = events.Substring(index + 1);
        }

        lines.RemoveAt(lines.Count - 1);
    }

    public void ShowEventScreen()
    {
        if (eventScreenCanvas.activeInHierarchy)
        {
            LeanTween.color(blur.rectTransform, transparent, 0f).setOnComplete(HideEvents);
        }
        else 
        {
            LeanTween.color(blur.rectTransform, blurred, 0f).setOnComplete(ShowEvents);
        }
    }

    private void ShowEvents()
    {
        eventScreenCanvas.SetActive(true);
    }

    private void HideEvents()
    {
        eventScreenCanvas.SetActive(false);
    }

    private void AnimateRemovedLine(string line)
    {
        LeanTween.cancel(animatedText.rectTransform);
        LeanTween.cancel(text.rectTransform);

        animatedText.text = "";
        animatedText.transform.localPosition = initialTextPosition;
        animatedText.color = textColor;
        animatedText.text = line;

        animatedText.LeanAlphaText(0, 0.7f);
        LeanTween.moveY(animatedText.rectTransform, text.transform.localPosition.y + 400f, 1f);
        RemoveExtraNewLine();
    }

    private void RemoveExtraNewLine()
    {
        int index = text.text.IndexOf('\n');
        text.text = text.text.Substring(0, index) + text.text.Substring(index + 1);
        LeanTween.moveY(text.rectTransform, text.transform.localPosition.y + 10, 0.25f).setEase(LeanTweenType.punch);
    }
}
