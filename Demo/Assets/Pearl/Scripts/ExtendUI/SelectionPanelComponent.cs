using it.amalfi.Pearl.events;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace it.amalfi.Pearl.UI
{
    /// <summary>
    //  the class manages the active buttons by putting them in a stack so that 
    //  if the user comes back he can activate his previous actions.
    /// </summary>
    public class SelectionPanelComponent : LogicalComponent
    {
        #region Private Fields
        private Stack<GameObject> listSelectedButtons;
        #endregion

        #region Properties
        /// <summary>
        //  Returns the last gameobject selected
        /// </summary>
        public GameObject LastSelectedButton
        {
            get
            {
                if (listSelectedButtons.Count == 0)
                    return null;
                return listSelectedButtons.Peek();
            }
        }

        /// <summary>
        ///  Returns if the user is on the homepage of the UI
        /// </summary>
        public bool IsOpenPage { get { return LastSelectedButton == null; } }
        #endregion

        #region Constructors
        public SelectionPanelComponent()
        {
            listSelectedButtons = new Stack<GameObject>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Puts in the list the new gameobject to activate and activate it
        /// </summary>
        /// <param name = "obj"> The new gameobject</param>
        public void ObeyChangeSelectNext(GameObject obj)
        {
            if (obj != EventSystem.current.currentSelectedGameObject)
            {
                listSelectedButtons.Push(EventSystem.current.currentSelectedGameObject);
                EventSystem.current.SetSelectedGameObject(obj);
            }
        }

        /// <summary>
        /// Resets the stack and any activated gameobject
        /// </summary>
        public void ObeyReset()
        {
            EventSystem.current?.SetSelectedGameObject(null);
            listSelectedButtons.Clear();
        }

        /// <summary>
        /// Returns the previously active gameobject
        /// </summary>
        public void ObeyChangeInPreSelect()
        {
            if (listSelectedButtons.Count != 0)
                EventSystem.current.SetSelectedGameObject(listSelectedButtons.Pop());
        }
        #endregion
    }
}