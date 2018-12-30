using UnityEngine;
using it.amalfi.Pearl.multitags;
using it.amalfi.Pearl.events;

namespace it.amalfi.Pearl.UI
{
    public class VisibilityPanelComponent : LogicalComponent
    {
        #region Private Fields
        private GameObject actualPanel;
        private GameObject auxPanel;
        #endregion

        #region Public Methods
        public bool ObeyIsSamePanel(GameObject obj)
        {
            auxPanel = FindPanelForSpecificUIObj(obj.transform).gameObject;
            return actualPanel.GetInstanceID() == auxPanel.GetInstanceID();
        }

        public void ObeyShow(GameObject obj, Transform transform)
        {
            ObeyOpenOrCloseAllPanels(false, transform);
            actualPanel = FindPanelForSpecificUIObj(obj.transform)?.gameObject;
            if (!actualPanel)
            {
                Debug.LogError("There isn't panel");
                return;
            }

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            actualPanel.SetActive(true);
        }

        public void ObeyOpenOrCloseAllPanels(bool open, Transform transform)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(open);
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
