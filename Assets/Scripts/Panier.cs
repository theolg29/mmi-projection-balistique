using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Panier : MonoBehaviour
{
    public TextMeshProUGUI texteAffichageArmes;
    public TextMeshProUGUI texteAffichageDouilles;
    public string tagDesArmes = "weapon";
    public string tagDesDouilles = "bullet";

    private List<GameObject> armesDansLePanier = new List<GameObject>();
    private List<GameObject> douillesDansLePanier = new List<GameObject>();
    private const string introTexteArmes = "Armes déposées :\n";
    private const string introTexteDouilles = "Douilles déposées :\n";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagDesArmes))
        {
            if (!armesDansLePanier.Contains(other.gameObject))
            {
                armesDansLePanier.Add(other.gameObject);
                MettreAJourTexteArmes();
            }
        }

        if (other.CompareTag(tagDesDouilles))
        {
            if (!douillesDansLePanier.Contains(other.gameObject))
            {
                douillesDansLePanier.Add(other.gameObject);
                MettreAJourTexteDouilles();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagDesArmes))
        {
            if (armesDansLePanier.Contains(other.gameObject))
            {
                armesDansLePanier.Remove(other.gameObject);
                MettreAJourTexteArmes();
            }
        }

        if (other.CompareTag(tagDesDouilles))
        {
            if (douillesDansLePanier.Contains(other.gameObject))
            {
                douillesDansLePanier.Remove(other.gameObject);
                MettreAJourTexteDouilles();
            }
        }
    }

    private void MettreAJourTexteArmes()
    {
        if (armesDansLePanier.Count == 0)
        {
            texteAffichageArmes.text = "Panier d'armes vide.";
        }
        else
        {
            texteAffichageArmes.text = introTexteArmes;
            foreach (GameObject arme in armesDansLePanier)
            {
                texteAffichageArmes.text += arme.name + "\n";
            }
        }
    }

    private void MettreAJourTexteDouilles()
    {
        if (douillesDansLePanier.Count == 0)
        {
            texteAffichageDouilles.text = "Panier de douilles vide.";
        }
        else
        {
            texteAffichageDouilles.text = introTexteDouilles;
            foreach (GameObject douille in douillesDansLePanier)
            {
                texteAffichageDouilles.text += douille.name + "\n";
            }
        }
    }
}
