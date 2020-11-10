namespace TowerDefense.Singletons.Shots
{
    public class PlayerShotProvider : ShotProvider
    {
        public static ShotProvider instance;

        private void Awake()
        {
            instance = this;
        }
    }
}