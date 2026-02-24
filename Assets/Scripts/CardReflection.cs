using UnityEngine;
using UnityEngine.UI;

public class CardReflection : MonoBehaviour
{
    //implement later with DOTWEEN
    private Material refMaterial;
    private float brightness;
    private bool effectActive;
    void Start()
    {
        //refMaterial = GetComponent<Renderer>().material;
        refMaterial = GetComponent<Image>().material;
        refMaterial.SetFloat("_Brightness", 0);
    }

    void Update()
    {
        if (effectActive)
        {
            brightness *= (1 - (Time.deltaTime * 15)); //5
            refMaterial.SetFloat("_Brightness", brightness);
            if (brightness<0.01f)
            {
                effectActive = false;
                refMaterial.SetFloat("_Brightness", 0);
            }
        }
    }

    public void CardHightlight()
    {
        brightness = 15; //5
        refMaterial.SetFloat("_Brightness", brightness);
        effectActive = true;
    }
}
