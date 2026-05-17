using UnityEngine;
using UnityEngine.UI;

public class DeckListA2B : MonoBehaviour
{
    [HideInInspector] public Vector3 Destination;
    [SerializeField] private float _flySpeed;
    [SerializeField] ParticleSystem particles;

    [SerializeField] Image background;
    [SerializeField] Image manaCrystal;

    private Material backgroundMaterial;
    private Material manaMaterial;
    private Color glow;
    private float glowStrength;

    private Vector3 _startingPosition;
    private float _timer;
    private float _easedTimer;

    private bool arrived;
    void Start()
    {
        _startingPosition = transform.position;
        _timer = Time.time;


        background.material = new Material(background.material);
        backgroundMaterial = background.material;

        manaCrystal.material = new Material(manaCrystal.material);
        manaMaterial = manaCrystal.material;

        glowStrength = 6;
        glow = new Color(0.625f, 0.625f, 1, 1) * glowStrength;

        backgroundMaterial.SetFloat("_Thickness", 0.02f);
        backgroundMaterial.SetColor("_Color", glow);
        manaMaterial.SetFloat("_Thickness", 0.02f);
        manaMaterial.SetColor("_Color", glow);

    }

    void Update()
    {
        _easedTimer = Mathf.SmoothStep(0, 1, (Time.time - _timer) * _flySpeed);
        transform.position = Vector3.Lerp(_startingPosition, Destination, _easedTimer);
        if (Vector3.Distance(transform.position, Destination) < 0.1f  && !arrived)
        {
            particles.transform.parent = null;
            particles.transform.localScale = Vector3.one;
            particles.Stop();
            Destroy(particles, 2);
            Destroy(gameObject,2);
            CardManager.sigleton.UpdateDeck();

            arrived = true;

        }
        if (arrived)
        {
            glowStrength *= (1 - (Time.deltaTime * 9));
            glow = new Color(0.625f, 0.625f, 1, 1) * glowStrength;
            backgroundMaterial.SetColor("_Color", glow);
            manaMaterial.SetColor("_Color", glow);
        }
    }
}
