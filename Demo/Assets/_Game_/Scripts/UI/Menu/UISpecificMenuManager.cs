using it.amalfi.Pearl.audio;
using it.amalfi.Pearl.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using it.amalfi.Pearl;
using it.amalfi.Pearl.events;

namespace it.demo.UI
{
    public class UISpecificMenuManager : UIMenuManager
    {
        #region Private Methods
        private Dictionary<AudioEnum, Slider> sliders;
        #endregion

        #region Override Methods
        protected override void OnAwake()
        {
            SetMusicSlider();
        }
        #endregion

        #region Public Methods
        public void ChangeSlide(string nameSlider)
        {
            AudioEnum audioEnum = EnumExtend.ParseEnum<AudioEnum>(nameSlider);
            EventsManager.GetIstance<AudioManager>().SetVolume(audioEnum, sliders[audioEnum].value, true);
        }
        #endregion

        #region Private Methods
        private void SetMusicSlider()
        {
            AudioManager audioManager = EventsManager.GetIstance<AudioManager>();
            Transform parent = transform.Find("OptionsPanel/");
            sliders = new Dictionary<AudioEnum, Slider>
            {
                { AudioEnum.Music, parent.Find("SliderMusic").GetComponent<Slider>() },
                { AudioEnum.SoundEffects, parent.Find("SliderEffects").GetComponent<Slider>() }
            };

            sliders[AudioEnum.Music].value = audioManager.GetVolume(AudioEnum.Music, true);
            sliders[AudioEnum.SoundEffects].value = audioManager.GetVolume(AudioEnum.SoundEffects, true);
        }
        #endregion
    }
}
