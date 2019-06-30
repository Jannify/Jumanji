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
    [SerializeField]
    public TMP_FontAsset fallBackFont;
    [SerializeField]
    public Material fallBackMaterial;

    private void Awake()
    {
        Main = this;
    }

    public void setText(string text)
    {
        if (text.Contains("§"))
        {
            textField.font = fallBackFont;
            textField.material = fallBackMaterial;
            text = text.Replace("§", "");
        }
        Main.textField.SetText(text.ToUpper());
    }

    public void play()
    {
        //Main.textManager.Play();
    }
}
