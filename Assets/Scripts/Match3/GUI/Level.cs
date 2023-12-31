﻿using UnityEngine;
using UnityEngine.UI;

namespace SweetSugar.Scripts.GUI
{
    public class Level : MonoBehaviour {
        public int number;
        public Text label;
        public GameObject lockimage;

        // Use this for initialization
        void Start () {
            if( PlayerPrefs.GetInt( "Score" + (number-1) ) > 0  )
            {
                lockimage.gameObject.SetActive( false );
                label.text = "" + number;
            }

            var stars = PlayerPrefs.GetInt( string.Format( "Level.{0:000}.StarsCount", number ), 0 );

            if( stars > 0 )
            {
                for( var i = 1; i <= stars; i++ )
                {
                    transform.Find( "Star" + i ).gameObject.SetActive( true );
                }

            }

        }
	
        // Update is called once per frame
        void Update () {
	
        }

        public void StartLevel()
        {
//        InitScript.Instance.OnLevelClicked(number);

        }
    }
}
