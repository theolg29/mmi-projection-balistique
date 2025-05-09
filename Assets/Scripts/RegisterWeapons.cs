using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RegisterWeapons : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 30, 0);
    private Rigidbody rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private bool hasBeenGrabbed = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
    }

    void Update()
    {
        if (!hasBeenGrabbed)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Grabbed");
        if (rb != null)
        {
            rb.useGravity = true;
        }
        hasBeenGrabbed = true;
    }
}
