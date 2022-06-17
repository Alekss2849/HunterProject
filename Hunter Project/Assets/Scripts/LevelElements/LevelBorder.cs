using HunterProject.Data;
using UnityEngine;

namespace HunterProject.LevelElements
{
    public class LevelBorder : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (false == other.gameObject.CompareTag(Idents.BORDER_TAG))
            {
                Destroy(other.gameObject);
            }
        }
    }
}