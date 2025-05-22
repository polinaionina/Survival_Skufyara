using UnityEngine;

public class AudioInspector : MonoBehaviour
{
    void Start()
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource source in sources)
        {
            string clipName = source.clip != null ? source.clip.name : "None";
            Debug.Log($"[AUDIO DEBUG] Object: {source.gameObject.name}, Clip: {clipName}, Playing: {source.isPlaying}");
        }
    }
}
