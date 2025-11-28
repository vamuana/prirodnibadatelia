using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int items { get; private set; }

    public void AddItem(int amount = 1) => items += amount;
    public void Clear() => items = 0;
}
