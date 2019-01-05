namespace it.amalfi.Pearl.events
{
    /// <summary>
    /// The father of all the components of the manager
    /// </summary>
    public abstract class LogicalComponent
    {
        #region Unity CallBacks
        /// <summary>
        /// A method called when the manager is destroyed
        /// </summary>
        public virtual void OnDestroy()
        {
        }
        #endregion
    }
}
