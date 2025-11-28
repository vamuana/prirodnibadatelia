using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour
{
    [SerializeField] int amount = 1;
    [SerializeField] bool destroyOnPick = true;

    public bool TryPick(Inventory inv)
    {
        if (!inv) return false;
        inv.AddItem(amount);
        if (destroyOnPick) gameObject.SetActive(false);
        return true;
    }
}
