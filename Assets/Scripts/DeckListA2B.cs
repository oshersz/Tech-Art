using UnityEngine;

public class DeckListA2B : MonoBehaviour
{
    [HideInInspector] public Vector3 Destination;
    [SerializeField] private float _flySpeed;
    [SerializeField] ParticleSystem particles;
    private Vector3 _startingPosition;
    private float _timer;
    private float _easedTimer;
    void Start()
    {
        _startingPosition = transform.position;
        _timer = Time.time;
    }

    void Update()
    {
        _easedTimer = Mathf.SmoothStep(0, 1, (Time.time - _timer) * _flySpeed);
        transform.position = Vector3.Lerp(_startingPosition, Destination, _easedTimer);
        if (Vector3.Distance(transform.position, Destination) < 0.1f)
        {
            particles.transform.parent = null;
            particles.transform.localScale = Vector3.one;
            particles.Stop();
            Destroy(particles, 2);
            Destroy(gameObject);
            CardManager.sigleton.UpdateDeck();
        }
    }
}
