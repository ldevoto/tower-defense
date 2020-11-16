using UnityEngine;
using UnityEngine.Audio;

namespace TowerDefense.SO
{
    [CreateAssetMenu(fileName = "AudioMixerController", menuName = "SO/AudioMixerController", order = 0)]
    public class AudioMixerController : ScriptableObject
    {
        [SerializeField] private string MasterVolumeProperty = "MasterVolume";
        [SerializeField] private string MusicVolumeProperty = "MusicVolume";
        [SerializeField] private string SFXVolumeProperty = "SFXVolume";
        [SerializeField] private AudioMixer masterMixer = null;
        
        public float masterVolume = 1f;
        public float musicVolume = 1f;
        public float sfxVolume = 1f;
        public bool isMuted = false;

        private float lastMasterVolume = 1f;

        public void Initialize()
        {
            lastMasterVolume = masterVolume;
            SetMasterVolume(masterVolume);
            SetMusicVolume(musicVolume);
            SetSFXVolume(sfxVolume);
            UpdateMute();
        }
        
        public void SetMasterVolume(float masterVolume)
        {
            if (masterVolume == 0f)
            {
                isMuted = true;
                lastMasterVolume = this.masterVolume;
            }
            this.masterVolume = masterVolume;
            SetVolume(MasterVolumeProperty, masterVolume);
        }

        public void SetMusicVolume(float musicVolume)
        {
            this.musicVolume = musicVolume;
            SetVolume(MusicVolumeProperty, musicVolume);
        }

        public void SetSFXVolume(float sfxVolume)
        {
            this.sfxVolume = sfxVolume;
            SetVolume(SFXVolumeProperty, sfxVolume);
        }

        public void ToggleMute()
        {
            isMuted = !isMuted;
            UpdateMute();
        }

        private void UpdateMute()
        {
            SetMasterVolume(isMuted ? 0f : lastMasterVolume);
        }
        
        private void SetVolume(string exposedName, float value)
        {
            masterMixer.SetFloat(exposedName, Mathf.Lerp(-80.0f, 0.0f, Mathf.Clamp01(value)));
        }
 
        private float GetVolume(string exposedName)
        {
            return masterMixer.GetFloat(exposedName, out var volume) ? Mathf.InverseLerp(-80.0f, 0.0f, volume) : 0f;
        }
        
        private void LogValues()
        {
            Debug.LogFormat("Music Volume: {0}", GetVolume("MusicVolume"));
            Debug.LogFormat("SFX Volume: {0}", GetVolume("SFXVolume"));
            Debug.LogFormat("Master Volume: {0}", GetVolume("MasterVolume"));
        }
    }
}