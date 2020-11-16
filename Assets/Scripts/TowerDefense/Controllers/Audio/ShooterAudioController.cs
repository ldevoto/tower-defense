using UnityEngine;

namespace TowerDefense.Controllers.Audio
{
    public class ShooterAudioController : SfxAudioController
    {
        [SerializeField] private AudioClip[] shotSounds = null;
        
        private ShooterController _shooterController = null;

        private void Awake()
        {
            _shooterController = GetComponent<ShooterController>();
        }

        private void OnEnable()
        {
            _shooterController.OnShot += HandleShot;
        }
        
        private void OnDisable()
        {
            _shooterController.OnShot -= HandleShot;
        }

        private void HandleShot()
        {
            PlayRandomSfxWithRandomPitch(shotSounds);
        }
    }
}