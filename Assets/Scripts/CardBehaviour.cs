using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBehaviour : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    private float XRotCalc;
    private float YRotCalc;

    private PointerEventData eventData;
    private List<RaycastResult> RR;

    [SerializeField] ParticleSystem sparkleEffect;
    private Vector2 previousMousePosition;
    private float mouseDistance;

    [SerializeField] CardReflection cardRef;

    void Start()
    {
        RR = new List<RaycastResult>();
    }
    void Update()
    {
        YRotCalc = (0.5f - MouseBehaviour.Singleton.mouseXPositionPercent) * 50;
        XRotCalc = (0.5f - MouseBehaviour.Singleton.mouseYPositionPercent) * 50;

        transform.rotation = Quaternion.Euler(XRotCalc, -YRotCalc, 0); //inverted Y
        sparkleEffect.transform.rotation = transform.rotation;
        //MouseHoverSparkle();
        if (sparkleEffect!=null)
        {
            //MouseMoveSparkle();
        }
    }

    //check this code later
    public void MouseHoverSparkle()
    {
        eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        EventSystem.current.RaycastAll(eventData, RR);
        if (RR.Count>0)
        {
            sparkleEffect.gameObject.SetActive(true);
            sparkleEffect.transform.rotation = transform.rotation;
            mouseDistance += Vector2.Distance(Input.mousePosition, previousMousePosition);
            if (mouseDistance > 100)
            {
                mouseDistance = 0;
                sparkleEffect.Play();
            }
            previousMousePosition = Input.mousePosition;
        }
        /*
        if (RR.Count>0)
        {
            foreach (RaycastResult result in RR)
            {
                Debug.Log(result.gameObject.name);
            }
        }
        */
    }


    public void MouseMoveSparkle()
    {
        sparkleEffect.transform.rotation = transform.rotation;
        mouseDistance += Vector2.Distance(Input.mousePosition, previousMousePosition);
        if (mouseDistance > 100)
        {
            mouseDistance = 0;
            sparkleEffect.Play();
        }
        previousMousePosition = Input.mousePosition;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //transform.localScale = Vector3.one * 1.25f;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        sparkleEffect.transform.position = transform.position;
        sparkleEffect.Play();
        cardRef.CardHightlight();
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        //transform.localScale = Vector3.one;
        //sparkleEffect.gameObject.SetActive(false);
    }

}
