using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    public GameObject RotationSlider;
    public GameObject ScaleSlider;
    public GameObject CaliibrationButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableSliders()
    {
        ScaleSlider.SetActive(false);
        RotationSlider.SetActive(false);
    }

    public void DisableCalibrationButton()
    {
        CaliibrationButton.SetActive(false);
        DisableSliders();
    }
}
