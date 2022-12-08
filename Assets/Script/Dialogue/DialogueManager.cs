using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    private void Awake()
    {
        instance = this;
        if(instance == null)
        {
            Debug.LogWarning(" Il y a plus d une instance de dialogueManager dans la scene");
            return;
        }

        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {     
        string sentence = sentences.Dequeue();
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    // Coroutine pour afficher caractère par caractère
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialogue()
    {   
        animator.SetBool("isOpen", false);
        Debug.Log("Fin du dialogue");
    }

}
