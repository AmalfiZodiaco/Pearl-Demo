using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pearl.actionTrigger;
using Pearl.multitags;
using Pearl.events;

namespace Paerl.Demo.test
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
