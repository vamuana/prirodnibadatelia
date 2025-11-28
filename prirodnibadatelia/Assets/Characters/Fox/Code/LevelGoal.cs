using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    [Header("Požiadavka")]
    [SerializeField] int requiredItems = 1;
    [SerializeField] Transform goalMarker;  // napr. vlajočka/empty pri nore
    [SerializeField] float reachRadius = 0.6f;

    [Header("Refs")]
    [SerializeField] Transform player;          // fox@all
    [SerializeField] Inventory playerInventory; // Inventory na líške
    [SerializeField] FoxAnimatorBridge bridge;  // kvôli Win triggeru

    public bool CheckWin()
    {
        if (!player || !goalMarker || !playerInventory) return false;
        if (playerInventory.items < requiredItems) return false;

        float d = Vector3.Distance(new Vector3(player.position.x, 0, player.position.z),
                                   new Vector3(goalMarker.position.x, 0, goalMarker.position.z));
        if (d <= reachRadius)
        {
            bridge?.PlayWin();
            return true;
        }
        return false;
    }
}
