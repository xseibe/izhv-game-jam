using UnityEngine;

public class FallOver : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] Transform TorquePoint;

    private bool fallen = false;

    // First Domino should just fall
    public void Interact()
    {
        GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * -15f, TorquePoint.position, ForceMode.Acceleration);
        fallen = true;
    }

    public bool InteractionAllowed()
    {
        return !fallen;
    }
}
