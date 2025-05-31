using UnityEngine;

public class ClickToQuit : MonoBehaviour
{
    private void OnMouseDown()
    {
        QuitGame();
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}