using UnityEngine;

public class ExitGame : MonoBehaviour // <--- Вот здесь!
{
    public void Exit()
    {
        Debug.Log("Выход из игры...");
        Application.Quit();
    }
}