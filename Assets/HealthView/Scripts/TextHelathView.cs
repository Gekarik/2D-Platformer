using System.Collections;
using TMPro;
using UnityEngine;

public class TextHelathView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthField;
    [SerializeField] private Health _health;
    [SerializeField] private float _smoothDecreaseDuration = 0.5f;

    private void Start()
    {
        _healthField.text = _health.Max.ToString("");
    }

    private void OnEnable()
    {
        _health.Changed += ChangeHealth;
    }

    private void OnDisable()
    {
        _health.Changed -= ChangeHealth;
    }

    public void ChangeHealth()
    {
        StartCoroutine(nameof(TextValueUpdate));
    }

    private IEnumerator TextValueUpdate()
    {
        var healthPoint = float.Parse(_healthField.text);
        var currentHealthPoint = _health.Current;
        float lerpTime = 0.0f;

        while (lerpTime < _smoothDecreaseDuration)
        {
            lerpTime += Time.deltaTime;
            float lerpFactor = lerpTime / _smoothDecreaseDuration;
            healthPoint = Mathf.Lerp(healthPoint, currentHealthPoint, lerpFactor);
            _healthField.text = healthPoint.ToString("0");
            yield return null;
        }
    }
}
