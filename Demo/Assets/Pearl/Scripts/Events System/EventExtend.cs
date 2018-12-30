namespace it.amalfi.Pearl.events
{
    /// <summary>
    /// A event handler with a parameter generic
    /// </summary>
    /// <param name = "param1"> This is first parameter of generic delegate</param>
    /// <param name = "param2"> This is the second parameter of generic delegate</param>
    public delegate void genericDelegate<T, F>(T param1, F param2);

    /// <summary>
    /// A event handler with a parameter generic
    /// </summary>
    /// <param name = "parm1"> This first parameter of generic method</param>
    public delegate void genericDelegate<T>(T param1);

    /// <summary>
    /// A event handler with no parameter very generic
    /// </summary>
    public delegate void genericDelegate();
}