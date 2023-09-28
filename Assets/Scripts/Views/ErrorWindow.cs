using UnityEngine;
using TMPro;

public class ErrorWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI message;

    public void SetMessage(string message)
    {
        this.message.text = message;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
