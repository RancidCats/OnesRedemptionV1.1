using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeBar : MonoBehaviour
{
    public Slider slider;
    public float life;
    public float Life;

    private void Awake()
    {
        slider.GetComponent<Slider>();

    }
    private void Start()
    {

    }
    private void Update()
    {
        Life = Player.instance.maxLife;
        life = Player.instance.health;
        slider.value = life / Life;
    }
}
