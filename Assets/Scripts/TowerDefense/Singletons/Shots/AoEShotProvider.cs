namespace TowerDefense.Singletons.Shots
{
    public class AoEShotProvider : ShotProvider
    {
        public static ShotProvider instance;

        private void Awake()
        {
            instance = this;
        }
    }
}