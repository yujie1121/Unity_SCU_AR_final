using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("�u��C��");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
