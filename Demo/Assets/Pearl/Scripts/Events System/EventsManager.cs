using System;
using System.Collections.Generic;
using UnityEngine;

namespace it.amalfi.Pearl.events
{
    /// <summary>
    /// This class manages the communication between different gameobjects 
    /// in a centralized way. For example, the gameobject A must 
    /// receive instructions from the gamebject B: A subscribes to an 
    /// event C of the "EventsManager" class, whereas when B wants to call A, it 
    /// invokes the C event of the "EventsManager" class with the necessary parameters.
    /// Event C invokes object A and all other objects subscribed to C.
    /// The events are distinguished by an enumerator (EventAction).
    /// </summary>
    public static class EventsManager
    {
        #region Private fields
        /// <summary>
        /// A dictionary that invokes the specific event from the enumerator
        /// </summary>
        private static Dictionary<EventAction, Delegate> dictionaryEvent;
        #endregion

        #region Constructors
        /// <summary>
        /// The static constructor of this class
        /// </summary>
        static EventsManager()
        {
            CreateDictonary();
        }
        #endregion

        #region Init Methods
        /// <summary>
        /// The method that creates events.The method creates as many events as 
        /// the length of the enumerrator.
        /// </summary>
        private static void CreateDictonary()
        {
            dictionaryEvent = new Dictionary<EventAction, Delegate>();
            int lenght = EnumExtend.Lenght<EventAction>();
            for (int i = 0; i < lenght; i++)
            {
                dictionaryEvent.Add((EventAction)i, null);
            }
        }
        #endregion

        #region Add method in event
        /// <summary>
        /// The function adds a method with zero parameters to a specific event
        /// </summary>
        public static void AddMethod(EventAction eventAction, genericDelegate action)
        {
            dictionaryEvent[eventAction] = (genericDelegate)dictionaryEvent[eventAction] + action;
        }

        /// <summary>
        /// The function adds a method with one parameter to a specific event
        /// </summary>
        public static void AddMethod<T>(EventAction eventAction, genericDelegate<T> action)
        {
            dictionaryEvent[eventAction] = (genericDelegate<T>)dictionaryEvent[eventAction] + action;
        }

        /// <summary>
        /// The function adds a method with two parameters to a specific event
        /// </summary>
        public static void AddMethod<T, F>(EventAction eventAction, genericDelegate<T, F> action)
        {
            dictionaryEvent[eventAction] = (genericDelegate<T, F>)dictionaryEvent[eventAction] + action;
        }
        #endregion

        #region Remove method in event
        /// <summary>
        /// The function remove a method with zero parameters to a specific event
        /// </summary>
        public static void RemoveMethod(EventAction eventAction, genericDelegate action)
        {
            dictionaryEvent[eventAction] = (genericDelegate) dictionaryEvent[eventAction] - action;
        }

        /// <summary>
        /// The function remove a method with one parameter to a specific event
        /// </summary>
        public static void RemoveMethod<T>(EventAction eventAction, genericDelegate<T> action)
        {
            dictionaryEvent[eventAction] = (genericDelegate<T>) dictionaryEvent[eventAction] - action;
        }

        /// <summary>
        /// The function remove a method with two parameters to a specific event
        /// </summary>
        public static void RemoveMethod<T, F>(EventAction eventAction, genericDelegate<T, F> action)
        {
            dictionaryEvent[eventAction] = (genericDelegate<T, F>) dictionaryEvent[eventAction] - action;
        }
        #endregion

        #region Invoke event
        /// <summary>
        /// This function calls the specific event to activate all its subscribed methods
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
