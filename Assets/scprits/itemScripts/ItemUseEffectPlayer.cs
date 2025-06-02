using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemUseEffectPlayer : MonoBehaviour
{
    public static ItemUseEffectPlayer Instance;

    public Canvas effectCanvas;
    public Image effectImage;
    public float frameRate = 0.05f;
    public CanvasGroup effectCanvasGroup; 
    
    private void Awake()
    {
        Instance = this;
        effectCanvas.sortingOrder = 0;
    }

    public void PlayEffect(Sprite[] frames, float alpha = 1f)
    {
        if (effectCanvasGroup != null)
            effectCanvasGroup.alpha = alpha;

        StartCoroutine(Play(frames));
    }


    private IEnumerator Play(Sprite[] animationFrames)
    {
        effectCanvas.sortingOrder = 100;
        
        for (var i = 0; i < animationFrames.Length; i++)
        {
            effectImage.sprite = animationFrames[i];
            yield return new WaitForSeconds(frameRate);
        }
        Debug.Log("эффект вызван");
        effectCanvas.sortingOrder = 0;
    }
}
