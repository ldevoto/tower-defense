namespace TowerDefense.Singletons.Shots
{
    public class SniperShotProvider : ShotProvider
    {
        public static ShotProvider instance;

        private void Awake()
        {
            instance = this;
        }
    }
}