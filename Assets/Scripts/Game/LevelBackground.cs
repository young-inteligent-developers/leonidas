using UnityEngine;

public class LevelBackground : MonoBehaviour
{
    public float tweenTime;

    void Start()
    {
        LeanTween.color(gameObject, new Color(0.415f, 0, 0.392f, 1), tweenTime)
            .setLoopPingPong();
    }
}
