using UnityEngine;
using UnityEngine.SceneManagement;
 
 
public class Controller : MonoBehaviour
{
    // Method handle playbutton click
    public void PlayBtnClicked()
    {
        //Start the Game
        SceneManager.LoadScene("Game");
    } 
}