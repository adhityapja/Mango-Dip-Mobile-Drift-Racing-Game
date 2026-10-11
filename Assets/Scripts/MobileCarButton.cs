using UnityEngine;
using UnityEngine.EventSystems;

public class MobileCarButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private PrometeoTouchInput touchInput;

    void Awake()
    {
        touchInput = GetComponent<PrometeoTouchInput>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        touchInput.ButtonDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        touchInput.ButtonUp();
    }
}