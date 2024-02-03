using System.Collections;
using UnityEngine;

public class Parpadeo : MonoBehaviour
{
    public GameObject mesh;
    public int cantidadDeParpadeos = 15;
    public float tiempoEntreParpadeos = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Parpadear());
    }

    IEnumerator Parpadear()
    {
        for (int i = 0; i < cantidadDeParpadeos; i++)
        {
            yield return new WaitForSeconds(tiempoEntreParpadeos);

            // Desaparecer el objeto
            mesh.SetActive(false);

            // Esperar el tiempo especificado
            yield return new WaitForSeconds(tiempoEntreParpadeos);

            // Volver a aparecer el objeto
            mesh.SetActive(true);
        }
    }
}
