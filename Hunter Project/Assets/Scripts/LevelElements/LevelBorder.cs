using UnityEngine;

namespace HunterProject.LevelElements
{
    public class LevelBorder : MonoBehaviour
    {
        private const string _BOUND_TAG_ = "Bound";

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (false == collision.gameObject.CompareTag(_BOUND_TAG_))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}