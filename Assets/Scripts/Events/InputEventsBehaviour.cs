using UnityEngine;
using UnityEngine.InputSystem;
using Events.InputEvents;

public class InputEventsBehaviour : MonoBehaviour
{
    public void OnMouseMove(InputAction.CallbackContext context)
    {
        EventAggregator.Get<MouseMovedEvent>().Publish(context.ReadValue<Vector2>());
    }
    public void OnMoveCamera(InputAction.CallbackContext context)
    {
        EventAggregator.Get<CameraMovedEvent>().Publish(context.ReadValue<Vector2>());
    }
    public void OnZoomCamera(InputAction.CallbackContext context)
    {
        EventAggregator.Get<CameraZoomedEvent>().Publish(-context.ReadValue<Vector2>().y);
    }
}


