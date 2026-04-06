using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // The scale multiplier when the button is hovered over
    public float hoverScale = 1.2f;

    // Stores the original scale so we can return to it on exit
    private Vector3 originalScale;

    void Start()
    {
        // Save the button's original scale at the start
        originalScale = transform.localScale;
    }

    // Called automatically when the mouse enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * hoverScale;
    }

    // Called automatically when the mouse leaves the button
    public void OnPointerExit(PointerEventData eventData)
    {
        // Restore the button to its original scale
        transform.localScale = originalScale;
    }
}