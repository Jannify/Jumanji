using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Main;
    [SerializeField]
    private GameObject modes = default;
    [SerializeField]
    private GameObject[] modePrefabs = default;
    private GameMode selectetMode;
    private GameObject selectetModeObject;

    public static bool passwordMode = true;

    private void Awake()
    {
        Main = this;
    }

    void Start()
    {
        changeMode(GameMode.Menu);
    }

    public void OnTouch()
    {
        if (selectetMode == GameMode.Text)
        {
            setJumanjiText(CharFileData.Main.originalText[Random.Range(0, CharFileData.Main.originalText.Count)]);
        }

        if (selectetMode == GameMode.PasswordText)
        {
            changeMode(GameMode.Password);
        }
    }

    public void changeMode(int mode) { changeMode((GameMode)mode); }
    public void changeMode(GameMode mode)
    {
        if ((int)mode < Main.modePrefabs.Length)
        {
            //Main.StopCoroutine("changeModeEnumerator");
            Main.StartCoroutine(changeModeEnumerator(mode));
        }
    }
    private IEnumerator changeModeEnumerator(GameMode mode)
    {
        CrossFader.crossFadeCanvasGroup(Main.modes, -1);
        yield return new WaitForSeconds(0.8f);
        Main.selectetMode = mode;
        Destroy(Main.selectetModeObject);
        Main.selectetModeObject = Instantiate(Main.modePrefabs[(int)mode], Main.modes.transform);
        CrossFader.crossFadeCanvasGroup(Main.modes, 1);
    }

    public void setJumanjiText(string text)
    {
        //JumanjiText.Main.textManager.Stop();
        //JumanjiText.Main.textManager.SetProgress(0);
        //JumanjiText.Main.textManager.SetVerticesDirty();
        //JumanjiText.Main.play();

        StartCoroutine(setJumanjiTextEnumerator(text));
    }

    private IEnumerator setJumanjiTextEnumerator(string text)
    {
        yield return new WaitForSeconds(0.9f);
        JumanjiText.Main.setText(text);
    }
}

public enum GameMode
{
    Menu,
    Text,
    Password,
    PasswordText,
    Network
}
