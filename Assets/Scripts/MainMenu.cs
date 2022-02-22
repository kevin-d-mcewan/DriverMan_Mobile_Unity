using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Button playButton;
    [SerializeField] private int MaxEnergy;                     // INT for Engegy Max i.e. 5
    [SerializeField] private int energyRechargeDuration;        // How long it takes for Energy to recharge (Sec or Min or Hrs) Mins in this version
    [SerializeField] private AndroidNotificationHandler androidNotificationHandler;
    [SerializeField] private iOSNotificationHandler iosNotificationHandler;

    private int energy;                                         // Energy that will be used in code

    private const string EnergyKey = "Energy";                  // The key used for Player Pref so it can't be changed and less likely to miss spell string
    private const string EnergyReadyKey = "EnergyReady";        // The Key const for player pref of when the energy will be ready



    // This runs all code at start, crash and with fx call follow those rules as well so need both
    private void Start()
    {
        OnApplicationFocus(true);
    }



    // Will be ran anytime we load up the game or going back to the main menu
    // "OnApplicationFocus" will be used so that if it gets minimized it still runs this function where it would not in start
    private void OnApplicationFocus(bool hasFocus)
    {
        // We want this to happen everytime we go To MainMenu so Start() is best
        // Load from PlayerPrefs the HighScore, Then Update the HighScoreText text.

        // If you want to access information from another class; prepend the function or var your wanting to use with ClassName.Function/var name
        // i.e. "ScoreSystem.currentHighScore

        // if minimized return nothing and do nothing
        if (!hasFocus) { return; }

        // If we are maximizing the app or restoring it we want to Cancel all the invokes in the script; If you want to call a specific one then switch it with
        // CancelInvoke(("InvokeStringName"));
        CancelInvoke();

        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);

        highScoreText.text = $"High Score: {highScore}";  //Same way as doing " 'High Score: ' + highScore.ToString();"

        energy = PlayerPrefs.GetInt(EnergyKey, MaxEnergy); // In GetInt( Key, maxValue)

        if (energy == 0)
        {
            string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);     // "string.Empty" is used if it is missing a string

            // if it returns an Empty String then there is an "issue" and something else needs to be done (This should never happen below line)
            if (energyReadyString == string.Empty){ return;  }

            DateTime energyReady = DateTime.Parse(energyReadyString); // This takes the string and parses it into a DateTime obj to be used that way

            if (DateTime.Now > energyReady)     // "DateTime.Now" refers to real world time right this second and if thats > 'energyReady' we set the "energy" to maxEnergy value and store it in PlayerPrefs
            {
                energy = MaxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
            else
            {
                // Turn off Energy Button when not Ready
                playButton.interactable = false;
                Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
            }

        }

        energyText.text = $"Play: ({energy})";
    }



    private void EnergyRecharged()
    {
        // Turns on the Play Button
        playButton.interactable = true;
        // Sets energy to Max
        energy = MaxEnergy;
        // updates energy amount in PlayerPrefs
        PlayerPrefs.SetInt(EnergyKey, energy);
        // updates text in the Play Button
        energyText.text = $"Play ({energy})";
    }


    
    // Play() is attach to OnClick() event in Inspector attached to TMP_Button Play. This fx is ran everytime play btn is pressed
    public void Play()
    {
        // If player has no energy return nothing so Play button wont work
        if (energy < 1) { return; }

        // When btn pressed subtract 1 from energy total
        energy--;

        // Save new energy amount in energy to the "EnergyKey" id (think of it as the id in SQL)
        PlayerPrefs.SetInt(EnergyKey, energy);

        // If energy is equal to 0 then run code in IF Statement
        if (energy == 0)
        {
            // "energyReady" is set to Add Minutes of "energyRechargeDuration" that was set in Game Inspector
            DateTime energyReady = DateTime.Now.AddMinutes(energyRechargeDuration);
            // We save it (SetString) as a string bc it can't be saved in PlayerPrefs as a DateTIme
            PlayerPrefs.SetString(EnergyReadyKey, energyReady.ToString());

            // This next set of #if/#endif will not compile unless its running a compiled Android Version
#if UNITY_ANDROID
            androidNotificationHandler.ScheduleNotification(energyReady);
#elif UNITY_IOS
            iosNotificationHandler.ScheduleNotification(energyRechargeDuration);
#endif

        }
        
        // LoadScene(1) if we have energy >= 1
        SceneManager.LoadScene(1);


        
    }

}
