using System;
using UnityEngine;
using UnityEngine.UI;

public class PropUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] Prop prop;

    private void OnEnable()
    {
        prop.OnPropInteracted += Prop_OnPropInteracted;
    }

    private void OnDisable()
    {
        prop.OnPropInteracted -= Prop_OnPropInteracted;
    }

    private void Prop_OnPropInteracted()
    {
        slider.value = prop.TimeCleaned / prop.MaxCleanTime;
        print(slider.value);
    }
}
