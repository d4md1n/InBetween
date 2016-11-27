using UnityEngine;

namespace Assets.Scripts
{
    public class VinesCollider : MonoBehaviour {

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.tag == "RollingStone")
            {
                Destroy(gameObject);
            }

        }
    }
}
