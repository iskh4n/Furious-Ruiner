using UnityEngine;

public class ColorChangingLight : MonoBehaviour
{
    public float colorChangeSpeed = 1f;
    public Color[] colors;

    private Light lightComponent;
    private int currentIndex = 0;
    private int nextIndex = 1;
    private float t = 0f;

    private void Start()
    {
        lightComponent = GetComponent<Light>();
    }

    private void Update()
    {
        if (colors.Length < 2)
            return;

        t += Time.deltaTime * colorChangeSpeed;

        if (t >= 1f)
        {
            t = 0f;
            currentIndex = nextIndex;
            nextIndex = (nextIndex + 1) % colors.Length;
        }

        lightComponent.color = Color.Lerp(colors[currentIndex], colors[nextIndex], t);
    }
}
