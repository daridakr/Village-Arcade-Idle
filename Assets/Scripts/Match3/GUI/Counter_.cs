using System.Collections;
using System.Linq;
using SweetSugar.Scripts.Core;
using SweetSugar.Scripts.Level;
using TMPro;
using UnityEngine;

namespace SweetSugar.Scripts.GUI
{
    /// <summary>
    /// various GUi counters
    /// </summary>
    public class Counter_ : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private float lastTime;
        private bool alert;

        private LevelData _thisLevelData;

        public LevelData ThisLevelData
        {
            get
            {
                if (_thisLevelData == null) _thisLevelData = LevelData.THIS;
                return _thisLevelData;
            }
            set => _thisLevelData = value;
        }

        void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            ThisLevelData = LevelManager.THIS.levelData;
        }

        void OnEnable()
        {
            lastTime = 0;
            UpdateText();
            alert = false; StartCoroutine(UpdateRare());
            if (name == "Limit") StartCoroutine(TimeTick());
        }

        // Update is called once per frame
        IEnumerator UpdateRare()
        {
            while (true)
            {
                if (_text == null) continue;

                UpdateText();
                yield return new WaitForSeconds(0.5f);
            }
        }

        private void UpdateText()
        {
            if (name == "Score")
            {
                _text.text = "" + LevelManager.Score;
            }

            if (name == "BestScore")
            {
                _text.text = "Best score:" + PlayerPrefs.GetInt("Score" + PlayerPrefs.GetInt("OpenLevel"));
            }

            if (name == "Limit" && ThisLevelData != null)
            {
                if (ThisLevelData.limitType == LIMIT.MOVES)
                {
                    _text.text = "" + Mathf.Clamp(ThisLevelData.limit, 0, ThisLevelData.limit);
                    _text.transform.localScale = Vector3.one;
                    if (ThisLevelData.limit <= 5)
                    {
                        _text.color = new Color(255f / 255f, 132f / 255, 222f / 255);
                        _text.outlineColor = Color.white;
                        if (!alert)
                        {
                            alert = true;
//                            SoundBase.Instance.PlayOneShot(SoundBase.Instance.alert);
                        }
                    }
                    else
                    {
                        alert = false;
                        _text.color = Color.white;
                        // txt.GetComponent<Outline>().effectColor = new Color(148f / 255f, 61f / 255f, 95f / 255f);
                    }
                }
                else
                {
                    var minutes = Mathf.FloorToInt(ThisLevelData.limit / 60F);
                    var seconds = Mathf.FloorToInt(ThisLevelData.limit - minutes * 60);
                    _text.text = "" + $"{minutes:00}:{seconds:00}";
                    _text.transform.localScale = Vector3.one * 0.68f;
                    _text.fontSize = 80;
                    if (ThisLevelData.limit <= 5 && LevelManager.THIS.gameStatus == GameState.Playing)
                    {
                        // txt.color = new Color(216f / 255f, 0, 0);
                        // txt.outlineColor = Color.white;
                        if (lastTime + 5 < Time.time)
                        {
                            lastTime = Time.time;
                            SoundBase.Instance.PlayOneShot(SoundBase.Instance.timeOut);
                        }
                    }
                    else
                    {
                        _text.color = Color.white;
                        _text.outlineColor = new Color(148f / 255f, 61f / 255f, 95f / 255f);
                    }
                }
            }

            if (name == "Lifes")
            {
                _text.text = "" + InitScript.Instance?.GetLife();
            }

            if (name == "FailedCount")
            {
                if (ThisLevelData.limitType == LIMIT.MOVES)
                    _text.text = "+" + LevelManager.THIS.ExtraFailedMoves;
                else
                    _text.text = "+" + LevelManager.THIS.ExtraFailedSecs;
            }

            if (name == "FailedPrice")
            {
                _text.text = "" + LevelManager.THIS.FailedCost;
            }

            if (name == "FailedDescription")
            {
                _text.text = "" + LevelData.THIS.GetTargetCounters().First(i => !i.IsTotalTargetReached()).targetLevel.GetFailedDescription();
            }

            if (name == "Gems")
            {
                _text.text = "" + InitScript.Gems;
            }

            if (name == "TargetScore")
            {
                _text.text = "" + ThisLevelData.star1;
            }

            if (name == "Level")
            {
                _text.text = "" + PlayerPrefs.GetInt("OpenLevel");
            }

            // if (name == "TargetDescription1")
            // {
            //     txt.text = "" + LevelData.THIS.GetTargetContainersForUI().First().targetLevel.GetDescription();
            // }
        }

        IEnumerator TimeTick()
        {
            while (true)
            {
                if (LevelManager.THIS.gameStatus == GameState.Playing)
                {
                    if (_thisLevelData.limitType == LIMIT.TIME)
                    {
                        _thisLevelData.limit--;
                        if (!LevelManager.THIS.DragBlocked)
                            LevelManager.THIS.CheckWinLose();
                    }
                }
                if (LevelManager.THIS.gameStatus == GameState.Map)
                    yield break;
                yield return new WaitForSeconds(1);
            }
        }
    }
}
