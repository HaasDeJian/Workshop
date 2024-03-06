using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI healthText;

    [SerializeField]
    private float maximumInjuredLayerWeight;

    private const float MaximumHealth = 100;
    private float _currentHealth;

    private Animator _animator;
    private int _injuredLayerIndex;
    private float _layerWeightVelocity;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = MaximumHealth;
        _animator = GetComponent<Animator>();
        _injuredLayerIndex = _animator.GetLayerIndex("Injured");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentHealth -= MaximumHealth / 10;

            if (_currentHealth < 0)
            {
                _currentHealth = MaximumHealth;
            }
        }

        var healthPercentage = _currentHealth / MaximumHealth;
        healthText.text = $"Health: {healthPercentage * 100}%";

        var currentInjuredLayerWeight = _animator.GetLayerWeight(_injuredLayerIndex);
        var targetInjuredLayerWeight = (1 - healthPercentage) * maximumInjuredLayerWeight;
        
        _animator.SetLayerWeight(_injuredLayerIndex,
            Mathf.SmoothDamp(currentInjuredLayerWeight,
                targetInjuredLayerWeight,
                ref _layerWeightVelocity, 
                0.2f));
    }
}
