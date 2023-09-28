using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float totalTime;
        [SerializeField] float timeLeft;
        [SerializeField] bool timerOn=false;
        [SerializeField] TextMeshProUGUI timerText;
        [SerializeField] private Image image;
        TurnView view;



        public void StartTime(float time, TurnView view)
        {
            totalTime = time;
            timeLeft= time;
            timerOn= true;
            this.view = view;
        }

        void Update()
        {
            if (!timerOn) return;

            timeLeft -= Time.deltaTime;
            timerText.text = timeLeft.ToString("f0");
            image.fillAmount = timeLeft / totalTime;

            if (timeLeft <= 0)
            {
                timeLeft = 0;
                view.EndTurn();
            }

        }

        public float GetTimeLeft()
        {
            return timeLeft;
        }

        public void StopTime()
        {
            timerOn = false;
        }

   
    }
}