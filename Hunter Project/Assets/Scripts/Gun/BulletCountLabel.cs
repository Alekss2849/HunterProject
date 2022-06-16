using TMPro;
using UnityEngine;

namespace HunterProject.Gun
{
    public class BulletCountLabel : MonoBehaviour
    {
        private TMP_Text _label;

        private void Awake()
        {
            _label = GetComponent<TMP_Text>();
        }

        public void SetBulletCount(int count)
        {
            _label.text = $"Bullets: {count}";
        }
    }
}