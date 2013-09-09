using System;
using System.Collections.Generic;
using Twitterizer;
using Twitterizer.Streaming;
using Microsoft.AspNet.SignalR;

namespace TwitterSky
{
    public class TwitterSkyListener : Hub
    {
        private static TwitterStream _stream;

        private static OAuthTokens tokens = new OAuthTokens()
            {
                ConsumerKey = "add",
                ConsumerSecret = "your",
                AccessToken = "own",
                AccessTokenSecret = "keys ;)"
            };

        public void StartCaptureStreaming(string searchkey)
        {
            StreamOptions options = new StreamOptions();
            options.Track.Add(searchkey);

            _stream = new TwitterStream(tokens, "TwitterSky", options);

            _stream.StartPublicStream(
                StreamStopped,
                NewTweet,
                DeletedTweet,
                OtherEvent
            );
        }

        public void StopCaptureStreaming()
        {
            _stream.EndStream();
            _stream.Dispose();
        }

        void StreamStopped(StopReasons reason)
        {
            if (reason == StopReasons.StoppedByRequest)
            {
                //Do something...
            }
            else
            {
                //Do something...
            }
        }

        void NewTweet(TwitterStatus twitterizerStatus)
        {
            Clients.All.broadcastTweet(twitterizerStatus);
        }

        void DeletedTweet(TwitterStreamDeletedEvent e)
        {
            //Remove dialog
        }

        void OtherEvent(TwitterStreamEvent e)
        {
            //Show Event message or do nothing
        }  
    }
}