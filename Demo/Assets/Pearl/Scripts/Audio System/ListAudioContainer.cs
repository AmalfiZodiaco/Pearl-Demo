using it.amalfi.Pearl.events;
using System.Collections.Generic;
using System;

namespace it.amalfi.Pearl.audio
{
    public class ListAudioContainer : LogicalComponent
    {
        #region Private fields
        private Dictionary<AudioEnum, AudioContainer> containers;
        #endregion

        #region Constructors
        public ListAudioContainer()
        {
            CreateDictonary();
        }
        #endregion

        #region Init Methods
        private void CreateDictonary()
        {
            int lenght = EnumExtend.Lenght<AudioEnum>();
            containers = new Dictionary<AudioEnum, AudioContainer>();
            for (int i = 0; i < lenght; i++)
            {
                string stringVolume = ((AudioEnum)i).ToString();
                stringVolume = Char.ToLowerInvariant(stringVolume[0]) + stringVolume.Substring(1) + "Volume";
                containers.Add((AudioEnum)i, new AudioContainer(stringVolume));
            }
        }
        #endregion

        #region Public Methods
        public AudioContainer GetContainer(AudioEnum audioEnum)
        {
            return containers[audioEnum];
        }
        #endregion
    }
}
