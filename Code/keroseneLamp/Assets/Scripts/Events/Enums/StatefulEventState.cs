﻿namespace Events
{
    /// <summary>
    /// Describes an event state.
    /// Event state is set to:
    ///    0) Undefined, when the state is unknown or not needed
    ///    1) Enter, when 2 bodies are interacting in the current frame, but they did not interact the previous frame
    ///    2) Stay, when 2 bodies are interacting in the current frame, and they also interacted in the previous frame
    ///    3) Exit, when 2 bodies are not interacting in the current frame, but they did interact in the previous frame
    /// </summary>
    public enum StatefulEventState
    {
        Undefined,
        Enter,
        Stay,
        Exit
    }
}
