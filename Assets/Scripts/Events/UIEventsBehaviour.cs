using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Events.UIEvents;
using Events.StateEvents;
using Events.InputEvents;

public class UIEventsBehaviour : MonoBehaviour
{
    public Slider timelineSlider;
    public Text currentStateIndexText;
    public Button resetButton;
    public Button clearButton;
    public Button playButton;
    public Button pauseButton;
    public Image playSliderBlocker;

    private bool isPlaying = false;
    private bool IsPlaying { get => this.isPlaying; set { this.isPlaying = value; } }

    public void OnChangePlaybackScale(float newSpeed)
    {
        EventAggregator.Get<PlaybackScaleChangedEvent>().Publish((int)newSpeed);
    }

    public void OnScrubTimeline(float rawStateIndex)
    {
        if (rawStateIndex > Game.GetStateCount() - 1)
        {
            rawStateIndex = Game.GetStateCount() - 1;
        }

        if (!this.IsPlaying)
        {
            EventAggregator.Get<TimelineScrubbedEvent>().Publish((int)rawStateIndex);
        }
    }

    public void OnMouseClick()
    {
        EventAggregator.Get<MouseClickedEvent>().Publish();
    }

    public void OnResetButtonPressed()
    {
        EventAggregator.Get<ResetButtonPressedEvent>().Publish();
    }

    public void OnClearButtonPressed()
    {
        EventAggregator.Get<ClearButtonPressedEvent>().Publish();
    }

    public void OnPlayButtonPressed()
    {
        this.IsPlaying = true;
        EventAggregator.Get<PlayButtonPressedEvent>().Publish();
        this.playButton.gameObject.SetActive(false);
        this.pauseButton.gameObject.SetActive(true);
        this.playSliderBlocker.gameObject.SetActive(true);
    }

    public void OnPauseButtonPressed()
    {
        this.IsPlaying = false;
        EventAggregator.Get<PauseButtonPressedEvent>().Publish();
        this.playButton.gameObject.SetActive(true);
        this.pauseButton.gameObject.SetActive(false);
        this.playSliderBlocker.gameObject.SetActive(false);
    }

    public void OnPrevStepButtonPressed()
    {
        EventAggregator.Get<PrevStepButtonPressedEvent>().Publish();
    }

    public void OnNextStepButtonPressed()
    {
        EventAggregator.Get<NextStepButtonPressedEvent>().Publish();
    }

    public void OnStatePaused()
    {
        this.IsPlaying = false;
        this.playButton.gameObject.SetActive(true);
        this.pauseButton.gameObject.SetActive(false);
        this.playSliderBlocker.gameObject.SetActive(false);
    }

    private void UpdateMaxTimelineValue()
    {
        this.timelineSlider.maxValue = Game.GetStateCount() - 1;
    }

    private void OnCurrentStateIndexChanged(int newStateIndex)
    {
        this.timelineSlider.value = newStateIndex;

        this.currentStateIndexText.text = newStateIndex.ToString();
        if (newStateIndex == 0)
        {
            this.clearButton.gameObject.SetActive(true);
            this.resetButton.gameObject.SetActive(false);
        }
        else
        {
            this.clearButton.gameObject.SetActive(false);
            this.resetButton.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        EventAggregator.Get<StateChangedEvent>().Subscribe(UpdateMaxTimelineValue);
        EventAggregator.Get<CurrentStateIndexChangedEvent>().Subscribe(OnCurrentStateIndexChanged);
        EventAggregator.Get<StatePausedEvent>().Subscribe(OnStatePaused);
    }
    private void OnDestroy()
    {
        EventAggregator.Get<StateChangedEvent>().Unsubscribe(UpdateMaxTimelineValue);
        EventAggregator.Get<CurrentStateIndexChangedEvent>().Unsubscribe(OnCurrentStateIndexChanged);
        EventAggregator.Get<StatePausedEvent>().Unsubscribe(OnStatePaused);
    }
}