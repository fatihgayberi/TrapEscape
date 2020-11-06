using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject[] controlPoint;
    [SerializeField] GameObject[] enemyObj;
    [SerializeField] Slider timeSlider;
    [SerializeField] GameObject button;
    [SerializeField] GameObject bombParticle;
    [SerializeField] GameObject panelLoser;
    public float maxValue;
    GameObject selectControlPoint;
    bool stayControlObj;
    int controlObjCounter;

    // Start is called before the first frame update
    void Start()
    {
        timeSlider.maxValue = maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        StayController();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            panelLoser.SetActive(true);
            
        }
        if (other.CompareTag("ControlPoint"))
        {
            stayControlObj = true;
            for (int i = 0; i < controlPoint.Length; i++)
            {
                if (other.gameObject == controlPoint[i])
                {
                    selectControlPoint = controlPoint[i];
                }
            }
        }
        if (other.CompareTag("ControlButton"))
        {
            Bomb();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ControlPoint"))
        {
            stayControlObj = false;
            timeSlider.value = 0;
        }
    }

    void StayController()
    {
        if (stayControlObj)
        {
            timeSlider.value += Time.deltaTime;

            if (timeSlider.value >= timeSlider.maxValue)
            {
                selectControlPoint.transform.GetChild(0).GetComponent<Light>().color = Color.green;
                selectControlPoint.GetComponent<Collider>().enabled = false;
                timeSlider.value = 0;
                stayControlObj = false;
                controlObjCounter++;
                if (controlObjCounter == controlPoint.Length)
                {
                    Finish();
                }
            }
        }
    }

    void Finish()
    {
        button.GetComponent<Collider>().enabled = true;
        button.GetComponent<Light>().color = Color.white;
    }

    void Bomb()
    {
        for (int i = 0; i < controlPoint.Length; i++)
        {
            Instantiate(bombParticle, controlPoint[i].transform.position, Quaternion.identity);
        }
        for (int i = 0; i < enemyObj.Length; i++)
        {
            enemyObj[i].GetComponent<EnemyAI>().enabled = false;
        }
        button.GetComponent<Collider>().enabled = false;
    }
}
