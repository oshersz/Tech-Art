using UnityEngine;
using UnityEngine.EventSystems;

public class PackBehaviour : MonoBehaviour, IPointerClickHandler
{
    private float XRotCalc;
    private float YRotCalc;
    private float XPreviousMousePosition;
    private float YPreviousMousePosition;
    private bool followMouse;

    private Vector2 mousePos;

    [SerializeField] ParticleSystem particleFollowEffect;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (followMouse)
        {
            followMouse = false;
            transform.rotation = Quaternion.identity;
        }
        else
        {
            followMouse = true;
            particleFollowEffect.Play();

            XPreviousMousePosition = MouseBehaviour.Singleton.mouseXPositionPercent;
            YPreviousMousePosition = MouseBehaviour.Singleton.mouseYPositionPercent;
        }
    }

    void Update()
    {
        if (followMouse)
        {
            particleFollowEffect.transform.position = transform.position;
            particleFollowEffect.transform.rotation = transform.rotation;
            //code help from unity forum
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, Input.mousePosition, Camera.main, out mousePos);
            transform.position = transform.TransformPoint(mousePos);

            //transform.position = new Vector3(MouseBehaviour.Singleton.mouseXPositionPercent, MouseBehaviour.Singleton.mouseYPositionPercent, 0);
            YRotCalc += ((MouseBehaviour.Singleton.mouseXPositionPercent - XPreviousMousePosition) * 150);
            XRotCalc += ((MouseBehaviour.Singleton.mouseYPositionPercent - YPreviousMousePosition) * 150);

            transform.rotation = Quaternion.Euler(XRotCalc, -YRotCalc, 0); //inverted Y

            XPreviousMousePosition = MouseBehaviour.Singleton.mouseXPositionPercent;
            YPreviousMousePosition = MouseBehaviour.Singleton.mouseYPositionPercent;

            YRotCalc *= (1 - Time.deltaTime * 2f);
            XRotCalc *= (1 - Time.deltaTime * 2f);

            //transform.rotation = Quaternion.Euler(transform.eulerAngles * (1-Time.deltaTime*5));
        }

    }
}
