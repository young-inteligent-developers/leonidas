using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.up, 0.01f);
            if (!hit) return;

            if (hit.transform.tag == "Field")
            {
                hit.transform.GetComponent<Field>().OnClick();
            }
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount == 0) return;

        Touch t = Input.GetTouch(0);
        if (t.phase == TouchPhase.Ended)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(t.position);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.up, 0.01f);
            if (!hit) return;

            if (hit.transform.tag == "Field")
            {
                hit.transform.GetComponent<Field>().OnClick();
            }
        }
#endif
    }

}
