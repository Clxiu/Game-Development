using UnityEngine;
using TMPro;
using System.Collections;

public class DisappearText : MonoBehaviour
{
    public TMP_Text textMeshPro;

    private void Start()
    {
        StartCoroutine(HideTextAfterDelay(5f)); // 5 seconds delay
    }

    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        textMeshPro.gameObject.SetActive(false);
    }
}
