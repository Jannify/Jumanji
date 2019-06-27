using UnityEngine;

public abstract class baseLockMode : MonoBehaviour
{

    public virtual void setNextChar(string Char) { }

    public bool checkPassword(string password)
    {
        if (CharFileData.Main.keyText.ContainsKey(password))
        {
            GameController.Main.changeMode(GameMode.PasswordText);
            GameController.Main.setJumanjiText(CharFileData.Main.keyText[password]);
            return true;
        }
        else return false;
    }
}
