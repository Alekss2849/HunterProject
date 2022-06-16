using UnityEngine;

namespace HunterProject.LevelElements
{
    public class LevelBorder : MonoBehaviour
    {
        private const string _BORDER_TAG_ = "Border";

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (false == other.gameObject.CompareTag(_BORDER_TAG_))
            {
                Destroy(other.gameObject);
            }
        }
    }
}