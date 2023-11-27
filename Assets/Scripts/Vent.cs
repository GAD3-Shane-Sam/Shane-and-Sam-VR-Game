using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Vent : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable interactable;

    private void Start()
    {
        // Get the Rigidbody component of the object
        rb = GetComponent<Rigidbody>();

        // Get the XRGrabInteractable component
        interactable = GetComponent<XRGrabInteractable>();

        // Check if the XRGrabInteractable component is not null
        if (interactable != null)
        {
            // Subscribe to the selectEntered and selectExited events
            interactable.selectEntered.AddListener(OnGrab);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // When the object is grabbed, set kinematic to false
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }
}
