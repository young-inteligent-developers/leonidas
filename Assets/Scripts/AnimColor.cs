using UnityEngine;

public class AnimColor : MonoBehaviour
{
    private float duration = 1;
    private float time = 0;

    private SpriteRenderer sr;      // sprite renderer
    private Color cc;               // current color
    private Color dc;               // destination color

    public void Animate(float duration, Color destinationColor)
    {
        this.duration = duration;
        cc = sr.color;
        dc = destinationColor;

        enabled = true;
    }

    private void Start()
    {
        sr = transform.Find("Border").GetComponent<SpriteRenderer>();
        enabled = false;
    }

    void Update()
    {
        if (time > duration)
        {
            enabled = false;
            return;
        }

        sr.color = Color.Lerp(cc, dc, time / duration);
        time += Time.deltaTime;
    }
}
