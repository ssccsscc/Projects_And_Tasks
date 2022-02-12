using System;

namespace Logic
{
    /// <summary>
    /// The custom exception that being thrown when a user requests a nonexistent object. Later it's being replaced with a message using middleware
    /// </summary>
    [Serializable]
    public class LogicExceptionNotFound : Exception
    {
        public LogicExceptionNotFound(string text) : base(text) { }
    }
}
