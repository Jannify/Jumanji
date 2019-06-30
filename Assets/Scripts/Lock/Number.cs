using TMPro;
using UnityEngine;

public class Number : MonoBehaviour
{
    public string number;
    public TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetNumber(string value)
    {
        number = value;
        textMeshProUGUI.text = value;
    }
}
