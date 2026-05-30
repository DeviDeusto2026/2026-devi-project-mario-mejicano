using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public CanvasGroup menuCanvas;
    [SerializeField] private GameObject botonSalir;
    [SerializeField] private GameObject botonContinuar;

    void Start()
    {
        botonSalir.GetComponent<Button>().onClick.AddListener(() => Application.Quit());
        botonContinuar.GetComponent<Button>().onClick.AddListener(() => prepareUI());

        SetMenu(false); // estado inicial limpio
        Time.timeScale = 1;
    }

    void SetMenu(bool visible)
    {
        menuCanvas.alpha = visible ? 1 : 0;
        menuCanvas.blocksRaycasts = visible;
        menuCanvas.interactable = visible; // ? esto faltaba
        Time.timeScale = visible ? 0 : 1;
    }

    void prepareUI()
    {
        bool estaVisible = menuCanvas.alpha == 1;
        SetMenu(!estaVisible);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            prepareUI();
        }
    }
}