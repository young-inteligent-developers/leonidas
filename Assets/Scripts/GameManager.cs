using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool isPlayerTurn    = true;
    [HideInInspector]
    public int turn             = 1;

    [Header("Properties")]
    public int recruitmentBonus;

    [Header("Objects")]
    public FieldManager fieldManager;
    public TextMeshProUGUI turnText;

    public void EndTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        SetTurn(turn + 1);

        if (isPlayerTurn)
        {
            fieldManager.IncreaseUnits(Field.Ownership.Player, recruitmentBonus);
        }
        else
        {
            fieldManager.IncreaseUnits(Field.Ownership.Enemy, recruitmentBonus);
        }
    }

    public void SetTurn(int v)
    {
        turn = v;
        turnText.text = v.ToString();
    }
}
