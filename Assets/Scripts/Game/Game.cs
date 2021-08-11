using System.Collections.Generic;
using Events.StateEvents;
using static Ext;

public static class Game
{
    //public static OnStateChangePreviousToCurrent OnStateChanged = new OnStateChangePreviousToCurrent();
    private static int currentStateIndex = 0;
    private static int CurrentStateIndex
    {
        get => currentStateIndex;
        set
        {
            if (currentStateIndex != value)
            {
                currentStateIndex = value;
                EventAggregator.Get<CurrentStateIndexChangedEvent>().Publish(value);
            }
        }
    }
    private static List<State> stateHistory = new List<State>() { new State(new HashSet<Coords>()) };

    public static int GetStateCount() => Game.stateHistory.Count;
    public static State GetState() => Game.stateHistory[Game.CurrentStateIndex];
    public static State GetState(int index) => Game.stateHistory[index];
    public static State GetRelativeState(int index) => Game.stateHistory[Game.CurrentStateIndex + index];

    private static void AddState(State newState)
    {
        int lastElementIndex = Game.stateHistory.Count - 1;

        if (Game.CurrentStateIndex != lastElementIndex)
        {
            Game.stateHistory.RemoveRange(Game.CurrentStateIndex + 1, Game.stateHistory.Count - (Game.CurrentStateIndex + 1));
        }


        if (!IsStateRepeating(newState))
        {
            Game.stateHistory.Add(newState);
            EventAggregator.Get<StateChangedEvent>().Publish();
            EventAggregator.Get<StateChangedPreviousToCurrentEvent>().Publish(Game.GetState(), newState);

            Game.CurrentStateIndex++;
        }
        else
        {
            EventAggregator.Get<StatePausedEvent>().Publish();
        }
    }

    private static bool IsStateRepeating(State newState)
    {
        if (Game.CurrentStateIndex > 0)
        {
            for (int i = Game.CurrentStateIndex - 1; i >= 0 && i > Game.CurrentStateIndex - 5; i--)
            {
                if (Game.GetState(i) == newState)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static int GetStateIndex() => Game.CurrentStateIndex;
    public static void SetStateIndex(int newStateIndex)
    {
        EventAggregator.Get<StateChangedEvent>().Publish();
        EventAggregator.Get<StateChangedPreviousToCurrentEvent>().Publish(Game.GetState(), Game.GetState(newStateIndex));

        Game.CurrentStateIndex = newStateIndex;
    }

    public static void ClearState()
    {
        State clearedState = new State(new HashSet<Coords>());

        EventAggregator.Get<StateChangedEvent>().Publish();
        EventAggregator.Get<StateChangedPreviousToCurrentEvent>().Publish(Game.GetState(), clearedState);

        Game.stateHistory.Clear();
        Game.stateHistory = new List<State>() { clearedState };
        Game.CurrentStateIndex = 0;
    }

    public static void OverwriteStateWith(State newState)
    {
        EventAggregator.Get<StateChangedEvent>().Publish();
        EventAggregator.Get<StateChangedPreviousToCurrentEvent>().Publish(Game.GetState(), newState);

        Game.stateHistory.Clear();
        Game.stateHistory = new List<State>() { newState };
        Game.CurrentStateIndex = 0;
    }

    public static void ResetToInitial()
    {
        EventAggregator.Get<StateChangedEvent>().Publish();
        EventAggregator.Get<StateChangedPreviousToCurrentEvent>().Publish(Game.GetState(), Game.GetState(0));

        Game.CurrentStateIndex = 0;
    }

    public static void StepBackward()
    {
        if (Game.CurrentStateIndex - 1 >= 0)
        {
            EventAggregator.Get<StateChangedEvent>().Publish();
            EventAggregator.Get<StateChangedPreviousToCurrentEvent>().Publish(Game.GetState(), Game.GetRelativeState(-1));

            Game.CurrentStateIndex--;
        }
    }

    public static void StepForward()
    {
        int lastElementIndex = Game.stateHistory.Count - 1;
        if (Game.CurrentStateIndex == lastElementIndex || lastElementIndex < 0)
        {
            Game.Next();
        }
        else
        {
            EventAggregator.Get<StateChangedEvent>().Publish();
            EventAggregator.Get<StateChangedPreviousToCurrentEvent>().Publish(Game.GetState(), Game.GetRelativeState(1));

            Game.CurrentStateIndex++;
        }
    }

    private static void Next()
    {
        var allCoords = Game.GetState().GetAllCellCoords();
        var newLivingCells = new HashSet<Coords>();
        foreach (var coords in allCoords)
        {
            var neighborCount = coords.LivingNeighborCount(Game.GetState().LivingCells);
            var wasAlive = Game.GetState().LivingCells.Contains(coords);
            if (Game.WillSurviveOrBecomeAlive(neighborCount, wasAlive) && WithinWorldBounds(coords))
            {
                newLivingCells.Add(coords);
            }
        }

        Game.AddState(new State(newLivingCells));
    }

    private static bool WillSurviveOrBecomeAlive(int neighborCount, bool wasAlivePreviously) =>
        (!wasAlivePreviously && neighborCount == 3) || (wasAlivePreviously && neighborCount >= 2 && neighborCount <= 3);
    private static bool WithinWorldBounds(Coords coords) => coords.x > -200 && coords.x < 200 && coords.y > -200 && coords.y < 200;


    public static void ModifyCell(Coords coords)
    {
        var livingCells = new HashSet<Coords>(Game.GetState().LivingCells);
        if (livingCells.Contains(coords))
        {
            livingCells.Remove(coords);
        }
        else
        {
            livingCells.Add(coords);
        }
        Game.OverwriteStateWith(new State(livingCells));
    }
}

