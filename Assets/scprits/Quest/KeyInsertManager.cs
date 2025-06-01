using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class KeyInsertManager : MonoBehaviour
{
    public Canvas insertCanvas;
    public CanvasGroup part1Group;
    public CanvasGroup part2Group;
    public Transform part1UI;
    public Transform part2UI;
    
    public TMP_Text instructionText;
    public TMP_Text statusText;
    public GameObject objectToEnable;
    private bool playerInTrigger = false;
    private bool active = false;

    private int currentPart = 0; 
    private float part1Rotation = 0f;
    private float part2Rotation = 0f;

    private const float correctRotation1 = 0f;
    private const float correctRotation2 = 0f;

    void Update()
    {   
        if (playerInTrigger && Input.GetKeyDown(KeyCode.F))
        {   
            if (active)
            {
                ShowCanvas(false); 
                return;
            }
            else
            {
                ShowCanvas(true);
                if (currentPart == 0){
                    if (InventoryUI.Instance.HasTwoKeyParts()) 
                    {
                        instructionText.text = "Вращай осколок на R, попробуй вставить на E";
                        ShowPart(1);
                        currentPart = 1;
                    }
                    else
                    {
                        instructionText.text = "Кажется тебе чего-то не хватает, осмотри комнату";
                    }
                }
            }
        }

        if (!active) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateCurrentPart();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInsertPart();
        }
    }

    void RotateCurrentPart()
    {
        if (currentPart == 1)
        {
            part1Rotation = (part1Rotation + 90f) % 360f;
            part1UI.rotation = Quaternion.Euler(0, 0, part1Rotation);
        }
        else if (currentPart == 2)
        {
            part2Rotation = (part2Rotation + 90f) % 360f;
            part2UI.rotation = Quaternion.Euler(0, 0, part2Rotation);
        }
    }

    void TryInsertPart()
    {
        if (currentPart == 1)
        {
            if (Mathf.Approximately(part1Rotation % 360f, correctRotation1))
            {
                statusText.text = "Правильно!";
                ShowPart(2);
                currentPart = 2;
            }
            else
            {
                statusText.text = "Мимо";
            }
        }
        else if (currentPart == 2)
        {
            if (Mathf.Approximately(part2Rotation % 360f, correctRotation2))
            {
                statusText.text = "Успешно";
                InventoryUI.Instance.ClearInventory();

                if (objectToEnable != null)
                {
                    objectToEnable.SetActive(true);
                }

                ShowCanvas(false);

                gameObject.SetActive(false);
            }
            else
            {
                statusText.text = "Мимо";
            }
        }
    }

    void ShowPart(int part)
    {
        int[] angles = { 90, 180, 270 };
        int randomAngle = angles[Random.Range(0, angles.Length)];

        if (part == 1)
        {
            part1Group.alpha = 1f;
            part1Rotation = randomAngle;
            part1UI.rotation = Quaternion.Euler(0, 0, part1Rotation);
        }
        else if (part == 2)
        {
            part2Group.alpha = 1f;
            part2Rotation = randomAngle;
            part2UI.rotation = Quaternion.Euler(0, 0, part2Rotation);
        }
    }


    void ShowCanvas(bool show)
    {
        insertCanvas.sortingOrder = show ? 100 : 0;
        active = show;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            ShowCanvas(false);
        }
    }
}
