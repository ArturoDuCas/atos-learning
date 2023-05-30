using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTimeController : MonoBehaviour
{

    Slider slider;
    CountDown countDownComponent;

    private void Awake() {
        slider = GetComponent<Slider>();
        countDownComponent = GameObject.Find("CountDown").GetComponent<CountDown>(); 
        slider.maxValue = countDownComponent.countdownTime; 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = countDownComponent.currentTime;
    }
}
