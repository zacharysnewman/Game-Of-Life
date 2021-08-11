using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events.CameraControl;

public class DynamicGrid2D : MonoBehaviour
{
    public GameObject gridPrefab;

    public Transform gridParent;
    public Camera mainCamera;

    public List<GameObject> gridObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        UpdateGrid();
        EventAggregator.Get<CameraStoppedEvent>().Subscribe(UpdateGrid);
    }

    private void UpdateGrid()
    {
        ClearPreviousGrid();
        //mainCamera.orthographicSize = (int)mainCamera.orthographicSize;

        var screenH = Screen.height;
        var screenW = Screen.width;
        var camH = (mainCamera.orthographicSize * 2);
        var camW = ((camH / screenH) * screenW);

        for (int row = 0; row < (int)camH; row++)
        {
            var gridLine = GameObject.Instantiate(gridPrefab, new Vector3(this.mainCamera.transform.position.x, (row + 0.5f) - (camH / 2) + this.mainCamera.transform.position.y), Quaternion.identity, gridParent);
            gridLine.transform.localScale = new Vector3(camW, gridLine.transform.localScale.y, 1);
            this.gridObjects.Add(gridLine);
        }

        for (int col = 0; col < (int)camW; col++)
        {
            var gridLine = GameObject.Instantiate(gridPrefab, new Vector3((col + 0.5f) - (camW / 2) + this.mainCamera.transform.position.x, this.mainCamera.transform.position.y), Quaternion.identity, gridParent);
            gridLine.transform.localScale = new Vector3(gridLine.transform.localScale.x, camH, 1);
            this.gridObjects.Add(gridLine);
        }
    }

    private void ClearPreviousGrid()
    {
        foreach (var go in this.gridObjects)
        {
            GameObject.Destroy(go);
        }
        this.gridObjects.Clear();
    }
}
