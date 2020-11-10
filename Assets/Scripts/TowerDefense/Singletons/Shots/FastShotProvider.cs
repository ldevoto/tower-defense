namespace TowerDefense.Singletons.Shots
{
    public class FastShotProvider : ShotProvider
    {
        public static ShotProvider instance;

        private void Awake()
        {
            instance = this;
        }
    }
}