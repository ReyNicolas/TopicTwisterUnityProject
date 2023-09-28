using TMPro;
using UnityEngine;

namespace Views.HomePage
{
    public class MatchmakingView : MonoBehaviour
    {
        [SerializeField]private TextMeshProUGUI playerText;
        [SerializeField]private TextMeshProUGUI rivalText;
        public void SetPlayer(string playerID)
        {
            playerText.text = playerID;
        }
        public void SetRival(string rivalID)
        {
            rivalText.text = rivalID;
        }
    }
}