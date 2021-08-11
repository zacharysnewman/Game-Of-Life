using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events.StateEvents;
using Events.InputEvents;
using Events.UIEvents;

using static Ext;

public class GameBehaviour : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform cellParent;
    public Camera mainCamera;

    private Dictionary<Coords, GameObject> livingCells = new Dictionary<Coords, GameObject>();
    private Vector2 mousePosition;
    //private readonly float playbackDelay = 0.5f;
    private int playbackSpeedScale = 1;

    //private State stateOnPlay;

    private bool isPlaying = false;
    public void Play()
    {
        if (this.isPlaying)
            return;

        StartCoroutine(_Play());
    }

    // Start is called before the first frame update
    public IEnumerator _Play()
    {
        this.isPlaying = true;

        while (true)
        {
            if (Time.frameCount % (61 - playbackSpeedScale) == 0)
                Game.StepForward();

            yield return new WaitForEndOfFrame();//WaitForSeconds(this.playbackDelay / this.playbackSpeedScale);
        }
    }

    public void Pause()
    {
        if (this.isPlaying)
        {
            this.isPlaying = false;
            StopAllCoroutines();
        }
    }

    public void PrevStep()
    {
        Game.StepBackward();
    }

    public void NextStep()
    {
        Game.StepForward();
    }

    public void Reset()
    {
        Game.ResetToInitial();
    }

    public void Clear()
    {
        Game.ClearState();
    }

    public void OnMouseMove(Vector2 mousePosition)
    {
        this.mousePosition = mousePosition;
    }

    public void OnMouseClick()
    {
        var ray = this.mainCamera.ScreenPointToRay(this.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000))
        {
            var x = Mathf.RoundToInt(hitInfo.point.x);
            var y = Mathf.RoundToInt(hitInfo.point.y);
            Game.ModifyCell(new Coords(x, y));
        }
    }

    public void OnChangePlaybackScale(int newSpeed)
    {
        this.playbackSpeedScale = newSpeed;
    }

    public void OnScrubTimeline(int stateIndex)
    {
        Game.SetStateIndex(stateIndex);
    }

    private void Render(State prevState, State newState)
    {
        var prevCells = prevState.LivingCells;
        var newCells = newState.LivingCells;
        var cellsToDelete = ExceptWith(prevCells, newCells);
        var cellsToCreate = ExceptWith(newCells, prevCells);

        this.livingCells = DestroyLivingCellObjects(cellsToDelete, this.livingCells);
        this.livingCells = CreateLivingCellObjects(cellsToCreate, this.livingCells);
    }

    //private void CheckForEnd(State prevState, State newState)
    //{
    //    if (ExceptWith(newState.LivingCells, prevState.LivingCells).Count == 0)
    //    {
    //        Pause();
    //        //EventAggregator.Get<PauseButtonPressedEvent>
    //    }
    //}

    private Dictionary<Coords, GameObject> CreateLivingCellObjects(HashSet<Coords> multipleCellCoords, Dictionary<Coords, GameObject> cellObjects)
    {
        foreach (var cellCoords in multipleCellCoords)
        {
            var livingCellObject = GameObject.Instantiate(cellPrefab, cellCoords, Quaternion.identity, this.cellParent); // Impossible to avoid, Unity side effects
            cellObjects.Add(cellCoords, livingCellObject);
        }
        return new Dictionary<Coords, GameObject>(cellObjects);
    }

    private Dictionary<Coords, GameObject> DestroyLivingCellObjects(HashSet<Coords> multipleCellCoords, Dictionary<Coords, GameObject> cellObjects)
    {
        foreach (var cellCoords in multipleCellCoords)
        {
            GameObject.Destroy(cellObjects[cellCoords]); // Impossible to avoid, Unity side effects
            cellObjects.Remove(cellCoords);
        }
        return new Dictionary<Coords, GameObject>(cellObjects);
    }

    public void Start()
    {
        EventAggregator.Get<StateChangedPreviousToCurrentEvent>().Subscribe(Render);
        EventAggregator.Get<MouseMovedEvent>().Subscribe(OnMouseMove);
        EventAggregator.Get<MouseClickedEvent>().Subscribe(OnMouseClick);
        EventAggregator.Get<PlaybackScaleChangedEvent>().Subscribe(OnChangePlaybackScale);
        EventAggregator.Get<TimelineScrubbedEvent>().Subscribe(OnScrubTimeline);
        EventAggregator.Get<ResetButtonPressedEvent>().Subscribe(Reset);
        EventAggregator.Get<ClearButtonPressedEvent>().Subscribe(Clear);
        EventAggregator.Get<PlayButtonPressedEvent>().Subscribe(Play);
        EventAggregator.Get<PauseButtonPressedEvent>().Subscribe(Pause);
        EventAggregator.Get<StatePausedEvent>().Subscribe(Pause);
        EventAggregator.Get<PrevStepButtonPressedEvent>().Subscribe(PrevStep);
        EventAggregator.Get<NextStepButtonPressedEvent>().Subscribe(NextStep);
    }

    private void OnDestroy()
    {
        EventAggregator.Get<StateChangedPreviousToCurrentEvent>().Unsubscribe(Render);
        EventAggregator.Get<MouseMovedEvent>().Unsubscribe(OnMouseMove);
        EventAggregator.Get<MouseClickedEvent>().Unsubscribe(OnMouseClick);
        EventAggregator.Get<PlaybackScaleChangedEvent>().Unsubscribe(OnChangePlaybackScale);
        EventAggregator.Get<TimelineScrubbedEvent>().Unsubscribe(OnScrubTimeline);
        EventAggregator.Get<ResetButtonPressedEvent>().Unsubscribe(Reset);
        EventAggregator.Get<ClearButtonPressedEvent>().Unsubscribe(Clear);
        EventAggregator.Get<PlayButtonPressedEvent>().Unsubscribe(Play);
        EventAggregator.Get<PauseButtonPressedEvent>().Unsubscribe(Pause);
        EventAggregator.Get<StatePausedEvent>().Unsubscribe(Pause);
        EventAggregator.Get<PrevStepButtonPressedEvent>().Unsubscribe(PrevStep);
        EventAggregator.Get<NextStepButtonPressedEvent>().Unsubscribe(NextStep);
    }
}
