using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MakaoGame
{
    public class SceneLoader : MonoBehaviour
    {
        public static void Load(string name)
        {
            SceneManager.LoadScene(name);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void Game()
        {
            SceneManager.LoadScene("GameView");
        }

        public void Win()
        {
            SceneManager.LoadScene("Win");
        }

        public void Lose()
        {
            SceneManager.LoadScene("Lose");
        }

        public void Rules()
        {
            SceneManager.LoadScene("Rules");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
