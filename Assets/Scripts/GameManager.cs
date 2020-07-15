using UnityEngine;

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

    public void EndTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        turn++;

        if (isPlayerTurn)
        {
            fieldManager.IncreaseUnits(Field.Ownership.Player, recruitmentBonus);
        }
        else
        {
            fieldManager.IncreaseUnits(Field.Ownership.Enemy, recruitmentBonus);
        }
    }
}
