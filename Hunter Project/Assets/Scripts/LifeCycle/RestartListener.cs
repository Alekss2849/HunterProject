using UnityEngine;
using UnityEngine.SceneManagement;

namespace HunterProject.LifeCycle
{
    public class RestartListener : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}