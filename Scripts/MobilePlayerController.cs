using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobilePlayerController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image joystickBackgroundImage, joystickControllerImage;
    private Vector3 inputVector;


    private void Start()
    {
        joystickBackgroundImage = GetComponent<Image>();
        joystickControllerImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackgroundImage.rectTransform, eventData.position, eventData.pressEventCamera, out position))
        {
            position.x = (position.x / joystickBackgroundImage.rectTransform.sizeDelta.x);
            position.y = (position.y / joystickBackgroundImage.rectTransform.sizeDelta.y);

            inputVector = new Vector3(position.x * 2 + 1, 0, position.y * 2 - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystickControllerImage.rectTransform.anchoredPosition = new Vector3(
                inputVector.x * (joystickBackgroundImage.rectTransform.sizeDelta.x / 2),
                inputVector.z * (joystickBackgroundImage.rectTransform.sizeDelta.y / 2)
                );

        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector3.zero;
        joystickControllerImage.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.z != 0)
            return inputVector.z;
        else
            return Input.GetAxis("Vertical");
    }





}
