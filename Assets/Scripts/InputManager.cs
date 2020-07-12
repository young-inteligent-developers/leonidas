using UnityEngine;

public class InputManager : MonoBehaviour
{
    public FieldManager fieldManager;
    public AttackPanel attackPanel;

    int inputPhase = 1;

    public void CancelSelection()
    {
        inputPhase = 1;
        fieldManager.selectedField.OnDeselect();
        fieldManager.UnhighlightConnectedFields();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.up, 0.01f);
            if (!hit)
            {
                if (inputPhase == 2) CancelSelection();
                return;
            }

            if (hit.transform.tag == "Field")
            {
                Field f = hit.transform.GetComponent<Field>();

                if (inputPhase == 1 && f.ownership == Field.Ownership.Player)
                {
                    inputPhase++;
                    f.OnSelect();
                    fieldManager.HighlightConnectedFields(f.index);
                    attackPanel.Set(f.strength);
                }
                else if (inputPhase == 2)
                {
                    if (!f.highlighted) return;
                    
                    inputPhase++;
                    attackPanel.Open();
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
            if (!hit)
            {
                if (inputPhase == 2) CancelSelection();
                return;
            }

            if (hit.transform.tag == "Field")
            {
                Field f = hit.transform.GetComponent<Field>();

                if (inputPhase == 1 && f.ownership == Field.Ownership.Player)
                {
                    inputPhase++;
                    f.OnSelect();
                    fieldManager.HighlightConnectedFields(f.index);
                    attackPanel.Set(f.strength);
                }
                else if (inputPhase == 2)
                {
                    if (!f.highlighted) return;
                    
                    inputPhase++;
                    attackPanel.Open();
                }
            }
        }
#endif
    }

}
