using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class LoadLevelOne : MonoBehaviour {

        public void QuitApplication()
        {
            Application.Quit();
        }
        public void PlayLevelOne()
        {
            SceneManager.LoadScene("demo");
        }
    }
}
