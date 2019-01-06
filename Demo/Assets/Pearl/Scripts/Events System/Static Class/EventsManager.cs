using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pearl.events
{
    /// <summary>
    /// This static class manages the communication between different gameobjects in a centralized way. 
    /// For example, the gameobject "A" must receive instructions from the gamebject "B": 
    /// A subscribes to an event "eV" of the "EventsManager" class. When "B" wants to call "A", it 
    /// invokes the "eV" event of the "EventsManager" class with the necessary parameters.
    /// Event "eV" invokes object "A" and all other objects subscribed to "eV".
    /// The events are distinguished by an enumerator (EventAction). 
    /// Moreover the class manages singleton
    /// </summary>
    public static class EventsManager
    {
        #region Private fields
        private static Dictionary<EventAction, Delegate> dictionaryEvent;
        private static Dictionary<Type, LogicalSimpleManager> dictionarySingleton;
        #endregion

        #region Constructors
        static EventsManager()
        {
            dictionarySingleton = new Dictionary<Type, LogicalSimpleManager>();
            CreateDictonary();
        }
        #endregion

        #region Init Methods
        private static void CreateDictonary()
        {
            dictionaryEvent = new Dictionary<EventAction, Delegate>();
            int lenght = EnumExtend.Length<EventAction>();
            for (int i = 0; i < lenght; i++)
            {
                dictionaryEvent.Add((EventAction)i, null);
            }
        }
        #endregion

        #region Singleton manager
        private static void AddSingleton(LogicalSimpleManager[] istance)
        {
            Debug.Assert(istance.Length == 1, "There isn't Singleton or there too istances of singleton");

            dictionarySingleton.Update(istance[0].GetType(), istance[0]);
        }

        private static bool IsntThereSingleton<T>()
        {
            return !dictionarySingleton.ContainsKey(typeof(T)) || dictionarySingleton[typeof(T)] == null;
        }

        /// <summary>
        ///  Returns the T singleton istance
        /// </summary>
        public static T GetIstance<T>() where T : LogicalSimpleManager
        {
            if (IsntThereSingleton<T>())
                AddSingleton(GameObject.FindObjectsOfType<T>());
            return (T) dictionarySingleton[typeof(T)];
        }
        #endregion

        #region Add method in event
        /// <summary>
        /// The function adds a action (method that returns void) with zero parameters to a specific event
        /// </summary>
        /// <param name = "eventAction">The dictionary key associated with the event</param>
        /// <param name = "action">The action</param>
        public static void AddMethod(EventAction eventAction, genericDelegate action)
        {
            dictionaryEvent[eventAction] = (genericDelegate)dictionaryEvent[eventAction] + action;
        }

        /// <summary>
        /// The function adds a action (method that returns void) with one parameter to a specific event
        /// </summary>
        /// <param name = "eventAction">The dictionary key associated with the event</param>
        /// <param name = "action">The action</param>
        public static void AddMethod<T>(EventAction eventAction, genericDelegate<T> action)
        {
            dictionaryEvent[eventAction] = (genericDelegate<T>)dictionaryEvent[eventAction] + action;
        }

        /// <summary>
        /// The function adds a action (method that returns void) with two parameters to a specific event
        /// </summary>
        /// <param name = "eventAction">The dictionary key associated with the event</param>
        /// <param name = "action">The action</param>
        public static void AddMethod<T, F>(EventAction eventAction, genericDelegate<T, F> action)
        {
            dictionaryEvent[eventAction] = (genericDelegate<T, F>)dictionaryEvent[eventAction] + action;
        }
        #endregion

        #region Remove method in event
        /// <summary>
        /// The function remove a action (method that returns void) with zero parameters to a specific event
        /// </summary>
        /// <param name = "eventAction">The dictionary key associated with the event</param>
        /// <param name = "action">The action</param>
        public static void RemoveMethod(EventAction eventAction, genericDelegate action)
        {
            dictionaryEvent[eventAction] = (genericDelegate) dictionaryEvent[eventAction] - action;
        }

        /// <summary>
        /// The function remove a action (method that returns void) with one parameter to a specific event
        /// </summary>
        /// <param name = "eventAction">The dictionary key associated with the event</param>
        /// <param name = "action">The action</param>
        public static void RemoveMethod<T>(EventAction eventAction, genericDelegate<T> action)
        {
            dictionaryEvent[eventAction] = (genericDelegate<T>) dictionaryEvent[eventAction] - action;
        }

        /// <summary>
        /// The function remove a action (method that returns void) with two parameters to a specific event
        /// </summary>
        /// <param name = "eventAction">The dictionary key associated with the event</param>
        /// <param name = "action">The action</param>
        public static void RemoveMethod<T, F>(EventAction eventAction, genericDelegate<T, F> action)
        {
            dictionaryEvent[eventAction] = (genericDelegate<T, F>) dictionaryEvent[eventAction] - action;
        }
        #endregion

        #region Invoke event
        /// <summary>
        /// This function calls the specific event to activate all its subscribed methods
        /// <param name = "eventAction">The dictionary key associated with the event</param>
        /// /// <param name = "objects">The parameters for invoke</param>
        /// </summary>
        public static void CallEvent(EventAction action, params System.Object[] objects)
        {
            switch (objects.Length)
            {
                case 0:
                    dictionaryEvent[action]?.DynamicInvoke();
                    break;
                case 1:
                    dictionaryEvent[action]?.DynamicInvoke(objects[0]);
                    break;
                case 2:
                    dictionaryEvent[action]?.DynamicInvoke(objects[0], objects[1]);
                    break;
            }
        }
        #endregion
    }
}
