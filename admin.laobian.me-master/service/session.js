'use strict';

const config = require('config');
const redis = require('../utility/redis');
const cryptoJS = require('crypto-js');
const moment = require('moment');
const uuid = require('node-uuid');
const prefix = "session_";

exports.exists = function(sessionId, callback){
    redis.get(prefix + sessionId, function(err, result){
        if(err || !result){
            callback(false);
        }else{
            callback(true);
        }
    });
};

exports.add = function(id, email, userName, allowed, callback){
    let encrypt = cryptoJS.AES.encrypt(uuid.v1(), config.get('session.secretkey'));
    redis.set(prefix + encrypt.toString(), JSON.stringify({  
        id: id,
        email: email,
        userName: userName,
        allowed: allowed,
        createAt: moment.utc().format()
    }), config.get('session.serverTTL'), function(err){
        callback(err, encrypt.toString());
    })
};

exports.get = function(sessionId, callback){
    redis.get(prefix + sessionId, function(err, result){
        callback(err, result);
    });
}

exports.remove = function(sessionId, callback){
    redis.del(prefix + sessionId, callback);
};