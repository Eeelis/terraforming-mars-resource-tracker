using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ProductionPhaseSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject bar;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Color highlightedColor;
    [SerializeField] private Color defaultColor;
    [SerializeField] private float maxXpos;
    [SerializeField] private float minXpos;

    private Image handleImage;
    private Image barImage;
    private Vector2 startingPos;
    private Vector2 barStartingPos;
    private Vector2 pos;
    private bool dragging;


    private void Start()
    {
        handleImage = GetComponent<Image>();
        barImage = bar.GetComponent<Image>();
        handleImage.color = defaultColor;
        barImage.color = defaultColor;
        startingPos = transform.position;
        barStartingPos = bar.transform.position;
    }
 
    public void OnPointerDown(PointerEventData data)
    {
        dragging = true;
        handleImage.color = highlightedColor;
    }
 
    public void OnPointerUp(PointerEventData data)
    {
        if (dragging)
        {
            dragging = false;
            transform.position = startingPos;
            handleImage.color = defaultColor;
        }
    }
 
    private void Update()
    {
        if (dragging)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);

            if (pos.x >= maxXpos)
            {
                pos.x = maxXpos;
                barImage.color = highlightedColor;
                HideBar();
                dragging = false;
            }

            if (pos.x < minXpos)
            {
                pos.x = minXpos; 
            }

            transform.position = new Vector2(canvas.transform.TransformPoint(pos).x, startingPos.y);
        }
    }

    private void HideBar()
    {
        LeanTween.moveY(bar, -100, 0.45f).setEase(LeanTweenType.easeInBack).setOnComplete(RunProductionPhase);
    }

    public void ShowBar()
    {
        handleImage.color = defaultColor;
        barImage.color = defaultColor;
        transform.position = new Vector2(startingPos.x, transform.position.y);
        LeanTween.moveY(bar, barStartingPos.y, 0.45f).setEase(LeanTweenType.easeOutCirc);
    }

    private void RunProductionPhase()
    {
        // Some delay added to space out the animations
        StartCoroutine(RunProductionPhaseCoroutine());
    }

    IEnumerator RunProductionPhaseCoroutine()
    {
        yield return new WaitForSeconds(0.75f);
        ProductionPhaseManager.productionPhaseManager.RunProductionPhase();
    }
}
