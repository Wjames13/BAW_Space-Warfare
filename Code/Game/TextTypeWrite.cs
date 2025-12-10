using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class TextTypeWrite : MonoBehaviour
{
    public TMP_Text textUI;
    public float typingSpeed = 0.04f;

    [TextArea(2, 4)]
    public string[] dialogueLines;

    private int index = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        textUI.text = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            
            if (dialogueLines == null || dialogueLines.Length == 0)
                return;

            if (isTyping)
            {
                SkipTyping();
            }
            else
            {
                NextLine();
            }
        }
    }

    
    public void StartDialogue()
    {
        
        if (dialogueLines == null || dialogueLines.Length == 0)
        {
            Debug.LogError("Dialogue Lines array is EMPTY!");
            return;
        }

        index = 0;
        textUI.text = "";

        StartTyping();
    }

    void StartTyping()
    {
        
        if (index < 0 || index >= dialogueLines.Length)
            return;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        textUI.text = "";

        string currentLine = dialogueLines[index];

        foreach (char letter in currentLine)
        {
            textUI.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void SkipTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        
        if (index >= 0 && index < dialogueLines.Length)
            textUI.text = dialogueLines[index];

        isTyping = false;
    }

    void NextLine()
    {
        index++;

        if (index < dialogueLines.Length)
        {
            StartTyping();
        }
        else
        {
            EndCutscene();
        }
    }

    void EndCutscene()
    {
        Debug.Log("Cutscene Finished ? Start Game");

        SceneManager.LoadScene("GameScene");
    }

}
