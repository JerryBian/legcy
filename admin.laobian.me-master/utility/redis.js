'use strict';

const config = require('config');
const redis = require('redis');
const client = redis.createClient({ host: config.get('redis.host'), password: config.get('redis.password') });

client.on('error', function(err){
    console.log(`Redis error: ${err}`);
});

exports.set = function(key, val, expire, callback){
    client.set(key, val, function(err, reply){
        if(err){
            callback(err);
        }else{
            client.expire(key, expire, function(err, reply){
                if(err){
                    callback(err);
                }else{
                    callback(null);
                }
            });
        }
    });
};

exports.get = function(key, callback){
    client.get(key, function(err, reply){
        callback(err, reply);
    });
};

exports.del = function(key, callback){
    client.del(key, callback);
}