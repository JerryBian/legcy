'use strict'

const config = require('config');
const twitter = require('twitter');
const twitter_client = new twitter({
    consumer_key: config.get('twitter.consumer_key'),
    consumer_secret: config.get('twitter.consumer_secret'),
    access_token_key: config.get('twitter.access_token_key'),
    access_token_secret: config.get('twitter.access_token_secret')
});

exports.timeline = function(screen_name, callback){
    twitter_client.get('statuses/user_timeline', { screen_name: screen_name, count: 8 }, callback);  
};