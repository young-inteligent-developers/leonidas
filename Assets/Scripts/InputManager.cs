using UnityEngine;

public class InputManager : MonoBehaviour
{
    public FieldManager fieldManager;

    int inputPhase = 1;

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
                Field f = hit.transform.GetComponent<Field>();
                if (inputPhase == 1)
                {
                    inputPhase++;
                    fieldManager.HighlightConnectedFields(f.index);
                }
                else if (inputPhase == 2)
                {
                    if (!f.highlighted) return;

                    inputPhase++;
                    f.OnClick();
                }
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
