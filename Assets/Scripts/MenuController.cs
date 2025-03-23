using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    private UIDocument menu;

    private Button botonJugar;
    private Button botonAyuda;
    private Button botonCreditos;


    void OnEnable()
    {
        menu = GetComponent<UIDocument>();
        var root = menu.rootVisualElement;

        botonJugar = root.Q<Button>("Jugar");
        botonAyuda = root.Q<Button>("Ayuda");
        botonCreditos = root.Q<Button>("Creditos");

        botonJugar.RegisterCallback<ClickEvent, String>(Jugar, "EscenaMapa");
        botonAyuda.RegisterCallback<ClickEvent, String>(Jugar, "EscenaAyuda");
        botonCreditos.RegisterCallback<ClickEvent, String>(Jugar, "EscenaCreditos");

    }
    private void Jugar(ClickEvent evt, String nombreEscena)
    {
        print("Click en botón A");
        SceneManager.LoadScene(nombreEscena);
    }
}