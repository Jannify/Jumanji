using UnityEngine;
using System.Collections;
using BrunoMikoski.TextJuicer;
using TMPro;

public class JumanjiText : MonoBehaviour
{
    private static JumanjiText Main;

    [SerializeField]
    private readonly JuicedTextMeshPro textManager;
    [SerializeField]
    private readonly TextMeshProUGUI textField;

    private void Awake()
    {
        Main = this;
    }

    private void Start()
    {
       // setText(CharFileData.originalText[Random.Range(0, CharFileData.keyText.Count + 1)]);
    }

    public static void setText(string text)
    {
        Main.textField.text = text;
        Main.textManager.Play();
    }
}
