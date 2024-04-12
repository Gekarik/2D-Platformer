using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HelathView_Bar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private bool _smoothChanging;
    [SerializeField] private float _speedOfChanging = 0.5f;

    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = _health.Max;
    }

    private void OnEnable()
    {
        _health.Changed += TakeDamage;
    }

    private void OnDisable()
    {
        _health.Changed -= TakeDamage;
    }

    private void TakeDamage()
    {
        if (_smoothChanging)
            StartCoroutine(nameof(ChangeValueSmoothly));
        else
            ChangeValue();
    }

    private void ChangeValue()
    {
        if (_slider.value != _health.Current)
            _slider.value = _health.Current;
    }

    private IEnumerator ChangeValueSmoothly()
    {
        while (_slider.value != _health.Current)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _health.Current, _speedOfChanging);
            yield return null;
        }
    }
}
