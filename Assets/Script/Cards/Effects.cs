using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    [SerializeField] private float _windForce = 10f;
    [SerializeField] private float _earthquakeForce = 5f;
    [SerializeField] private float _effectDuration = 3f;

    //[SerializeField] private ParticleSystem _windParticles;
    //[SerializeField] private ParticleSystem _earthquakeParticles;

    //[SerializeField] private AudioClip _windSound;
    //[SerializeField] private AudioClip _earthquakeSound;
    //private AudioSource _audioSource;

    private void Awake()
    {
        //_audioSource = GetComponent<AudioSource>();
    }

    public void TriggerWind()
    {
        StartCoroutine(WindEffect());
    }

    public void TriggerEarthquake()
    {
        StartCoroutine(EarthquakeEffect());
    }

    private IEnumerator WindEffect()
    {
        //if (_windParticles != null) _windParticles.Play();
        //if (_audioSource != null && _windSound != null) _audioSource.PlayOneShot(_windSound);

        float elapsedTime = 0f;

        while (elapsedTime < _effectDuration)
        {
            GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

            foreach (GameObject block in blocks)
            {
                if (block.TryGetComponent(out Rigidbody rb))
                {
                    // Синусоидальный ветер для более реалистичного эффекта
                    float windStrength = _windForce * Mathf.Sin(Time.time * 2f);
                    Vector3 windDirection = Vector3.right * windStrength;
                    rb.AddForce(windDirection, ForceMode.Force);
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //if (_windParticles != null) _windParticles.Stop();
    }

    private IEnumerator EarthquakeEffect()
    {
        //if (_earthquakeParticles != null) _earthquakeParticles.Play();
        //if (_audioSource != null && _earthquakeSound != null) _audioSource.PlayOneShot(_earthquakeSound);

        float elapsedTime = 0f;

        while (elapsedTime < _effectDuration)
        {
            GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

            foreach (GameObject block in blocks)
            {
                if (block.TryGetComponent(out Rigidbody rb))
                {
                    Vector3 shakeDirection = new Vector3(
                        Random.Range(-1f, 1f),
                        0,
                        Random.Range(-1f, 1f)
                    ).normalized;

                    rb.AddForce(shakeDirection * _earthquakeForce, ForceMode.Impulse);
                }
            }

            elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(0.5f);
        }

        //if (_earthquakeParticles != null) _earthquakeParticles.Stop();
    }
}
