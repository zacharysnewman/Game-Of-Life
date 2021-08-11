using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TODO : MonoBehaviour
{
    /* Share to GitHub
     * 
     * --- Bonus ---
     * 
     * Performance enhancements
     * - State implements IEnumerable
     * - State should contain logic for living cells etc.
     * - State should contain logic for end check
     * - Game should only save 1/100(?) states and resimulate from closest previous state when stepping or scrubbing backwards
     * - Play steps should be framerate-independent
     * - Lock Movement to world bounds
     * 
     * (√) Fix bug with pause on click
     * 
     * (√) Add hashcode and equals for State
     * 
     * End Check (Game)
     * (√) Try iterating through all previous states to see if any are equal, if so, stop
     * 
     * Playback speed
     * (√) Higher max speed scale
     * 
     * Decoupling
     * (√) Decouple all UI Controls from GameBehaviour or other classes
     * (√) All UI Controls should only talk to UIEventsBehaviour
     * (√) PrevStep
     * (√) NextStep
     * 
     * Timeline bar updates
     * (√) Should update on statechange while playing
     * (√) Should update after resetting
     * (√) Should update after clearing (Clicking timeline bar after clearing throws exception)
     * (√) Should not "update" after scrubbing
     * 
     * Auto-pause when clicking on-screen (Event?)
     * (√) Stop Play coroutine
     * (√) Enable Play Button
     * (√) Disable Pause Button
     * 
     * Clear
     * (√) Only visible while at index 0
     * (√) Should reset stateHistory to one empty state in the list 
     * 
     * Reset
     * (√) Only visible while NOT at index 0
     * (√) Restart at the beginning since last added cell
     * 
     * NEW Click To Place Cell
     * (√) UI Button filling the rest of the screen
     * (√) Triggers event to cast ray like other mouse click event
     * (√) Remove old method from PlayerInput, etc.
     * (√) Add text to Slider blocker
     * 
     * Fix Bugs
     * (√) Whenever you place a cell manually, that state is the the new index 0!!!
     * 
     * Integrate Event Aggregator
     * (√) Use new Event instead of UnityEvent
     * 
     * Fully functioning timeline bar
     * (√) Scrubbing
     * (X) Auto pause on scrub
     * (√) Whole numbers only
     * (√) to (dynamic) stateHistory.Count
     * 
     * Current state index display
     * (√) Always shows current state index
     * 
     * (X) (REMOVE GRID) Grid
     * - Scaling 2D grid lines based off camera size and screen dimensions
     * 
     * (X) (NO TOOLS AT THIS TIME) Tools
     * - Paint: Drag to paint multiple tiles
     * - Erase: Drag to erase multiple tiles
     * - Square Paint: Drag to draw a square of scaling size
     * - Square Erase: Drag to erase a square of scaling size
     * - Copy/Paste Area: Drag to select area, click to paste
     * 
     * (X) (NO PATTERNS AT THIS TIME) Patterns
     * - Saving and loading patterns from memory
     */
}
