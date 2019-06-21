using UnityEngine;

public abstract class baseLockMode : MonoBehaviour
{

    public virtual void setNextChar(string Char) { }

    public void checkPassword(string password)
    {
        if (CharFileData.keyText.ContainsKey(password))
        {
            GameController.Main.changeMode(GameMode.PasswordText);
            GameController.Main.setJumanjiTextDelayed(CharFileData.keyText[password]);
        }
    }
}
