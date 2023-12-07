using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableHighlighter : MonoBehaviour
{
    public Material highlightMaterial; // Assign this in the editor
    private Material originalMaterial; // Store the original material
    private Renderer tableRenderer; // Cache the renderer

    void Start()
    {
        tableRenderer = GetComponent<Renderer>();
        originalMaterial = tableRenderer.material; // Store the original material
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the colliding object is the player
        {
            HighlightTable(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HighlightTable(false);
        }
    }

    void HighlightTable(bool doHighlight)
    {
        if (doHighlight)
        {
            tableRenderer.material = highlightMaterial; // Change to highlight material
        }
        else
        {
            tableRenderer.material = originalMaterial; // Revert to original material
        }
    }
}
