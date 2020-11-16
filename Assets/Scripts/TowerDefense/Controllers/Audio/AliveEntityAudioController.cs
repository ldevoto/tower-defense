using UnityEngine;

namespace TowerDefense.Controllers.Audio
{
    public class AliveEntityAudioController : SfxAudioController
    {
        [SerializeField] private AudioClip[] hitSounds = null;
        [SerializeField] private AudioClip[] deadSounds = null;
        
        private AliveEntityController _aliveEntityController = null;

        private void Awake()
        {
            _aliveEntityController = GetComponent<AliveEntityController>();
        }

        private void OnEnable()
        {
            _aliveEntityController.OnHit += HandleHit;
            _aliveEntityController.OnKill += HandleKill;
        }
        
        private void OnDisable()
        {
            _aliveEntityController.OnHit -= HandleHit;
            _aliveEntityController.OnKill -= HandleKill;
        }

        private void HandleHit(float _)
        {
            PlayRandomSfxWithRandomPitch(hitSounds);
        }

        private void HandleKill()
        {
            PlayRandomSfx(deadSounds);
        }
    }
}