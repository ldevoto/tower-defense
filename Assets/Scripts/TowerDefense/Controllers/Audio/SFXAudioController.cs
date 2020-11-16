using UnityEngine;

namespace TowerDefense.Controllers.Audio
{
    public class SfxAudioController : MonoBehaviour
    {
        [SerializeField] private float pitch = 1f;
        [SerializeField] private float pitchVariation = 0.1f;
        [SerializeField] private float volume = 1f;
        

        public void PlaySFX(AudioClip audioClip)
        {
            PlayAudioClip(audioClip, volume, pitch);
        }

        public void PlayRandomSfx(AudioClip[] audioClips)
        {
            var audioClip = audioClips[Random.Range(0, audioClips.Length)];
            PlayAudioClip(audioClip, volume, pitch);
        }

        public void PlayRandomSfxWithRandomPitch(AudioClip[] audioClips)
        {
            var audioClip = audioClips[Random.Range(0, audioClips.Length)];
            var customPitch = pitch * Random.Range(pitch - pitchVariation, pitch + pitchVariation);
            PlayAudioClip(audioClip, volume, customPitch);
        }

        private static void PlayAudioClip(AudioClip audioClip, float volume, float pitch)
        {
            AudioController.instance.PlaySFX(audioClip, volume, pitch);
        }
    }
}