//using BrunoMikoski.TextJuicer;
using TMPro;
using UnityEngine;

public class JumanjiText : MonoBehaviour
{
    public static JumanjiText Main;

    //[SerializeField]
    //public JuicedTextMeshPro textManager;
    [SerializeField]
    public TextMeshProUGUI textField;

    private void Awake()
    {
        Main = this;
    }

    private void Start()
    {
        // setText(CharFileData.originalText[Random.Range(0, CharFileData.keyText.Count + 1)]);
    }

    public void setText(string text)
    {
        Main.textField.SetText(text);
        //Main.textManager.SetDirty();
    }

    public void play()
    {
        //Main.textManager.Play();
    }
}
