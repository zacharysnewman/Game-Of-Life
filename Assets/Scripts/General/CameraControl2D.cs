using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Events.CameraControl;
using Events.InputEvents;

public class CameraControl2D : MonoBehaviour
{
    public Transform cameraTransform;
    private Camera camera2D;
    private Vector2 moveInput;
    private float zoomInput;

    public float moveSpeed = 2f;
    public float zoomSpeed = 2f;
    public float minZoom = 5f;
    public float maxZoom = 200f;

    private Vector2 prevMoveAmount = Vector2.zero;
    private float prevZoomAmount = 0;
    //public static CameraStoppedEvent OnCameraStopped = new CameraStoppedEvent();

    // Start is called before the first frame update
    void Start()
    {
        this.camera2D = this.cameraTransform.GetComponent<Camera>();
        EventAggregator.Get<CameraMovedEvent>().Subscribe(OnCameraMoved);
        EventAggregator.Get<CameraZoomedEvent>().Subscribe(OnCameraZoomed);
    }

    // Update is called once per frame
    void Update()
    {
        var moveAmount = this.moveInput * Time.deltaTime * 60 / 10 * this.moveSpeed;
        this.cameraTransform.Translate(moveAmount);

        var zoomAmount = this.zoomInput * Time.deltaTime * 60 / 10 * this.zoomSpeed;
        if (this.camera2D.orthographicSize + zoomAmount > this.minZoom && this.camera2D.orthographicSize + zoomAmount < this.maxZoom)
        {
            this.camera2D.orthographicSize += zoomAmount;
        }

        if ((this.prevMoveAmount != Vector2.zero && moveAmount == Vector2.zero) || (this.prevZoomAmount != 0 && zoomAmount == 0))
        {
            EventAggregator.Get<CameraStoppedEvent>().Publish();
        }
        this.prevMoveAmount = moveAmount;
        this.prevZoomAmount = zoomAmount;
    }

    public void OnCameraMoved(Vector2 moveInput)
    {
        this.moveInput = moveInput;
    }

    public void OnCameraZoomed(float zoomInput)
    {
        this.zoomInput = zoomInput;
    }
}