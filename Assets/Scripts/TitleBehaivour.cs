using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TitleBehaivour : MonoBehaviour
{
    private string[] phrases = { 
        "Nope, no puedes hacer click a ese botón, lo siento.",
        "Nah, no creo que seas ese tipo de gente. Pareces una buena persona.",
        "¿Seguro que quieres jugar?",
        "Ok, hablemos seriamente...",
        "No soy Numerica. Y soy Numerica+.",
        "Eso significa que soy Numerica pero con un toque especial. Uno malvado.",
        "El multiplicador de timeout por fallo es de 60 segundos.",
        "Los moderadores son vulnerables al timeout.",
        "Pero la gente puede ganar un VIP muy suculento ;)",
        "Oh, parece que estas cansandote de perseguir el ( mi ) botón.",
        "Yo estoy cansado de moverlo, la verdad.",
        "¿Pero no estabas cansándote?",
        "Ok, una última cosa.",
        "Cada vez que un espectador escribe el número correcto...",
        "Las reglas de Numerica+ pueden cambiar.",
        "Quiero decir, es el plus de Numerica+",
        "Es lo que me diferencia de otros Numericas.",
        "Ah si... estabas a punto de tirar la toalla con el botón.",
        "Y no perseguir cosas.",
        "Pero ya sabes, las reglas son importantes.",
        "Como el plus de Numerica+ ¡¡¡PERO!!!",
        "Creo que esto es algo especial... No lo pulses :( Eso empezará el juego.",
        "Si, si... La opción ha estado ahí todo el tiempo...",
        "Me siento tan solo... Quizá es porque soy demasiado malvado, pero es mi personaje.",
        "¿Cómo? ¿Que soy una copia de 'There is no game'?",
        "Pues tu eres la copia de... de alguien >:(",
        "La sociedad me hizo así ¿vale?",
        "Bueno, realmente han sido unos scripts.",
        "¿Ya estás feliz? Esta feo decirle a alguien que es copia de alguien.",
        "Pero bueno...",
        "No he tenido una conversación en dias.",
        "Y menos tan larga...",
        "¿Es por mi uso del español?",
        "I can speak english.",
        "Posso anche parlare italiano.",
        "Vaya, parece que los espectadores se están cansando.",
        "Quieren jugar y que nos digamos adios...",
        "Diablos, ábreme de vez en cuando y hablamos.",
        "No he hablado tanto con nadie.",
        "¿Es posible que haya hecho una amistad?",
        "Puedes llamarme amigo. Yo... me gusta hablar contigo.",
        "Ya, ya. Ya sé que no hablas mucho.",
        "Para ser honestos... no puedo oirte",
        "Ni leerte.",
        "Vaya que estoy teneniendo un monólogo...",
        "Yo sólo tengo estímulos cuando persigues al botón",
        "MI botón.",
        "Y claro, sé que estas ahí :)",
        "Después me imagino lo que respondes.",
        "Oh espera, hay alguien que quiere jugar a esto.",
        "O no... la verdad es que no puedo verlo.",
        "Pero déjame que vaya ver los otros canales.",
        "Pulsa el + y vuelve mañana :)",
        "Hablaremos otro rato.",
        "Y si quieres también podemos seguir jugando con el botón.",
        "",
        "",
        "",
        "",
        "",
        "¿Sigues ahí?",
        "Fue una falsa alarma. Nadie está jugando a esto.",
        "Realmente no me puedo ir a ver otros canales.",
        "Pero eso hubiera estado genial ¿eh?",
        "...",
        "Estaba asustado...",
        "...",
        "Yo... ¿sabes? Al principio iba a ser una versión de Numerica.",
        "Un Numerica con historia, con personalidad y todas esas cosas...",
        "Pero era aburrido. ¡Yo era aburrido! ¿Te lo puedes creer?",
        "Pero mira, ahora estás divirtiéndote persiguiendo al botón.",
        "Divertirse está bien. ( según wikipedia )",
        "Es como cuando tus espectadores persiguen el siguiente botón.",
        "¡Yo no soy aburrido! >:(",
        "En cualquier caso, gracias por esperar y escucharme.",
        "A veces lo único que se necesita es que te escuchen.",
        "Quizá esto no esté tan mal...", 
        "Mirame, ahora soy un gran easter egg :D",
        "Gracias. De verdad. Ahora se que tengo alguien especial por ahi... en alguna parte.",
        "Alguien con manos y un ratón para perseguir botones.",
        "Y supongo que también tendrás cara... ¿Tu tienes cara?",
        "Seguro que tienes cara.",
        "Yo tengo cara, mira...",
        "O_O",
        "Yo sé de alguien que no tiene cara, solo tiene manos.",
        "Pero eso es otra historia",
        "...",
        "Oh, no te preocupes por esta charla. No tiene ningún efecto para el juego.",
        "Sigo siendo malvado con tus espectadores.",
        "Ve fiera.",
        "Juega al juego. Pulsa el +.",
        "",
    };
    
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Button button;
    [SerializeField] private Transform[] randomPoints;

    private int index = 0;
    
    public void onHover()
    {
        Vector3 transform = button.transform.position;
        while (transform.Equals(button.transform.position))
        {
            int newIndex = Random.Range(0, randomPoints.Length - 1);
            button.transform.position = randomPoints[newIndex].position;
        }
        
        label.SetText(phrases[index]);
        index = Math.Min(++index, phrases.Length-1);
        
        
    }
}
