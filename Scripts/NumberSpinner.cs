using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class NumberSpinner : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI buttonText;

    [Header("Animation Settings")]
    [SerializeField] private float initialSpinDuration = 0.05f;
    [SerializeField] private float finalSpinDuration = 0.5f;
    [SerializeField] private float totalSpinTime = 3f;
    [SerializeField] private AnimationCurve speedCurve;

    private bool isSpinning = false;
    private int currentNumber = 1;

    private void Start()
    {
        // Configurar el botón
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartSpinning);
        }

        // Mostrar número inicial
        UpdateNumberDisplay(1);
    }

    public void StartSpinning()
    {
        if (!isSpinning)
        {
            isSpinning = true;
            startButton.interactable = false;
            if (buttonText != null)
            {
                buttonText.text = "GIRANDO...";
            }
            StartCoroutine(SpinNumbers());
        }
    }

    private IEnumerator SpinNumbers()
    {
        float elapsedTime = 0f;
        float currentSpinDuration = initialSpinDuration;

        // Número final aleatorio
        int finalNumber = Random.Range(1, 11);

        while (elapsedTime < totalSpinTime)
        {
            // Calcular la duración actual del giro basada en el tiempo transcurrido
            float normalizedTime = elapsedTime / totalSpinTime;
            currentSpinDuration = Mathf.Lerp(initialSpinDuration, finalSpinDuration, speedCurve.Evaluate(normalizedTime));

            // Generar y mostrar un número aleatorio
            currentNumber = Random.Range(1, 11);
            UpdateNumberDisplay(currentNumber);

            yield return new WaitForSeconds(currentSpinDuration);
            elapsedTime += currentSpinDuration;
        }

        // Mostrar el número final
        UpdateNumberDisplay(finalNumber);

        // Resetear el estado
        isSpinning = false;
        startButton.interactable = true;
        if (buttonText != null)
        {
            buttonText.text = "COMENZAR";
        }
    }

    private void UpdateNumberDisplay(int number)
    {
        if (numberText != null)
        {
            numberText.text = number.ToString();
            // Efecto de escala
            numberText.transform.localScale = Vector3.one * 1.2f;
            LeanTween.scale(numberText.gameObject, Vector3.one, 0.2f).setEase(LeanTweenType.easeOutBack);
        }
    }
} 