using it.amalfi.Pearl.events;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using it.amalfi.Pearl.clock;

namespace it.amalfi.Pearl.audio
{
    /// <summary>
    ///  The singleton class automates the sound system using a mixer: examines its channels and creates a 
    ///  container for each of them. Containers allow the class to easily modify the audio.
    /// </summary>
    public class AudioManager : LogicalManager
    {
        #region Inspector Fields
        /// <summary>
        /// The audioMixer
        /// </summary>
        [SerializeField]
        private AudioMixer audioMixer;
        #endregion

        #region Private Fields
        private static readonly Range rangeAudioDb = new Range(-30, 10);
        private float volumeMixer;
        private List<AudioContainer> activeContainers;
        private bool isActivedContainer;
        private List<AudioContainer> auxList;
        AudioContainer auxContainer;
        #endregion

        #region Unity CallBacks
        protected override void OnAwake()
        {
            isActivedContainer = false;
            activeContainers = new List<AudioContainer>();
        }

        private void Update()
        {
            if (isActivedContainer)
                ExecuteChangeVolume();
        }
        #endregion

        #region Init Methods
        protected override void CreateComponents()
        {
            listComponents = new Dictionary<Type, LogicalComponent>
            {
                { typeof(ListAudioContainer), new ListAudioContainer() },
            };
        }
        #endregion

        #region Interface Methods

        #region Public Singleton Methods
        /// <summary>
        /// Sets Volume of specific channel of mixer
        /// </summary>
        /// <param name = "audioEnum">The channel of mixer</param>
        /// <param name = "newVolume">The volume to be reached</param>
        /// <param name = "isPercent">If it is false, the volume is expressed in decibels, it is true in a number in the interval [0, 1]</param>
        public void SetVolume(AudioEnum audioEnum, float newVolume, bool isPercent)
        {
            if (isPercent)
                newVolume = MathfExtend.ChangeRange(newVolume, rangeAudioDb);
            auxContainer = GetLogicalComponent<ListAudioContainer>().ObeyGetContainer(audioEnum);
            audioMixer.SetFloat(auxContainer.Name, newVolume);
        }

        /// <summary>
        /// Sets Volume of specific channel of mixer
        /// </summary>
        /// <param name = "audioEnum">The channel of mixer</param>
        /// <param name = "newVolume">The volume to be reached</param>
        /// <param name = "isPercent">If it is false, the volume is expressed in decibels, it is true in a number in the interval [0, 1]</param>
        /// <param name = "time"> Time for volume transition</param>
        /// <param name = "curve">The transition curve.If the curve does not exist, the volume change is linear, if it exists, the change follows the curve.</param>
        public void SetVolume(AudioEnum audioEnum, float value, bool isPercent, float time, AnimationCurve curve = null)
        {
            if (isPercent)
                value = MathfExtend.ChangeRange(value, rangeAudioDb);
            auxContainer = GetLogicalComponent<ListAudioContainer>().ObeyGetContainer(audioEnum);
            audioMixer.GetFloat(auxContainer.Name, out volumeMixer);

            if (value != volumeMixer)
            {
                volumeMixer = auxContainer.ObeyReset(volumeMixer, value, time, curve);
                activeContainers.Add(auxContainer);
                isActivedContainer = true;
            }
        }

        /// <summary>
        /// Returns the volume of specific channel of mixer
        /// </summary>
        public float GetVolume(AudioEnum audioEnum, bool isPercent)
        {
            auxContainer = GetLogicalComponent<ListAudioContainer>().ObeyGetContainer(audioEnum);
            audioMixer.GetFloat(auxContainer.Name, out volumeMixer);
            if (isPercent)
                volumeMixer = MathfExtend.Percent(volumeMixer, rangeAudioDb);
            return volumeMixer;
        }
        #endregion

        #endregion

        #region Logical Methods

        #region Private Methods
        private void ExecuteChangeVolume()
        {
            auxList = new List<AudioContainer>(activeContainers);
            for (int i = 0; i < activeContainers.Count; i++)
            {
                if (!auxList[i].ObeyIsFinish())
                    audioMixer.SetFloat(auxList[i].Name, auxList[i].ObeyReturnVolume());
                else
                {
                    activeContainers.RemoveAt(i);
                    if (activeContainers.Count == 0)
                        isActivedContainer = false;
                }
            }
        }
        #endregion

        #endregion
    }
}
