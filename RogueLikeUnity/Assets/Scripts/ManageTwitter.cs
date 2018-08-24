using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Twitter;
using UnityEngine;

namespace Assets.Scripts
{
    public class ManageTwitter : MonoBehaviour
    {
        const string PLAYER_PREFS_TWITTER_USER_ID = "TwitterUserID";
        const string PLAYER_PREFS_TWITTER_USER_SCREEN_NAME = "TwitterUserScreenName";
        const string PLAYER_PREFS_TWITTER_USER_TOKEN = "TwitterUserToken";
        const string PLAYER_PREFS_TWITTER_USER_TOKEN_SECRET = "TwitterUserTokenSecret";

        Twitter.RequestTokenResponse m_RequestTokenResponse;
        Twitter.AccessTokenResponse m_AccessTokenResponse;

        string m_PIN = "832955370649759744-Rpm97IGJ5ecZffWo9xb0ekBegJ8Lsbj";
        string m_Tweet = "enter your tweet here.";

        public void test()
        {

            string CONSUMER_KEY = "IrwkW1kQMD9ok0PeGP1l3mKCU";
            string CONSUMER_SECRET = "lvEYsOeypdi4FzpuWi13r7Q9iRadR5Czk5PvF5lCLeJol6Jgfu";
            StartCoroutine(Twitter.API.GetAccessToken(CONSUMER_KEY, CONSUMER_SECRET, m_RequestTokenResponse.Token, m_PIN,
                           new Twitter.AccessTokenCallback(this.OnAccessTokenCallback)));
        }

        void OnAccessTokenCallback(bool success, Twitter.AccessTokenResponse response)
        {
            if (success)
            {
                string log = "OnAccessTokenCallback - succeeded";
                log += "\n    UserId : " + response.UserId;
                log += "\n    ScreenName : " + response.ScreenName;
                log += "\n    Token : " + response.Token;
                log += "\n    TokenSecret : " + response.TokenSecret;
                print(log);

                m_AccessTokenResponse = response;

                PlayerPrefs.SetString(PLAYER_PREFS_TWITTER_USER_ID, response.UserId);
                PlayerPrefs.SetString(PLAYER_PREFS_TWITTER_USER_SCREEN_NAME, response.ScreenName);
                PlayerPrefs.SetString(PLAYER_PREFS_TWITTER_USER_TOKEN, response.Token);
                PlayerPrefs.SetString(PLAYER_PREFS_TWITTER_USER_TOKEN_SECRET, response.TokenSecret);
            }
            else
            {
                print("OnAccessTokenCallback - failed.");
            }
        }

        void LoadTwitterUserInfo()
        {
            m_AccessTokenResponse = new Twitter.AccessTokenResponse();

            const string PLAYER_PREFS_TWITTER_USER_ID = "TwitterUserID";
            const string PLAYER_PREFS_TWITTER_USER_SCREEN_NAME = "TwitterUserScreenName";
            const string PLAYER_PREFS_TWITTER_USER_TOKEN = "TwitterUserToken";
            const string PLAYER_PREFS_TWITTER_USER_TOKEN_SECRET = "TwitterUserTokenSecret";

            string m_PIN = "832955370649759744-Rpm97IGJ5ecZffWo9xb0ekBegJ8Lsbj";
            string m_Tweet = "tsTest";

            m_AccessTokenResponse.UserId = PlayerPrefs.GetString(PLAYER_PREFS_TWITTER_USER_ID);
            m_AccessTokenResponse.ScreenName = PlayerPrefs.GetString(PLAYER_PREFS_TWITTER_USER_SCREEN_NAME);
            m_AccessTokenResponse.Token = PlayerPrefs.GetString(PLAYER_PREFS_TWITTER_USER_TOKEN);
            m_AccessTokenResponse.TokenSecret = PlayerPrefs.GetString(PLAYER_PREFS_TWITTER_USER_TOKEN_SECRET);

            if (!string.IsNullOrEmpty(m_AccessTokenResponse.Token) &&
                !string.IsNullOrEmpty(m_AccessTokenResponse.ScreenName) &&
                !string.IsNullOrEmpty(m_AccessTokenResponse.Token) &&
                !string.IsNullOrEmpty(m_AccessTokenResponse.TokenSecret))
            {
                string log = "LoadTwitterUserInfo - succeeded";
                log += "\n    UserId : " + m_AccessTokenResponse.UserId;
                log += "\n    ScreenName : " + m_AccessTokenResponse.ScreenName;
                log += "\n    Token : " + m_AccessTokenResponse.Token;
                log += "\n    TokenSecret : " + m_AccessTokenResponse.TokenSecret;
              
            }
        }

        public static IEnumerator GetAccessToken(string consumerKey, string consumerSecret, string requestToken, string pin, AccessTokenCallback callback)
        {
            WWW web = WWWAccessToken(consumerKey, consumerSecret, requestToken, pin);

            yield return web;

            if (!string.IsNullOrEmpty(web.error))
            {
                Debug.Log(string.Format("GetAccessToken - failed. error : {0}", web.error));
                callback(false, null);
            }
            else
            {
                AccessTokenResponse response = new AccessTokenResponse
                {
                    Token = Regex.Match(web.text, @"oauth_token=([^&]+)").Groups[1].Value,
                    TokenSecret = Regex.Match(web.text, @"oauth_token_secret=([^&]+)").Groups[1].Value,
                    UserId = Regex.Match(web.text, @"user_id=([^&]+)").Groups[1].Value,
                    ScreenName = Regex.Match(web.text, @"screen_name=([^&]+)").Groups[1].Value
                };

                if (!string.IsNullOrEmpty(response.Token) &&
                    !string.IsNullOrEmpty(response.TokenSecret) &&
                    !string.IsNullOrEmpty(response.UserId) &&
                    !string.IsNullOrEmpty(response.ScreenName))
                {
                    callback(true, response);
                }
                else
                {
                    Debug.Log(string.Format("GetAccessToken - failed. response : {0}", web.text));

                    callback(false, null);
                }
            }
        }

        private static WWW WWWAccessToken(string consumerKey, string consumerSecret, string requestToken, string pin)
        {
            throw new NotImplementedException();
        }
    }
}
