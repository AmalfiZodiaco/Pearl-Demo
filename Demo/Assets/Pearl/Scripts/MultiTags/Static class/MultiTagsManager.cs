using System.Collections.Generic;
using UnityEngine;

namespace Pearl.multitags
{
    /// <summary>
    /// Static class that allow you to take advantage of the multitags
    /// </summary>
    public static class MultiTagsManager
    {
        private static List<GameObject> auxListGameObject;
        private static MultiTags[] auxArrayMultiTags;
        private static MultiTags auxMultiTags;
        private static GameObject[] auxArrayGameObject;
		
		#region Constructors
        static MultiTagsManager()
        {
            auxListGameObject = new List<GameObject>();
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Searches the gameobjects with tags.
        /// </summary>
        /// <param name = "only">This bool specifies whether the Gameobject should have only those tags</param>
        /// <param name = "tagsParameter">The tags that must have the object</param>
        public static GameObject[] FindGameObjectsWithMultiTags(bool only, params Tags[] tagsParameter)
        {
            Debug.Assert(tagsParameter != null && tagsParameter.Length != 0);

            auxListGameObject.Clear();
            auxArrayMultiTags = GameObject.FindObjectsOfType(typeof(MultiTags)) as MultiTags[];

            for (int i = 0; i < auxArrayMultiTags.Length; i++)
            {
                if ((!only && auxArrayMultiTags[i].ListTags.Count < tagsParameter.Length) || (only && auxArrayMultiTags[i].ListTags.Count != tagsParameter.Length))
                    continue;

                if (AreThereTagsInList(auxArrayMultiTags[i].ListTags, tagsParameter))
                    auxListGameObject.Add(auxArrayMultiTags[i].gameObject);
            }

            if (auxListGameObject.Count > 0)
                return auxListGameObject.ToArray();
            else
                return null;
        }

        /// <summary>
        /// Searches a gameobject with these tags.
        /// </summary>
        /// <param name = "only">This bool specifies whether the Gameobject should have only those tags</param>
        /// <param name = "tagsParameter">The tags that must have the object</param>
        public static GameObject FindGameObjectWithMultiTags(bool only, params Tags[] tagsParameter)
        {
            Debug.Assert(tagsParameter != null && tagsParameter.Length != 0);

            auxArrayGameObject = FindGameObjectsWithMultiTags(only, tagsParameter);
            if (auxArrayGameObject != null)
                return auxArrayGameObject[0];
            else
                return null;
        }
        #endregion

        #region Extend Methods
        /// <summary>
        /// Are there tags in this GameObject?
        /// </summary>
        /// <param name = "value">The gameobject</param>
        /// <param name = "tagsParameter">The tags that must have the GameObject</param>
        public static bool HasTags(this GameObject value, params Tags[] tagsParameter)
        {
            Debug.Assert(value != null && tagsParameter != null);

            auxMultiTags = value.GetComponent<MultiTags>();
            if (auxMultiTags == null)
                return !(tagsParameter.Length > 0);
            return AreThereTagsInList(auxMultiTags.ListTags, tagsParameter);
        }

        /// <summary>
        /// Return all tags in the specific GameObject
        /// </summary>
        /// <param name = "value">The specific gameobject</param> 
        public static List<Tags> ReturnTags(this GameObject value)
        {
            Debug.Assert(value != null);

            auxMultiTags = value.GetComponent<MultiTags>();
            if (auxMultiTags == null || auxMultiTags.ListTags.Count < 0)
                return null;
            return auxMultiTags.ListTags;
        }

        /// <summary>
        /// Add these tags in the specific GameObject
        /// </summary>
        /// <param name = "value">The specific gameobject</param>
        /// <param name = "tagsParameter">The tags that must be added to the GameObject</param>
        public static void AddTags(this GameObject value, params Tags[] tagsParameter)
        {
            Debug.Assert(value != null && tagsParameter != null);

            auxMultiTags = value.AddOnlyOneComponent<MultiTags>();

            for (int i = 0; i < tagsParameter.Length; i++)
            {
                if (auxMultiTags.ListTags.BinarySearch(tagsParameter[i]) < 0)
                    auxMultiTags.ListTags.Add(tagsParameter[i]);
            }

            auxMultiTags.ListTags.Sort();
        }

        /// <summary>
        /// Remove these tags in the specific GameObject
        /// </summary>
        /// <param name = "value">The specific gameobject</param>
        /// <param name = "tagsParameter">The tags that must be removed to the GameObject</param>
        public static void RemoveTags(this GameObject value, params Tags[] tagsParameter)
        {
            Debug.Assert(value != null && tagsParameter != null);

            auxMultiTags = value.GetComponent<MultiTags>();
            if (auxMultiTags == null || tagsParameter.Length == 0)
                return;

            for (int i = 0; i < tagsParameter.Length; i++)
                auxMultiTags.ListTags.Remove(tagsParameter[i]);

            auxMultiTags.ListTags.Sort();
        }
        #endregion

        #region Private Methods
        private static bool AreThereTagsInList(List<Tags> tags, params Tags[] tagsParameter)
        {
            Debug.Assert(tags != null && tagsParameter != null);

            if (tagsParameter.Length == 0)
                return true;

            if (tags.Count == 0)
                return false;
            bool areThereTags = false;

            for (int i = 0; i < tagsParameter.Length; i++)
            {
                if (tags.BinarySearch(tagsParameter[i]) >= 0)
                    areThereTags = true;
                else
                {
                    areThereTags = false;
                    break;
                }
            }
            return areThereTags;
        }
        #endregion
    }
}
