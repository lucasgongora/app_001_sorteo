using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    RectTransform Panel;
    Rect LastSafeArea = new Rect(0, 0, 0, 0);
    Vector2Int LastScreenSize = new Vector2Int(0, 0);
    ScreenOrientation LastOrientation = ScreenOrientation.AutoRotation;

    void Awake()
    {
        Panel = GetComponent<RectTransform>();

        if (Panel == null)
        {
            Debug.LogError("Cannot find RectTransform component");
            return;
        }

        Refresh();
    }

    void Update()
    {
        Refresh();
    }

    void Refresh()
    {
        Rect safeArea = Screen.safeArea;
        Vector2Int screenSize = new Vector2Int(Screen.width, Screen.height);
        ScreenOrientation orientation = Screen.orientation;

        // Si nada ha cambiado, no es necesario actualizar
        if (safeArea == LastSafeArea &&
            screenSize == LastScreenSize &&
            orientation == LastOrientation)
            return;

        // Actualizar nuestro seguimiento de valores
        LastSafeArea = safeArea;
        LastScreenSize = screenSize;
        LastOrientation = orientation;

        // Convertir las coordenadas del área segura a valores relativos al canvas
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= screenSize.x;
        anchorMin.y /= screenSize.y;
        anchorMax.x /= screenSize.x;
        anchorMax.y /= screenSize.y;

        // Aplicar los anclajes
        Panel.anchorMin = anchorMin;
        Panel.anchorMax = anchorMax;

        // Debug log para ver los valores
        Debug.Log($"Safe Area: {safeArea}");
        Debug.Log($"Anchors: {anchorMin} -> {anchorMax}");
    }
}
