using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class RegresoController : MonoBehaviour
{
    private UIDocument uiRegreso;
    private Button botonRegreso;
    void OnEnable()
    {
        uiRegreso = GetComponent<UIDocument>();
        var root = uiRegreso.rootVisualElement;
        botonRegreso = root.Q<Button>("BotonRegreso");
        botonRegreso.RegisterCallback<ClickEvent>(Regresar);
    }
    
    private void Regresar(ClickEvent evt)
    {
        SceneManager.LoadScene("EscenaMenu");
    }

}
