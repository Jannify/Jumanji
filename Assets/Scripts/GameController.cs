using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Main;
    [SerializeField]
    private GameObject modes;
    [SerializeField]
    private GameObject[] modePrefabs;
    public GameMode selectetMode;
    private GameObject selectetModeObject;

    private void Awake()
    {
        Main = this;
    }

    void Start()
    {
        changeMode(GameMode.Menu);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTouch()
    {
        if (selectetMode == GameMode.Original)
        {
            JumanjiText.setText(CharFileData.originalText[Random.Range(0, CharFileData.keyText.Count + 1)]);
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
            Main.selectetMode = mode;
            Destroy(Main.selectetModeObject);
            Main.selectetModeObject = Instantiate(Main.modePrefabs[(int)mode], Main.modes.transform);
        }
    }

    public void setJumanjiTextDelayed(string text)
    {
        StartCoroutine(setJumanjiTextEnumerator(text));
    }

    private IEnumerator setJumanjiTextEnumerator(string text)
    {
        yield return new WaitForSeconds(1.0f);
        JumanjiText.setText(text);
    }
}

public enum GameMode
{
    Menu,
    Original,
    Password,
    PasswordText = 1,
    Network
}
