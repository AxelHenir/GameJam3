using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace DescantRuntime
{
    public class DescantConversationUI : MonoBehaviour
    {
        //[Header("Data")]
        //[Tooltip("The Descant Graph that will be played")] public TextAsset descantGraph;
        
        [Header("UI")]
        [SerializeField, Tooltip("The NPC response text")] TMP_Text response;
        [SerializeField, Tooltip("The parent UI object for the player's choices (ideally a LayoutGroup)")] Transform choices;
        [SerializeField, Tooltip("The player choice prefab to be spawned with the choice text")] GameObject choice;

        [Header("Actors")]
        [SerializeField] Image choiceActor;
        [SerializeField] Image responseActor;
        [SerializeField] string[] actorNames;
        [SerializeField] Sprite[] actorSprites;
        
        DescantConversationController conversationController;

        public delegate void ConversationDone();

        public ConversationDone conversationDone;

        [HideInInspector] public bool isResponseType;

        bool clicktoSkip;
    
        void Awake()
        {
            //conversationController = gameObject.AddComponent<DescantConversationController>();
            //conversationController.Initialize(descantGraph);
        }
        
        void Start()
        {
            //DisplayNode();
        }

        void Update()
        {
            if (clicktoSkip && (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.E)))
                DisplayNode();
        }

        public void InitializeDialogue(TextAsset dialogueFile)
        {
            if (conversationController == null)
                conversationController = gameObject.AddComponent<DescantConversationController>();
            
            choiceActor.gameObject.SetActive(false);
            responseActor.gameObject.SetActive(false);
            
            conversationController.Initialize(dialogueFile);
            DisplayNode();
        }

        /// <summary>
        /// Calls the Next() method in the conversation controller, gets the data, and displays it on-screen
        /// </summary>
        /// <param name="choiceIndex">
        /// The index of the choice being made (base 0)
        /// (default 0 if the current node is a ResponseNode)
        /// </param>
        void DisplayNode(int choiceIndex = 0)
        {
            clicktoSkip = false;
            response.transform.GetChild(0).gameObject.SetActive(false);
            
            // Destroying all the old choices
            for (int i = 0; i < choices.childCount; i++)
                Destroy(choices.GetChild(i).gameObject);
            
            List<string> temp = conversationController.Next(choiceIndex);
            if (temp == null || temp.Count == 1){
                conversationDone.Invoke();
                return; // Stopping if there are no more nodes
            }

            Sprite currentSprite = GetActorSpriteFromName(conversationController.Current.Data.ActorName);
    
            // Displaying the ResponseNodes...
            if (temp[0] == "Response")
            {
                if (currentSprite != null)
                {
                    responseActor.gameObject.SetActive(true);
                    // change the sprite color base on the actor - later have this implemented for the editor
                    //blue, pink, orange, green, yellow
                    switch (conversationController.Current.Data.ActorName)
                    {
                        case "B":
                            responseActor.color = new Color(185 / 255.0f, 104 / 255.0f, 79 / 255.0f); //orange
                            break;
                        case "S":
                            responseActor.color = new Color(210 / 255.0f, 168 / 255.0f, 101 / 255.0f); //yellow
                            break;
                        case "P1":
                            responseActor.color = new Color(117 / 255.0f, 161 / 255.0f, 101 / 255.0f); //green
                            break;
                        case "P2":
                            responseActor.color = new Color(190 / 255.0f, 122 / 255.0f, 128 / 255.0f); //pink
                            break;
                        case "A":                            
                            responseActor.color = new Color(133 / 255.0f, 167 / 255.0f, 182 / 255.0f); //blue
                            break;
                        case "T":
                            responseActor.color = new Color(87 / 255.0f, 87 / 255.0f, 119 / 255.0f); //dark blue  /// responseActor.color = new Color(77 / 255.0f, 57 / 255.0f, 86 / 255.0f); //dark purple 
                            break;
                    }
                    responseActor.sprite = currentSprite;
                }
                else responseActor.gameObject.SetActive(false);
                
                isResponseType = true;
                response.text = temp[1];

                if (conversationController.Current.Next[0].Data.Type == "Response")
                {
                    clicktoSkip = true;
                    response.transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    // Once the response text has been shown, we skip ahead to show the player's possible choices
                    DisplayNode();   
                }
            }
            // Displaying the ChoiceNodes...
            else if (temp[0] == "Choice")
            {
                if (currentSprite != null)
                {
                    choiceActor.gameObject.SetActive(true);
                    choiceActor.sprite = currentSprite;
                }
                else choiceActor.gameObject.SetActive(false);
                
                isResponseType = false;
                for (int j = 1; j < temp.Count; j++)
                {
                    // Instantiating the player choices in teh player choice parent
                    GameObject tempChoice = Instantiate(choice, choices);
                    
                    // Setting the text of the choice
                    tempChoice.GetComponentInChildren<TMP_Text>().text = temp[j]; 
                    
                    // Copying the current index to a copy variable so that it can be used in the listener below
                    // (absolutely no idea why this must be done, but it must)
                    var copy = j - 1;
                    
                    // Adding a listener to the player choice's button to display the next node when clicked
                    tempChoice.GetComponentInChildren<Button>().onClick.AddListener(() =>
                    {
                        DisplayNode(copy); 
                    });
                }
            }
        }

        Sprite GetActorSpriteFromName(string actorName)
        {
            if(actorName != null)
            {
                if (actorName.Trim() == "") return null;

                for (int i = 0; i < actorNames.Length; i++)
                    if (actorNames[i] == actorName)
                        return actorSprites[i];
            }

            return null;
        }
    }
}