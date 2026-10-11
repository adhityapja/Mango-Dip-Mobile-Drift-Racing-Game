
using UnityEngine;

public class HelpScreenManager : MonoBehaviour
{
    public GameObject howToPlayPanel;

    public void OpenHelp()
    {
        howToPlayPanel.SetActive(true);
    }

    public void CloseHelp()
    {
        howToPlayPanel.SetActive(false);
    }
}
