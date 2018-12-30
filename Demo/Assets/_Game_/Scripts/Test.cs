using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using it.amalfi.Pearl.actionTrigger;
using it.amalfi.Pearl.multitags;
using it.amalfi.Pearl.events;

namespace it.demo.test
{
    public class Test : MonoBehaviour, ITrigger
    {
        public void Trigger(Informations informations, List<Tags> tags)
        {
            if (tags.Contains(Tags.Attack))
                Debug.Log("I was hit, I took " + informations.Take<byte>("damage") + " of damage");
            if (tags.Contains(Tags.Use))
                EventsManager.CallEvent(EventAction.SendHealth, 0.5f);
        }
    }
}
