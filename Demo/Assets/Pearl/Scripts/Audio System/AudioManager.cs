using it.amalfi.Pearl.events;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using it.amalfi.Pearl.clock;

namespace it.amalfi.Pearl.audio
{
    public class AudioManager : LogicalManager
    {
        #region Inspector Fields
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

        #region Public Methods
        public void SetVolume(AudioEnum audioEnum, float value)
        {
            Debug.Assert(value >= 0 && value <= 1);

            value = MathfExtend.ChangeRange(value, rangeAudioDb);
            AudioContainer container = GetLogicalComponent<ListAudioContainer>().GetContainer(audioEnum);
            audioMixer.SetFloat(container.Name, value);
        }

        public void SetVolume(AudioEnum audioEnum, float value, float time, AnimationCurve curve = null)
        {
            Debug.Assert(value >= 0 && value <= 1 && time >= 0);

            value = MathfExtend.ChangeRange(value, rangeAudioDb);
            auxContainer = GetLogicalComponent<ListAudioContainer>().GetContainer(audioEnum);
            audioMixer.GetFloat(auxContainer.Name, out volumeMixer);

            if (value != volumeMixer)
            {
                volumeMixer = auxContainer.ObeyReset(volumeMixer, value, time, curve);
                activeContainers.Add(auxContainer);
                isActivedContainer = true;
            }
        }

        public float GetVolume(AudioEnum audioEnum)
        {
            auxContainer = GetLogicalComponent<ListAudioContainer>().GetContainer(audioEnum);
            audioMixer.GetFloat(auxContainer.Name, out volumeMixer);
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
