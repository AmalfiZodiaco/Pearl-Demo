using UnityEngine;
using Pearl.multitags;
using Pearl.events;

namespace Pearl.UI
{
    /// <summary>
    /// This component is responsible for activating or deactivating panels
    /// </summary>
    public class VisibilityPanelComponent : LogicalComponent
    {
        #region Private Fields
        private GameObject actualPanel;
        private GameObject auxPanel;
        #endregion

        #region Obey Methods
        /// <summary>
        /// Check if the object panel is the same as the active panel.
        /// </summary>
        /// /// <param name = "obj"> The object to check</param>
        public bool ObeyIsSamePanel(GameObject obj)
        {
            auxPanel = FindPanelForSpecificUIObj(obj.transform).gameObject;
            return actualPanel.GetInstanceID() == auxPanel.GetInstanceID();
        }

        /// <summary>
        /// Check if the object panel is the same as the active panel.
        /// </summary>
        /// /// <param name = "obj"> The object to check</param>
        public void ObeyShow(GameObject obj)
        {
            actualPanel = FindPanelForSpecificUIObj(obj.transform)?.gameObject;
            ObeyOpenOrCloseAllPanels(false, actualPanel.transform.parent);

            Debug.Assert(actualPanel, "There isn't panel");

            actualPanel.SetActive(true);
        }

        /// <summary>
        /// Open or close all panels
        /// </summary>
        /// /// <param name = "obj"> The object to check</param>
        public void ObeyOpenOrCloseAllPanels(bool open, Transform transform)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(open);
            }
        }
        #endregion

        #region Private Methods
        private Transform FindPanelForSpecificUIObj(Transform transform)
        {
            if (transform.gameObject.HasTags(Tags.Panel))
                return transform;
            else if (transform.parent != null)
                return FindPanelForSpecificUIObj(transform.parent);
            else
                return null;
        }
        #endregion
    }
}
