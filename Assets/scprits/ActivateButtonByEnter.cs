using UnityEngine;
using UnityEngine.UI;

public class ActivateButtonByEnter : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (button != null)
                button.onClick.Invoke(); 
        }
    }
}
