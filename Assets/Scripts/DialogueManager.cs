using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    private Queue<string> sentences;
    //public int counter;
    [TextArea(3, 10)]
    public string[] textToShow;
    [TextArea(3, 10)]
    public string portalPromt;
    public GameObject dialogueWindow;
    private bool isActive = false;
    private bool isTutorial = true;
    private bool btnC = false;
    private bool btnI = false;
    //public bool moreToSay = false;
    private InventoryUI inventoryUI;
    public GameObject librarianTrigger;
    private DialogueTrigger librarianDialogueTrigger;
    public GameObject LibrarianNPCTut;
    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
        sentences.Clear();
        foreach(string sentencesOption in textToShow)
        {
            sentences.Enqueue(sentencesOption);
        }
        //NextDialogue();
        if (isTutorial)
        {
            inventoryUI = FindObjectOfType<InventoryUI>();
            librarianDialogueTrigger = librarianTrigger.GetComponent<DialogueTrigger>();
        }
    }

    IEnumerator Typesentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            //yield return null;
            yield return new WaitForSeconds(0.05f);
        }

    }
    public void NextDialogue()
    {
        if(sentences.Count == 0)
        {
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(Typesentence(sentence));
    }
    public void PortalPromt()
    {
        dialogueWindow.SetActive(true);
        string sentence = portalPromt;
        StopAllCoroutines();
        StartCoroutine(Typesentence(sentence));
        GameManager.instance.player.canMove = false;
        isActive = true;

    }
    public void ShowDialogue()
    {
        
        dialogueWindow.SetActive(true);
        NextDialogue();
        GameManager.instance.player.canMove = false;
        isActive = true;
    }
    public void HideDialogue()
    {
        if(isActive && Input.GetKeyDown(KeyCode.E))
        {
            dialogueWindow.SetActive(false);
            GameManager.instance.player.canMove = true;
            isActive = false;
        }

    }
    private void InventoryTutorial()
    {
        if (GameManager.instance.player.canMove && inventoryUI != null)
        {
            if(inventoryUI.inventoryUI.activeSelf) //(Input.GetKeyDown(KeyCode.C))
            {
                btnC = true;
            }
            if(inventoryUI.characterUI.activeSelf)//(Input.GetKeyDown(KeyCode.I))
            {
                btnI = true;
            }
        }
    } 
    public void ContractTutorial()
    {
        ShowDialogue();
    }
    public void EquipmentTutorial()
    {
        //bool isTutorial = true;
        if (FindObjectOfType<Portal>().tutorialCompleted == false)
        {
            ItemPickup[] items = GameManager.instance.objectsToActivate[0].GetComponentsInChildren<ItemPickup>();
            int counter = items.Length;
            if(counter == 0)
            {
                ShowDialogue();
                FindObjectOfType<Portal>().tutorialCompleted = true;
            }
        }

        //Debug.Log(counter);
    }
    private void Update()
    {
        /*if (moreToSay)
        {
            if (Input.GetKeyDown(KeyCode.E))
                NextDialogue();
        }*/
        HideDialogue();
        if(isTutorial)
        {
            InventoryTutorial();
            if(btnC & btnI)
            {
                ShowDialogue();
                librarianDialogueTrigger.isActive = true;
                LibrarianNPCTut.SetActive(true);
                isTutorial = false;
            }
        }
        if (FindObjectOfType<Portal>().tutorialCompleted == false)
        {
            EquipmentTutorial();
        }

    }
}
