using TowerDefense.SO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerDefense.Controllers.Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioMixerController audioMixerController = null;
        [SerializeField] private AudioSource musicSource = null;
        [SerializeField] private AudioSource[] sfxSources = null;
        
        public static AudioController instance = null;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            audioMixerController.Initialize();
        }

        public void PlayMusic(AudioClip audioClip, float volume = 1f)
        {
            musicSource.volume = volume;
            musicSource.clip = audioClip;
            musicSource.Play();
        }

        public void PlaySFX(AudioClip audioClip, float volume = 1f, float pitch = 1f)
        {
            var sfxSource = GetFirstFreeSFXSource();
            sfxSource.pitch = pitch;
            sfxSource.volume = volume;
            sfxSource.PlayOneShot(audioClip);
        }

        private AudioSource GetFirstFreeSFXSource()
        {
            foreach (var audioSource in sfxSources)
            {
                if (!audioSource.isPlaying)
                {
                    return audioSource;
                }
            }

            return sfxSources[Random.Range(0, sfxSources.Length)];
        }
    }
}