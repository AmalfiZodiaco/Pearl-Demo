using it.amalfi.Pearl.events;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace it.amalfi.Pearl.UI
{
    public class SelectionPanelComponent : LogicalComponent
    {
        #region Private Fields
        private Stack<GameObject> listSelectedButtons;
        #endregion

        #region Property
        public GameObject LastSelectedButton
        {
            get
            {
                if (listSelectedButtons.Count == 0)
                    return null;
                return listSelectedButtons.Peek();
            }
        }

        public bool IsOpenPage { get { return LastSelectedButton == null; } }
        #endregion

        #region Constructors
        // Use this for initialization
        public SelectionPanelComponent()
        {
            listSelectedButtons = new Stack<GameObject>();
        }
        #endregion

        #region Public Methods
        public void ObeyChangeSelectNext(GameObject obj)
        {
            if (obj != EventSystem.current.currentSelectedGameObject)
            {
                listSelectedButtons.Push(EventSystem.current.currentSelectedGameObject);
                EventSystem.current.SetSelectedGameObject(obj);
            }
        }

        public void ObeyReset()
        {
            EventSystem.current?.SetSelectedGameObject(null);
            listSelectedButtons.Clear();
        }

        public void ObeyChangeInPreSelect()
        {
            if (listSelectedButtons.Count != 0)
                EventSystem.current.SetSelectedGameObject(listSelectedButtons.Pop());
        }
        #endregion
    }
}