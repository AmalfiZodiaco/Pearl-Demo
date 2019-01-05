using it.amalfi.Pearl.events;
using System.Collections.Generic;
using System;

namespace it.amalfi.Pearl.audio
{
    /// <summary>
    /// A class that represents a list of mixer channel (AudioContainer).
    /// </summary>
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
            int lenght = EnumExtend.Length<AudioEnum>();
            containers = new Dictionary<AudioEnum, AudioContainer>();
            for (int i = 0; i < lenght; i++)
            {
                string stringVolume = ((AudioEnum)i).ToString();
                stringVolume = Char.ToLowerInvariant(stringVolume[0]) + stringVolume.Substring(1) + "Volume";
                containers.Add((AudioEnum)i, new AudioContainer(stringVolume));
            }
        }
        #endregion

        #region Obey Methods
        /// <summary>
        /// Returns a specific conteiner
        /// </summary>
        /// <param name = "audioEnum">The enumerator of the audio mixer channel</param>
        public AudioContainer ObeyGetContainer(AudioEnum audioEnum)
        {
            return containers[audioEnum];
        }
        #endregion
    }
}
