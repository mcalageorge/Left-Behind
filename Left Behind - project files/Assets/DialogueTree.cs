using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//keep this here just in case DONT DELETE IT
/* aux = new string[stage1noAction.GetLength(1)];
 for (int i = 0; i < stage1noAction.GetLength(1); i++)
 {
     aux[i] = stage1noAction[stage1noActionINT, i];
     Debug.Log("THE LENGTH IS: " + aux.Length + " aux[i] =  " + aux[i]);
 }
 stage1noActionINT++;*/

public class DialogueTree : MonoBehaviour
{
    string previousAction = "nothing";
    public string gameStage = "nothing";

    public int hits = 0;
    int pacified = 0;

    BossAI bosu;

    BoxAI2 boxi2;

    int stall = 0;

    int neutraled = 0;

    public Text pursue;

    public Text[] names;
    public Sprite goodMercy;

    void Start()
    {
        bosu = gameObject.GetComponent<BossAI>();
        boxi2 = gameObject.GetComponent<BoxAI2>();
    }

    public Knob_MvFlash knobi;

    public string[] CalculateReply(string previousAction)
    {
        this.previousAction = previousAction;

        string[] aux = null;


        switch (gameStage)
        {
            case ("nothing"):
                {
                    if (previousAction != "Attack")
                    {
                        aux = GetLineFrom2DMatrix(stage1noAction, ref stage1noActionINT);

                        boxi2.reply = "* You were not expecting for anyone to be here.9";

                        Debug.Log("the problem is: " + stage1noActionINT);

                        if (stage1noActionINT == 5) { gameStage = "stage1"; bosu.turningToSecondPhase = true; neutraled++; }
                    }
                    else
                    {

                        aux = GetLineFrom2DMatrix(stage1AttackHit, ref stage1AttackHitINT);
                        boxi2.reply = "* Prepare yourself.9";
                        Debug.Log("boxis is " + boxi2.reply);
                        gameStage = "stage1"; bosu.turningToSecondPhase = true; this.gameObject.GetComponent<Attacks>().attacked_first = true;
                    }

                    return aux;
                }
            case ("stage1"):
                {
                    if (previousAction == "Attack")
                    {
                        boxi2.reply = GetBOX2dialogue(ref staINT, stage1Landmark, stage1BOX2);
                        if(stage2noActionINT < 8)
                        {
                            aux = GetLineFrom2DMatrix(stage2noAction, ref stage2noActionINT);
                            if (hits == 4) gameStage = "genocideLock";
                        }
                        else
                        {
                            aux = GetLineFrom2DMatrix(dotdotdot, ref dotdotdotINT);
                            gameStage = "neutralLock";
                        }

                    }
                    else if (previousAction == "Pacify")
                    {

                        boxi2.reply = GetBOX2dialogue(ref staINT, stage1Landmark, stage1BOX2);

                        if (stage2noActionINT < 8)
                        {
                            aux = GetLineFrom2DMatrix(stage2noAction, ref stage2noActionINT);
                            // neutraled++;
                            pacified++;
                            if (pacified == 3 && stage2noActionINT < 7) gameStage = "pacifistLock";
                        }
                        else
                        {
                            aux = GetLineFrom2DMatrix(dotdotdot, ref dotdotdotINT);
                            gameStage = "neutralLock";
                        }
                    }
                    else
                    {
                        boxi2.reply = GetBOX2dialogue(ref staINT, stage1Landmark, stage1BOX2);
                        aux = GetLineFrom2DMatrix(stage2noAction, ref stage2noActionINT);
                        // neutraled++;
                        if (stage2noActionINT == 8)
                            gameStage = "neutralLock";
                    }
                    return aux;

                }
            case ("pacifistLock"):
                {
                    if (previousAction == "Pacify")
                    {

                        aux = GetLineFrom2DMatrix(stage2Pacify, ref stage2PacifyINT);

                        boxi2.reply = GetBOX2dialogue(ref pacINT, pacifyLandmark, pacifyBOX2);

                        Debug.Log("gamestage: pacifistlock, INT is " + stage2PacifyINT);
                        if (stage2PacifyINT == 8)
                        {
                            boxi2.reply = "* Everything seems quite now.9";

                            music.Stop();
                            names[0].color = new Color(255, 0, 94, 255);
                            names[1].color = new Color(255, 0, 94, 255);
                            names[2].color = new Color(255, 0, 94, 255);
                            gameStage = "pacifistLock2";
                            knobi.killBoos = true;
                            bosu.feelsLikeAttacking = false;

                            //knobi.diefromSans = true;
                        }
                    }
                    else if (previousAction == "Attack")
                    {
                        boxi2.reply = GetBOX2dialogue(ref genINT, genocideLandmark, genocideBOX2);
                        aux = GetLineFrom2DMatrix(specialPacifistToGenocide, ref specialPacifistToGenocideINT);

                        //ADD SPECIAL BOX2

                        gameStage = "genocideLock";
                    }
                    else
                    {
                        boxi2.reply = GetBOX2dialogue(ref pacINT, pacifyLandmark, pacifyBOX2);
                        boxi2.reply = "* Everything seems quite now.9";
                        aux = GetLineFrom2DMatrix(dotdotdot, ref dotdotdotINT);
                        dotdotdotINT--;
                        Debug.Log("tree: player has to choose");
                        //DD  OOO N  N EEE
                        //D D O O NN N EE
                        //DD  OOO N NN EEE
                        // the boss doesnt reply back
                    }
                    //check for stall boss says ...
                    return aux;
                }
            case ("pacifistLock2"):
                {
                    if (previousAction == "Spare")
                    {
                        spareing.sparing = true;
                        myEvent.SetSelectedGameObject(null);
                        Debug.Log("tree: you go back to beginning by foor");
                        StartCoroutine(bosu.BossMercy("WalkPacifistSpare"));
                        aux = GetLineFrom2DMatrix(nine, ref zero);

                        //myEvent.SetSelectedGameObject(null);
                        //Debug.Log("Tree: boss was spared");

                        //StartCoroutine(bosu.BossMercy("text"));
                        //DD  OOO N  N EEE
                        //D D O O NN N EE
                        //DD  OOO N NN EEE
                        //boss makes itself gray
                        //fade to text
                    }
                    else if (previousAction == "Attack")
                    {
                        Debug.Log("Tree: boss dies and sans kills you");



                        //knobi.

                        Debug.Log("gamestage: pacifistlock2 " + knobi.sansDeath + " " + knobi.killBoos);

                        /*string[] h = new string[] {  "9", "9", "9", "9" };

                        aux = h;*/
                        //DD  OOO N  N EEE
                        //D D O O NN N EE
                        //DD  OOO N NN EEE


                        //boss dies
                        //sans comes and kills you
                        //---
                        //kill boss && wait a few seconds && sans comes in talks to you and kills you
                        //go to sans option : gamestage = "sans";
                    }
                    else if (previousAction == "Inquire")
                    {
                        bosu.feelsLikeAttacking = false;
                        boxi2.reply = GetBOX2dialogue(ref pacINT, pacifyLandmark, pacifyBOX2);
                        //gaster voice enabled
                        if (stopped_music == false)
                        {
                            music.Stop();
                            music.clip = audio;
                            stopped_music = true;

                        }
                        if (stage2InquireINT == 4)
                        {
                            music.Play();
                            pursue.text = "* Seek the truth";
                        }
                        aux = GetLineFrom2DMatrix(stage2Inquire, ref stage2InquireINT);

                    }
                    else
                    {
                        boxi2.reply = "* Everything is quiet now.9";
                        boxi2.reply = GetBOX2dialogue(ref pacINT, pacifyLandmark, pacifyBOX2);
                        aux = GetLineFrom2DMatrix(dotdotdot, ref dotdotdotINT);
                        dotdotdotINT--;
                        Debug.Log("tree: player has to choose");
                    }
                    // the boss doesnt reply back
                    //boss says ... upon other actions box says reason why

                    return aux;
                }
            /*  case ("sans"):
                  {         
                      aux = GetLineFrom2DMatrix(stage4sans, ref stage4sansINT);
                      return aux;
                      break;
                  }*/
            case ("genocideLock"):
                {
                    if (previousAction == "Attack")
                    {
                        /*OH GOD*/
                        boxi2.reply = GetBOX2dialogue(ref genINT, genocideLandmark, genocideBOX2);

                        aux = GetLineFrom2DMatrix(stage2AttackHit, ref stage2AttackHitINT);
                        if (stage2AttackHitINT == 6) { gameStage = "genocideLock2"; bosu.gameObject.GetComponent<Attacks>().final_attack = true; }
                    }
                    // put special interaction for each button
                    else
                    {
                        boxi2.reply = GetBOX2dialogue(ref genINT, genocideLandmark, genocideBOX2);
                        aux = GetLineFrom2DMatrix(dotdotdot, ref dotdotdotINT);
                        dotdotdotINT--;
                        Debug.Log("tree: player has to choose");
                        //DD  OOO N  N EEE
                        //D D O O NN N EE
                        //DD  OOO N NN EEE
                        // the boss doesnt reply back
                    }

                    return aux;
                }
            case ("genocideLock2"):
                {
                    if (previousAction == "Attack")
                    {
                        boxi2.reply = GetBOX2dialogue(ref genINT, genocideLandmark, genocideBOX2);
                        aux = GetLineFrom2DMatrix(stage2Genocide, ref stage2GenocideINT);

                        if (stage2GenocideINT == 2)
                        {
                            knobi.setTo1HP = true;
                            bosu.feelsLikeAttacking = false;
                            music.Stop();
                            boxi2.reply = "* The struggle is over.9";
                        }

                        else if (stage2GenocideINT == 3)
                        {

                            boxi2.reply = "* The struggle is over.9";

                            names[0].color = new Color(255, 0, 94, 255);
                            names[1].color = new Color(255, 0, 94, 255);
                            names[2].color = new Color(255, 0, 94, 255);


                            Debug.Log("sage2genocideint _" + stage2GenocideINT);
                            gameStage = "genocideLock3"; /*get boss hp to 1*/
                            StartCoroutine(SetDeathScene("WalkGenocideFight"));
                        }
                    }
                    // put special interaction for each button
                    else
                    {
                        boxi2.reply = GetBOX2dialogue(ref genINT, genocideLandmark, genocideBOX2);
                        aux = GetLineFrom2DMatrix(dotdotdot, ref dotdotdotINT);
                        dotdotdotINT--;
                        Debug.Log("tree: player has to choose");
                        //DD  OOO N  N EEE
                        //D D O O NN N EE
                        //DD  OOO N NN EEE
                        // the boss doesnt reply back
                    }
                    return aux;
                }
            case ("genocideLock3"):
                {
                    if (previousAction == "Attack")
                    {

                        boxi2.reply = GetBOX2dialogue(ref genINT, genocideLandmark, genocideBOX2);
                        aux = GetLineFrom2DMatrix(genocideEnding, ref genocideEndingINT);
                    }
                    else if (previousAction == "Spare")
                    {

                        boxi2.reply = GetBOX2dialogue(ref genINT, genocideLandmark, genocideBOX2);
                        aux = GetLineFrom2DMatrix(genocideMercyEnding, ref genocideMercyEndingINT);
                        if (genocideMercyEndingINT == 4)
                        {


                            bosu.feelsLikeAttacking = true;
                            Debug.Log("tree: the play was killed");
                            bosu.gameObject.GetComponent<Attacks>().kill_player_now = true;
                            bosu.gameObject.GetComponent<Attacks>().test = false;
                            //DD  OOO N  N EEE
                            //D D O O NN N EE
                            //DD  OOO N NN EEE
                            // kill the player
                        }
                    }
                    // put special interaction for each button ... no stall
                    else
                    {

                        boxi2.reply = GetBOX2dialogue(ref genINT, genocideLandmark, genocideBOX2);
                        aux = GetLineFrom2DMatrix(dotdotdot, ref dotdotdotINT);
                        dotdotdotINT--;
                        Debug.Log("tree: player has to choose");
                        //DD  OOO N  N EEE
                        //D D O O NN N EE
                        //DD  OOO N NN EEE
                        // the boss doesnt reply back
                    }
                    return aux;
                }
            case "neutralLock":
                {
                    Debug.Log("dsg;kihdfpouhgod[rfzhnijg'foxhjg");
                    if (previousAction == "Attack")
                    {

                        boxi2.reply = GetBOX2dialogue(ref genINT, genocideLandmark, genocideBOX2);
                        aux = GetLineFrom2DMatrix(dotdotdot, ref dotdotdotINT);
                        gameStage = "genocideLock";
                        /*if (hits <= 0)
                        {
                            // ADD SPECIAL INTERACTION FROM NEUTRAL TO GENCIDE IN BOX2
                            boxi2.reply = GetBOX2dialogue(ref genINT, genocideLandmark, genocideBOX2);
                            aux = GetLineFrom2DMatrix(stage1AttackHit, ref stage1AttackHitINT);
                            gameStage = "genocideLock";
                        }
                        else
                        {
                            
                            boxi2.reply = GetBOX2dialogue(ref neuINT, neutralLandmark, neutralBOX2);
                            aux = GetLineFrom2DMatrix(stage3noAction, ref stage3noActionINT);
                            if (stage3noActionINT == 1)
                            {
                                names[0].color = new Color(255, 187, 212);
                                names[1].color = new Color(255, 187, 212);
                                names[2].color = new Color(255, 187, 212);

                                neutraled++;
                                gameStage = "neutralLock2";
                                Debug.Log("tree: boss stopped attacking");
                                bosu.feelsLikeAttacking = false;
                                //knobi.killboosfair = true;
                                //knobi.killBoos = true;
                                //knobi.goToMap = "WalkNeutralAttack";
                                //Debug.Log("map to go to:" + knobi.goToMap);
                                
                                music.Stop();

                                //DD  OOO N  N EEE
                                //D D O O NN N EE
                                //DD  OOO N NN EEE
                                //boss stops attacking
                            }
                        }*/
                    }
                    else if (previousAction == "Pacify")
                    {

                        if (pacified == 4)
                        {
                            // textdmg.text = "9999";
                            // sliderdmg.value = 0;
                            boxi2.reply = GetBOX2dialogue(ref pacINT, pacifyLandmark, pacifyBOX2);
                            aux = GetLineFrom2DMatrix(stage2Pacify, ref stage2PacifyINT);
                            gameStage = "pacifistLock";

                        }
                        else
                        {
                            names[0].color = new Color(255, 0, 94, 255);
                            names[1].color = new Color(255, 0, 94, 255);
                            names[2].color = new Color(255, 0, 94, 255);

                            boxi2.reply = GetBOX2dialogue(ref neuINT, neutralLandmark, neutralBOX2);
                            neutraled++;
                            aux = GetLineFrom2DMatrix(stage3noAction, ref stage3noActionINT);
                            gameStage = "neutralLock2";
                            knobi.killboosfair = true;
                            knobi.goToMap = "WalkNeutralAttack";
                            Debug.Log("map to go to:" + knobi.goToMap);
                            knobi.killBoos = true;
                            music.Stop();
                        }

                    }
                    else
                    {

                        names[0].color = new Color(255, 0, 94, 255);
                        names[1].color = new Color(255, 0, 94, 255);
                        names[2].color = new Color(255, 0, 94, 255);

                        boxi2.reply = GetBOX2dialogue(ref neuINT, neutralLandmark, neutralBOX2);
                        boxi2.reply = "* Everything is quiet now.9";
                        neutraled++;
                        aux = GetLineFrom2DMatrix(stage3noAction, ref stage3noActionINT);

                        bosu.feelsLikeAttacking = false;
                        knobi.killboosfair = true;
                        knobi.goToMap = "WalkNeutralAttack";
                        Debug.Log("map to go to:" + knobi.goToMap);
                        knobi.killBoos = true;
                        music.Stop();
                        gameStage = "neutralLock2";
                        //DD  OOO N  N EEE
                        //D D O O NN N EE
                        //DD  OOO N NN EEE
                        //boss stops attacking
                    }
                    return aux;
                }
            case "neutralLock2":
                {
                    if (previousAction == "Attack")
                    {
                        knobi.killBoos = true;
                        knobi.killboosfair = true;

                        knobi.goToMap = "WalkNeutralAttack";
                        Debug.Log("map to go to:" + knobi.goToMap);
                        Debug.Log("tree: boss dies");

                        // boss dies
                    }
                    else if (previousAction == "Spare")
                    {
                        spareing.sparing = true;
                        myEvent.SetSelectedGameObject(null);
                        Debug.Log("tree: you go back to beginning by foor");
                        StartCoroutine(bosu.BossMercy("WalkNeutralSpare"));
                        aux = GetLineFrom2DMatrix(nine, ref zero);
                        //DD  OOO N  N EEE
                        //D D O O NN N EE
                        //DD  OOO N NN EEE
                        //you go back to the beginning
                    }
                    else
                    {
                        boxi2.reply = GetBOX2dialogue(ref neuINT, neutralLandmark, neutralBOX2);
                        aux = GetLineFrom2DMatrix(dotdotdot, ref dotdotdotINT);
                        dotdotdotINT--;
                        Debug.Log("tree: player has to choose");
                        //DD  OOO N  N EEE
                        //D D O O NN N EE
                        //DD  OOO N NN EEE
                        // the boss doesnt reply back
                    }
                    return aux;
                }
            default:
                {
                    Debug.Log("if the game failes i will kms");
                    return aux;
                }

        }
    }

    public int locked = 0;

    public Text textdmg;
    public Slider sliderdmg;

    public Spare spareing;

    int zero = 0;

    public UnityEngine.EventSystems.EventSystem myEvent;

    public AudioSource music;
    public AudioClip audio;
    bool stopped_music = false;
    bool started_music = false;

    int specialPacifistToGenocideINT = 0;

    string[,] specialPacifistToGenocide = new string[,]
    {
        { "GAAHG!9", "...9", "*laugh*9", "I knew it, I knew it from the begining.9", "You couldn't help yourself, could you?9", "If you think I would let my guard down that easily you are dead wrong.9", "How many monsters did you do this to?9", "You disgusting creature.9", "You don't deserve to breathe.9", "9" },
        { "9", "9", "9", "9", "9", "9", "9", "9", "9", "9" }
    };


    string[,] nine = new string[2, 2]
    {
        { "9", "9" },
        { "9", "9" }
    };

    string[] GetLineFrom2DMatrix(string[,] lines, ref int linesINT)
    {
        string[] auxString = new string[lines.GetLength(1)];
        for (int i = 0; i < lines.GetLength(1); i++)
        {
            auxString[i] = lines[linesINT, i];
            Debug.Log("THE LENGTH IS: " + auxString.Length + " aux[i] =  " + auxString[i]);
        }
        linesINT++;
        return auxString;
    }

    int dotdotdotINT = 0;

    public string[,] dotdotdot = new string[,]
    {
        { "...9", "9"},
        { "...9", "9"},
        { "9", "9"}
    };
    bool intro = true;
    bool firstPhase = true;
    bool secondPhase = false;

    int aux_counter = 0;

    /*public string CalculateReplyBox()
    {
        string reply = "9";
        Debug.Log("pacified is " + pacified + " and aux_counter is " + aux_counter + " pacINT is " + pacINT + " and neuINT is " + neuINT);
        if (firstPhase == true)
        {
            if (intro == true)
            {
                intro = false;
                reply = introMesssage;
                Debug.Log("reply is " + reply);

            }
            else if (hits == 1 && pacified == 0 && neutraled == 0)
            {
                firstPhase = false;
                secondPhase = true;
                reply = secondPhaseGenocide;
            }
            else 
            {
                reply = "You weren't expecting to encounter anyone here.";
                aux_counter++;
                if (aux_counter == 5)
                {
                    firstPhase = false;
                    secondPhase = true;
                    reply = "Second Form arise";
                }
            }
        }
        else if (secondPhase == true)
        {
            if (pacified == pacifyLandmark[pacINT])
            {
                reply = pacifyBOX2[pacINT];
                pacINT++;
                if (pacINT == pacifyLandmark.Length) pacINT--;
            }
            else if (neutraled == neutralLandmark[neuINT])
            {
                reply = neutralBOX2[neuINT];
                neuINT++;

                if (neuINT == neutralLandmark.Length) neuINT--;
            }
            else if (hits == atacetedLandmark[attINT])
            {
                reply = atacededBOX2[attINT];
                attINT++;

                if (attINT == atacetedLandmark.Length) attINT--;
            }
        }
        else { reply = "9"; }

        return reply;
    }*/

    IEnumerator SetDeathScene(string map)
    {
        yield return new WaitForSeconds(1f);
        knobi.killboosfair = true;
        knobi.goToMap = map;
        Debug.Log("map to go to:" + knobi.goToMap);
        knobi.killBoos = true;
    }


    string introMesssage = "The spider appears9";

    string secondPhaseGenocide = "She is really angry now9";

    string secondPhaseNeutral = "The ground is slightly shaking.9";



    string GetBOX2dialogue(ref int number, int[] aux, string[] lines)
    {
        string reply_for_box;

        Debug.Log("number is:" + number + " auxLength is " + aux.Length + aux[number] + " aux[nr] is");

        if (number == lines.Length) { number--; reply_for_box = lines[aux[number]]; }
        else
        {
            reply_for_box = lines[aux[number]];
            number++;
        }




        return reply_for_box;
    }
    int[] stage1Landmark = { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
    int staINT = 0;


    string[] stage1BOX2 =
    {
        "* She is glaring at you with a judging look.9",
        "* The enemy appears to be sizing you up.9"
    };

    int[] genocideLandmark = { 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
    int genINT = 0;

    string[] genocideBOX2 =
        {
        "* You remark that her arms are exposed.9",
        "* You notice a weak spot in her armour.9",
        "* You notice her movement slow down.9",
        "* Her breathing is getting sporadic.9",
        "* Her arms seem weaker now.9"
        };

    int[] neutralLandmark = { 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
    int neuINT = 0;


    string[] neutralBOX2 =
    {
        "* You weren't expecting to encounter anyone here.9",
        "* You can feel the ground slowly tremble.9",
        "* You wonder where Sans is.9",
        "* You spot Sans' figure outside the room, but you are not sure it's him.9",
        "* It's a lot quieter now.9"
    };

    int[] pacifyLandmark = { 0, 0, 2, 2, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
    int pacINT = 0;

    string[] pacifyBOX2 =
    {
        "* She seems to be a bit calmer now.9",
        "* You can see some remorse in her attacks now.9",
        "* You spot Sans' figure outside the room, but you are not sure it's him.9",
        "* Her attacks are becoming reluctant.9",
        "* It's a lot quieter now.9"
    };

    int stage4sansINT = 0;

    /* string[,] stage4sans = new string[,]
     {
         {"6line19", "line29","line39","line49","line59", "9"}
     };*/

    /*  public string[,] stage2EnrageBox1 =
          {
          "You tell her that she is too slow to get you.\nEnemy attacks are now faster.9", "You tease her about her armor.\nEnemy attacks are now getting even faster.9",
          "You tell her she is the weakest spider you have ever meet.\nThat really seemed to hurt her.\nShe is now fighting in her best form.9", "Nothing you say can make her angrier.9", "9"
          };*/

    /*public string[] stage2PacifyBox1 =
        {
        "You try to your best to calm the enemy down.\nShe ignore you, but you can see her expresion change.9",
        "You try to apologize for the unfortunate circumstances you are in.\nShes ignores you, but you can see visible anger on her face.",
        "You try your best to defuse the situation.9",
        };*/

    ///////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////
    ///							<summary>						    ///
    /// COMMENCE WITH THE DIALOGUE DONT BLAME ME IT LOOKS THIS WAY  ///
    ///							</summary>							///
    ///////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////

    // this dialogue is for when you begin the fight and you do any other action rather than attack

    int stage2PacifyINT = 0;

    public string[,] stage2Pacify = new string[,]
    {
        { "What are you trying?9", "I won't fall for your kind's tricks.9", "I hate people like you.9", "You always try to befriend everyone.9", "I don't know you and you don't know me.9", "What are you trying to pull?9",
        "No... I won't let you.9", "9"},
        { "Your kind always had this sharp tongue facade.9", "You emit a nice and bubbly personality, but how can anyone trust you?9", "9", "9", "9", "9", "9", "9"},
        { "What you are trying is pointless.9", "You only act nice to save your own skin.9", "9", "9", "9", "9", "9", "9"},
        { "I've been deceived by humans too many times to count.9", "I won't allow this to happen again.9", "9", "9", "9", "9", "9", "9"},
        { "I can sense your skeleton companion outside this room.9", "He seems similar to the former royal scients.9", "Perhaps he knows something.9", "9", "9", "9", "9", "9"},
        { "...9", "...9", "*laugh*9", "I doubt this, but...9", "Perhaps the only thing I need is to see the sun again.9", "9", "9", "9"},
        { "*laugh*9", "What I think I need, is some time to process all of this.9", "Maybe I'll eventually figure out what happened in the past.9", "9", "9", "9", "9", "9"},
        { "*laugh*9", "...\nMaybe...9", "9", "9", "9", "9", "9", "9"},
    };


    int stage1noActionINT = 0;

    public string[,] stage1noAction = new string[,]
        {
        { "This makes no sense...9", "I knew humans were strong.9", "But such a child to pass Asgore?9", "9", "9"},
        { "️And not only Asgore but his royal guard too...9", "Ha... Ha...9", "Weaklings...9", "9", "9"},
        { "I wonder if any of my kin are still alive.9", "I wonder if the old scientist is still alive.9", "9", "9", "9"},
        { "Hey you, I don't know what's going on the surface now.9", "But...9",
        "Before I was imprisoned we were at war.9", "And my job was to kill any human on sight.9", "9"},
        { "I don't care what you have done.9", "I don't care who you are.9",
        "Your kind is the reason the doctor got so desperate.9", "YOU WILL BE MY REVENGE!9", "9"}
        };

    int stage1AttackHitINT = 0;

    // this dialogue is for when you choose to attack after the battle begins
    public string[,] stage1AttackHit = new string[,]
        {
        {"Huh?9", "This really shows the difference between us.9", "You see, human souls are way too powerful.9", "That's why most monsters refused to fight.9", "And how we were driven so easily underground.9",
               "But I will not give up as easily.9", "So, prepare yourself!9" ,"9"}
			//commence stage 2, clock flies
		};

    int stage2AttackHitINT = 0;

    // this dialogue is for when you are locked into genocide
    public string[,] stage2AttackHit = new string[,]
        {
        {"HUGH!9", "*laugh*9", "You sure pack a punch.9", "But... I will not give up easily, so prepare yourself.9", "9"},
        {"You came here to find out more about these earthquakes.9", "Now you know that I am causing them.9", "9", "9", "9"},
        {"You may have a noble cause.9", "But that is of no importance now.9", "9", "9", "9"},
        {"You see, even one human soul will boost a monster power beyond belief.9", "If I take you out, I could make things right.9", "9", "9", "9"},
        {"With your determined soul I will rip humanity to shreds.9", "Just like your kind tried to do to us.9", "9", "9", "9"},
        {"Khaaak!9", "You are pretty strong, but it's pointless.9", "You will die right now.9", "9", "9"}
            //lock into genocie
        };

    int stage2GenocideINT = 0;

    // this dialogue plays in the semi final dialogue before you have the option to spare or kill
    // stops attacking
    public string[,] stage2Genocide = new string[,]
        {
        {"GHAAAH\n...\n...?9", "9", "9", "9", "9", "9", "9"},
        {"*laugh*9", "I guess I lost a few limbs.9", "That explains why my vision is getting blury.9",
"I guess this is how I die.9", "I had a good life I think.9", "Unfortunate it had to end this way.9", "9"},
        {"*laugh*9", "Now I guess I'll see him again.9", "If we go to the same place that is.9", "9","9", "9", "9"}
        };

    int genocideEndingINT = 0;

    // after this line is said, the player receives some exp and the fight ends
    public string[,] genocideEnding = new string[,]
        {
        {"*laugh*9", "Now I guess I'll see him again.9", "If we go to the same place that is.9", "9"},
        {"9","9","9", "9"}
        };

    int genocideMercyEndingINT = 0;

    // this dialogue is said if you get the enemy to 1 health and the player keeps clicking mercy
    public string[,] genocideMercyEnding = new string[,]
    {
        {"What...?9", "No...9", "You don't get to walk away from this.9", "Kill me!9", "9"},
        {"...9", "Why?9", "Why go so far?9", "Why not finish me off?9", "9"},
        {"If you don't kill me now, I will come after you.9", "My arms will grow back eventually.9", "9", "9", "9"},
		
		//the player is killed by a lot of rocks
		
        {"9", "9", "9", "9", "9"}
		//commence destrucitve attack that kills player
	};


    int stage2noActionINT = 0;

    public string[,] stage2noAction = new string[8, 4]
        {
        {"You know... I never really truly hated humans.9", "Nor did I ever relate to them.9", "9", "9"},
        {"I was born on the surface.9", "Everything was way nicer than down here.9", "9", "9"},
        {"You see, the royal scientist, he knew a war was coming.9", "And so did I.9", "9", "9"},
        {"He wanted to prepare, so that monsterkind had the upperhand.9", "But ASGORE refused.9", "9", "9"},
        {"The king wanted to trust the humans.9", "He thought our only way to survive was to befriend them.9", "9", "9"},
        {"But, they were really the ones who were scared.9", "They knew of our potential.9", "We posed a great threat to them.9", "9"},
        {"So what did they do?9", "They attempted to slaughter all of us.9", "9", "9"},
        {"We barely managed to retreat underground.9", "We were pushed to our limits here, but...9", "We survived...9", "9"}

        };

    int stage3noActionINT = 0;

    public string[,] stage3noAction = new string[2, 10]
         {
        {"Ugh...9", "...9", "I guess nothing has changed.9", "I still can't kill even a human child.9",
"*laugh*9", "Now leave me alone!9", "All of this is pointless anyway.9", "With the barrier broken, no monster is safe.9", "History will simply reapeat itself.9","9"},
        { "9","9","9","9","9","9","9","9","9","9"}
         };
    //stops attacking

    // *laugh* and changed expresion.
    // kill player

    //ON ATTACK HIT THEY DODGE AND LAUNCH UNDOGEBLE ROCK ATTACK

    //ENDING 2
    //
    //ON SPARE THEY GET SPARRED AND YOU LEAVE.

    int stage2InquireINT = 0;

    public string[,] stage2Inquire = new string[,]
        {
        {"I was part of the royal guard even before the war started.9", "Now, I guess I'm really a part of it anymore.9",
"9", "9", "9", "9", "9", "9"},
        {"You see, after monsterkind was pushed underground.9", "We needed a way to get out of here.9", "I offered myself to a soul enhancing experiment, led by the royal scient.9",
         "But, something went wrong and I got stuck in barrier.9", "9", "9", "9", "9"},
        {"Don't you already know he is always listening?9", "As a matter of fact he may already be in this very room.9",
         "If I tell you any more he might even make a move, even though I doubt it.9", "9", "9", "9", "9", "9"},
        //Inquire button cahnges to Pursue The Truth
        {"*LAUGH*9", "He fell into the core and he got trapped there.9", "You broke the barrier and you freed him.9", "9", "9", "9", "9", "9"},
        {"*LAUGH*9", "You already know about all of this, don't you?9", "Funny thing, he always had this aura around him.9", "As if he knew everything.9", "...9", "...9", "...9", "9"},
        //stop animation,all eyes are open and unmoving
        {"*GHHHHHHH*9", "8F︎i︎n︎a︎l︎l︎y︎.︎.︎.︎9", "A︎l︎l︎ t︎h︎a︎n︎ks︎ t︎o︎ y︎o︎u︎.︎9", "A︎n︎d︎ t︎h︎i︎s︎ p︎o︎o︎r︎ g︎i︎r︎l︎.︎9", "H︎o︎w︎ I︎ w︎i︎s︎h︎ t︎o︎ b︎e︎ a︎l︎i︎v︎e︎ a︎g︎a︎i︎n︎,︎ b︎u︎t︎ t︎h︎i︎s︎ b︎o︎d︎y︎ w︎i︎l︎l︎ d︎o︎.︎9",
         "N︎o︎w︎ i︎t︎'︎s︎ m︎y︎ t︎i︎m︎e︎ t︎o︎ m︎a︎ke︎ e︎v︎e︎r︎y︎t︎h︎i︎n︎g︎ r︎i︎g︎h︎t︎.︎9", "79", "9"},
        //gamecloses
        };
    //after this is exhausted, the button text of inquire changes to 'pursue the truth'

    //close game outright
}

/*{"*GHHHHHHH*9", "8F︎i︎n︎a︎l︎l︎y︎.︎.︎.︎9",
"✌︎●︎●︎ ⧫︎♒︎♋︎■︎🙵⬧︎ ⧫︎□︎ ⍓︎□︎◆︎📬︎9", "✌︎■︎♎︎ ⧫︎♒︎♓︎⬧︎ ◻︎□︎□︎❒︎ ♑︎♓︎❒︎●︎📬︎9", "☟︎□︎⬥︎ ✋︎ ⬥︎♓︎⬧︎♒︎ ⧫︎□︎ ♌︎♏︎ ♋︎●︎♓︎❖︎♏︎ ♋︎♑︎♋︎♓︎■︎📪︎ ♌︎◆︎⧫︎ ⧫︎♒︎♓︎⬧︎ ♌︎□︎♎︎⍓︎ ⬥︎♓︎●︎●︎ ♎︎□︎📬︎",
         "☠︎□︎⬥︎ ♓︎⧫︎🕯︎⬧︎ ❍︎⍓︎ ⧫︎♓︎❍︎♏︎ ⧫︎□︎ ❍︎♋︎🙵♏︎ ♏︎❖︎♏︎❒︎⍓︎⧫︎♒︎♓︎■︎♑︎ ❒︎♓︎♑︎♒︎⧫︎📬︎", "9"},
*/
