using UnityEngine;

namespace TowerDefense.Controllers
{
    public class DamageDealerController : MonoBehaviour
    {
        public void Damage(AliveEntityController aliveEntityController)
        {
            aliveEntityController.Damage(10f);
        }
    }
}