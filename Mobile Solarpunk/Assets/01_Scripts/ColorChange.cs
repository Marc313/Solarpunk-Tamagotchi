using UnityEngine;
using UnityEngine.EventSystems;

public class ColorChange : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<Camera>().backgroundColor = Random.ColorHSV();
    }
}
