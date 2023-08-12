using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TextFadeEllipsize : MonoBehaviour
{
    private string[] phrases = { "El café es la segunda bebida que más se consume en nuestro planeta. La gente suele consumir agua. Débiles.", 
        "Donde más café se bebe es en los países nórdicos. España se encuentra en una posición intermedia. Bebe más café.",
        "El 65% de los cafés se toman en el desayuno. El otro 45 también si los ayunos son de un segundo.",
        "La mejor hora para consumir café es desde que te despiertas hasta 16 horas después.",
        "Los gritos de una persona durante 8 años, 7 meses y 6 días, generarían suficiente energía para calentar una taza de café.",
        "El café puede mejorar el rendimiento físico. Puede que también lo empeore. Puede que no haga nada.",
        "Aunque no lo parezca, el café tiene grandes ventajas para la salud. Más de una concretamente.",
        "Los granos de café en realidad no son granos. Son semillas de fruta. Vives una mentira",
        "Los efectos del café disminuyen cuando se mezcla con leche. Y llegan a 0 si no tomas.",
        "La gente que programa es la única máquina que convierte el café en líneas de código.",
        "El café viene de árbol llamada cafeto de hasta seis metros de altura. Búscalo, no estoy muy seguro.",
        "La saliva elimina la mitad del sabor del café por muy fuerte que lo sientas. La otra mitad lo eliminó el azucar del torrefacto.",
        "El 1 de octubre de 86 a. C. nace Salustio un historiador romano. El 1 de octubre también es el dia mundial del café. No tienen nada que ver una cosa con la otra",
        "All praise the clock",
        "Los dos gatos más longevos del mundo tenían algo en común, ambos tomaban café. Sorprendente ¿no? (Correlación no implica causalidad).",
        "El café es un buen fertilizante. Pero no te pases. La medida suele estar entre una cucharadita de café y 8 kilos.",
        "Si lo piensas muy detenidamente, un café no es una ensalada."
    };
    public TextMeshProUGUI label;
    private Coroutine coroutine;
    public CanvasGroup canvasGroup;


    private void Start()
    {
        nextPhrase();
    }

    public void setText(string text)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        canvasGroup.alpha = 1;
        label.SetText(text);
    }

    void nextPhrase()
    {
        int randomIndex = Random.Range(0, phrases.Length - 1);
        string next = phrases[randomIndex];
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(showPhrase(next));
    }

    private IEnumerator showPhrase(string next)
    {
        label.SetText(next);
        yield return FadeCGAlpha(0, 1, 2);
        yield return new WaitForSeconds(3.0f);
        yield return FadeCGAlpha(1, 0, 2);
        nextPhrase();
    }
    
    private IEnumerator FadeCGAlpha(float from, float to, float duration) {
        float elaspedTime = 0f;
        while (elaspedTime <= duration) {
            elaspedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, elaspedTime / duration);
            yield return null;
        }
        canvasGroup.alpha = to;
    }
}
