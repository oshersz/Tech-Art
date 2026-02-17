using UnityEngine;
using TMPro;

public class MouseBehaviour : MonoBehaviour
{
    public static MouseBehaviour Singleton { get; private set; }
    public float mouseXPositionPixels;
    public float mouseYPositionPixels;
    public float mouseXPositionPercent;
    public float mouseYPositionPercent;

    [SerializeField] TextMeshProUGUI mouseXPixeslText;
    [SerializeField] TextMeshProUGUI mouseYPixeslText;
    [SerializeField] TextMeshProUGUI mouseXPercentText;
    [SerializeField] TextMeshProUGUI mouseYPercentText;

    private void Awake()
    {
        Singleton = this;
    }

    void Update()
    {
        mouseXPositionPixels = Input.mousePosition.x;
        mouseYPositionPixels = Input.mousePosition.y;
        mouseXPositionPercent = (mouseXPositionPixels / Screen.width);
        mouseYPositionPercent = (mouseYPositionPixels / Screen.height);


        mouseXPixeslText.text = "Mouse X Pixels: " + mouseXPositionPixels;
        mouseYPixeslText.text = "Mouse Y Pixels: " + mouseYPositionPixels;
        mouseXPercentText.text = "Mouse X Percent: " + mouseXPositionPercent;
        mouseYPercentText.text = "Mouse Y Percent: " + mouseYPositionPercent;
    }
}
