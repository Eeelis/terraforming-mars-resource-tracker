using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonAnimator : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject targetGameObject;
    [SerializeField] private TMP_Text targetText;


    private Color initialColor;
    private Color targetColor;
    private Vector2 scaledUp = new Vector2(1.1f, 1.1f);

    public void OnPointerClick(PointerEventData data)
    {
        LeanTween.cancel(targetGameObject);
        

        LeanTween.scale(targetGameObject, scaledUp, 0.4f).setEase(LeanTween.punch);

        if (targetText)
        {
            LeanTween.cancel(targetText.rectTransform);
            LeanTween.scale(targetText.rectTransform, scaledUp, 0.4f).setEase(LeanTween.punch);
        }
    }
}
