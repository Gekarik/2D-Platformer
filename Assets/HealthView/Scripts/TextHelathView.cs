using System.Collections;
using TMPro;
using UnityEngine;

public class TextHelathView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthField;
    [SerializeField] private Health _health;
    [SerializeField] private float _changeSpeed = 100.0f;

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

        while (healthPoint != currentHealthPoint)
        {
            healthPoint = Mathf.MoveTowards(healthPoint, currentHealthPoint, _changeSpeed * Time.deltaTime);
            _healthField.text = healthPoint.ToString("");
            yield return null;
        }
    }
}
