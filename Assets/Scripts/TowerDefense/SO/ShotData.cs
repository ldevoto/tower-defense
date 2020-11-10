using System;
using TowerDefense.Controllers;
using TowerDefense.Singletons.Shots;
using TowerDefense.SO.Behaviour;
using UnityEngine;

namespace TowerDefense.SO
{
    [CreateAssetMenu(fileName = "ShotData", menuName = "SO/Entity/ShotData", order = 6)]
    public class ShotData : ScriptableObject
    {
        public ShotController shotPrefab = null;
        public float damage = 10f;
        public float impulse = 20f;
        public ShotBehaviour shotBehaviour = null;
        public string[] targets = null;
        public Vector3 size = Vector3.one;
        public ShotType shotType = ShotType.Player;

        public void ShotFrom(Transform shotOrigin)
        {
            GetShotController().ShotWith(this, shotOrigin);
        }
        
        private ShotController GetShotController()
        {
            switch (shotType)
            {
                case ShotType.Player:
                    return PlayerShotProvider.instance.GetOne();
                case ShotType.FastTower:
                    return FastShotProvider.instance.GetOne();
                case ShotType.AoETower:
                    return AoEShotProvider.instance.GetOne();
                case ShotType.SniperTower:
                    return SniperShotProvider.instance.GetOne();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [Serializable]
        public enum ShotType
        {
            Player,
            FastTower,
            AoETower,
            SniperTower
        }
    }
}