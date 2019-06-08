using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MakaoGame
{
    /// <summary>
    /// Klasa odpowidająca za przejścia między scenami gry.
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
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
