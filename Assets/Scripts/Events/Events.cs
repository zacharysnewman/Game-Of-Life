using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    namespace StateEvents
    {
        public class StateChangedPreviousToCurrentEvent : Event<State, State> { }
        public class StateChangedEvent : Event { }
        public class CurrentStateIndexChangedEvent : Event<int> { }
        public class StatePausedEvent : Event { }
    }

    namespace CameraControl
    {
        public class CameraStoppedEvent : Event { }
    }

    namespace UIEvents
    {
        public class TimelineScrubbedEvent : Event<int> { }
        public class PlaybackScaleChangedEvent : Event<int> { }
        public class ResetButtonPressedEvent : Event { }
        public class ClearButtonPressedEvent : Event { }
        public class PlayButtonPressedEvent : Event { }
        public class PauseButtonPressedEvent : Event { }
        public class PrevStepButtonPressedEvent : Event { }
        public class NextStepButtonPressedEvent : Event { }
    }

    namespace InputEvents
    {
        public class MouseClickedEvent : Event { }
        public class MouseMovedEvent : Event<Vector2> { }
        public class CameraMovedEvent : Event<Vector2> { }
        public class CameraZoomedEvent : Event<float> { }
    }
}
