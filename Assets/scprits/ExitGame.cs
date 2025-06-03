using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void Exit()
    {
        Debug.Log("Выход из игры...");
        Application.Quit();
    }
}