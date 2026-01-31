using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBehaviour : MonoBehaviour
{
    public Image[] hearts;
    private HealthComponent healthComponent;
    
    void Start()
    {
        healthComponent = Player.Instance.transform.GetComponent<HealthComponent>();
        healthComponent.OnHealthChanged += UpdateHearts;
        UpdateHearts(healthComponent.CurrentHP);
    }
    void OnDisable()
    {
        healthComponent.OnHealthChanged -= UpdateHearts;
    }

    void UpdateHearts(int currentHealth, bool _ = false)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            // Solo los corazones que estén dentro de la vida actual se muestran
            hearts[i].gameObject.SetActive(i < currentHealth-1);
        }
    }
}
