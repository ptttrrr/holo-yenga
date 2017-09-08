using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class Present : MonoBehaviour
{
    static int sphereCount;
    int sphereNumber;

    void Start()
    {
        this.gameObject.GetComponent<HandDraggable>().StartedDragging += OnStartedDragging;
        this.gameObject.GetComponent<HandDraggable>().StoppedDragging += OnStoppedDragging;

      
    }
    void OnStartedDragging()
    {
        // When we start dragging we clear out any existing world anchor for this
        // Xmas present.
        WorldAnchorManager.Instance.RemoveAnchor(this.gameObject);

        // We also add the RigidBody component so that (e.g.) you can't push
        // the present through the floor.
        var rigidBody = this.gameObject.AddComponent<Rigidbody>();

        // But we don't want it to fall on the floor on its own.
        rigidBody.useGravity = false;
    }
    void OnStoppedDragging()
    {
        // We take away the RigidBody because it doesn't play well with
        // Rigidbody.
        Destroy(this.gameObject.GetComponent<Rigidbody>());

        // Now remember where the present was.
        WorldAnchorManager.Instance.AttachAnchor(this.gameObject, this.sphereNumber.ToString());
    }
    void OnCollisionEnter(Collision collision)
    {
        // If we do get a collision then we (a bit rudley) just stop the
        // drag operation.
        this.gameObject.GetComponent<HandDraggable>().StopDragging();
    }
}