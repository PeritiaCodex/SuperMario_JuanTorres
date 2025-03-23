using UnityEngine;
using UnityEngine.UIElements;

public class InfiniteCreditsScroller : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private float scrollSpeed = 30f; // Velocidad de scroll

    private VisualElement rootCredits;
    private VisualElement creditsContainer;

    private float rootHeight;
    private float containerHeight;

    private void OnEnable()
    {
        if (uiDocument != null)
        {
            var root = uiDocument.rootVisualElement;
            // Busca por "name" (ID) en el UXML
            rootCredits = root.Q<VisualElement>("RootCredits");
            creditsContainer = root.Q<VisualElement>("CreditsContainer");
        }

        // Cuando el layout esté calculado, mediremos la altura
        if (rootCredits != null)
            rootCredits.RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
    }

    private void OnGeometryChanged(GeometryChangedEvent evt)
    {
        if (rootCredits == null || creditsContainer == null)
            return;

        // Medimos el alto total del root (pantalla) y del contenedor (créditos)
        rootHeight = rootCredits.layout.height;
        // contentRect.height suele reflejar mejor el tamaño real, 
        // en caso de offsets o si el contenedor es más grande que su layout aparente.
        containerHeight = creditsContainer.contentRect.height;

        // Como deseamos que inicie “debajo” de la pantalla,
        // posicionamos la parte superior del contenedor a la altura del root.
        var pos = creditsContainer.transform.position;
        pos.y = rootHeight;
        creditsContainer.transform.position = pos;
    }

    private void Update()
    {
        if (creditsContainer == null) return;

        // Tomamos la posición actual
        var currentPosition = creditsContainer.transform.position;

        // Movemos hacia arriba
        currentPosition.y -= scrollSpeed * Time.deltaTime;
        creditsContainer.transform.position = currentPosition;

        // Revisamos si la parte inferior del contenedor ya quedó por encima de la parte superior del root
        // es decir, containerBottom < 0 (o currentPosition.y + containerHeight < 0)
        if ((currentPosition.y + containerHeight) < 0f)
        {
            // Reiniciamos para que aparezca de nuevo “debajo” del root
            currentPosition.y = rootHeight;
            creditsContainer.transform.position = currentPosition;
        }
    }
}
