using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch t = Input.GetTouch(0);
        if (t.phase == TouchPhase.Ended)
        {
            RaycastHit2D hit = Physics2D.Raycast(t.position, Vector2.up, 0.01f);
            if (!hit) return;

            if (hit.transform.tag == "Field")
            {
                hit.transform.GetComponent<Field>().HandleClick();
            }
        }
    }
}
