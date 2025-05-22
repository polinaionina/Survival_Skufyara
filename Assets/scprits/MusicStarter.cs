using UnityEngine;

public class MusicStarter : MonoBehaviour
{
    void Start()
    {
        MusicManager.Instance.PlayMainMenuMusic(); 
    }
}
