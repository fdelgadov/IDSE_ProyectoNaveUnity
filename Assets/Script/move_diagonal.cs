    using UnityEngine;

public class MovimientoDiagonalOscilatorio : MonoBehaviour
{
    [SerializeField] float amplitudX = 5f; // Amplitud en el eje X (izquierda-derecha)
    [SerializeField] float amplitudY = 5f; // Amplitud en el eje Y (arriba-abajo)
    [Range(0, 1)] [SerializeField] float factorMovimientoX;
    [Range(0, 1)] [SerializeField] float factorMovimientoY;
    private float periodoX = 2f;
    private float periodoY = 2f;

    Vector3 posicionInicial;

    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento oscilatorio en el eje X (izquierda-derecha)
        float ciclosX = Time.time / periodoX;
        const float tauX = Mathf.PI * 2;
        float valorSenoidalX = Mathf.Sin(tauX * ciclosX);
        factorMovimientoX = valorSenoidalX / 2 + 0.5f;

        // Movimiento oscilatorio en el eje Y (arriba-abajo)
        float ciclosY = Time.time / periodoY;
        const float tauY = Mathf.PI * 2;
        float valorSenoidalY = Mathf.Sin(tauY * ciclosY);
        factorMovimientoY = 0.5f - valorSenoidalY / 2; // Invertir el movimiento en el eje Y


        // Calcula la nueva posición del objeto en diagonal
        Vector3 nuevaPosicion = posicionInicial + new Vector3(amplitudX * factorMovimientoX, amplitudY * factorMovimientoY, 0);

        // Actualiza la posición del objeto
        transform.position = nuevaPosicion;
    }
}