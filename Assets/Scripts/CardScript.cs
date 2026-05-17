using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public string cardName;
    public int cost;

    //private Animator anim;
    private ParticleSystem sparkle;
    private Image image;
    private Material spriteMaterial;

    private bool effectActive; //replace with DOTween
    private Color glow;
    private float glowVariance;

    void Start()
    {
        //anim = GetComponent<Animator>();
        sparkle = GetComponentInChildren<ParticleSystem>();

        image = GetComponentInChildren<Image>();
        image.material = new Material(image.material);
        spriteMaterial = image.material;

        spriteMaterial.SetFloat("_Thickness", 0);
        spriteMaterial.SetColor("_Color", Color.black);
        spriteMaterial.SetFloat("_Brightness", 0);
    }

    void Update()
    {
        if (effectActive)
        {
            glowVariance *= (1 - (Time.deltaTime * 9));
            glow = new Color(0.625f, 0.625f, 1, 1) * glowVariance;
            spriteMaterial.SetColor("_Color", glow);
            spriteMaterial.SetFloat("_Brightness", glowVariance / 6);
            if (glowVariance < 0.01f)
            {
                effectActive = false;
                spriteMaterial.SetFloat("_Thickness", 0);
                spriteMaterial.SetColor("_Color", Color.black);
                spriteMaterial.SetFloat("_Brightness", 0);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!effectActive)
        {
            spriteMaterial.SetFloat("_Thickness", 0.02f);
            spriteMaterial.SetColor("_Color", Color.white);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        sparkle.transform.position = transform.position;
        sparkle.Play();

        //dotween later
        effectActive = true;
        spriteMaterial.SetFloat("_Thickness", 0.02f);
        glow = new Color(0.625f, 0.625f, 1, 1); //(0.015f, 0.1f, 0.75f, 1)
        glowVariance = 6;
        glow = glow *glowVariance;
        spriteMaterial.SetColor("_Color",glow);
        spriteMaterial.SetFloat("_Brightness", glowVariance/6);

        CardManager.sigleton.AddToDeck(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!effectActive)
        {
            spriteMaterial.SetFloat("_Thickness", 0);
            spriteMaterial.SetColor("_Color", Color.black);
            spriteMaterial.SetFloat("_Brightness", 0);
        }
    }
}
