using UnityEngine;
using TMPro;
using System.Collections;

public class WarningUI : MonoBehaviour
{
    public TextMeshProUGUI warningText;

    void Start()
    {
        if (warningText != null)
            warningText.gameObject.SetActive(false); 
    }

    public void ShowWarning(string message)
    {
        if (warningText == null)
        {
            Debug.LogError("WarningUI: No warningText assigned!");
            return;
        }

        warningText.text = message;
        warningText.gameObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(HideAfterDelay());
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        warningText.gameObject.SetActive(false);
    }
}
