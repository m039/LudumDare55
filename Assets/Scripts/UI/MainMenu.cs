using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD55
{
    public class MainMenu : MonoBehaviour
    {
        public void OnStartGameClicked()
        {
            SceneManager.LoadScene(Consts.Level1Scene);
        }
    }
}
