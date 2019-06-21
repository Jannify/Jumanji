using UnityEngine;

public class LockController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] lockModePrefabs;

    private GameObject currentMode;

    private void Start()
    {
        changeMode(false);
    }

    public void changeMode(bool numbers)
    {
        int i = numbers ? 1 : 0;
        Destroy(currentMode);
        currentMode = Instantiate(lockModePrefabs[i], transform);
    }
}
