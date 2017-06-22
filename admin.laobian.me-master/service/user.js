'use strict';

const mysql = require('../utility/mysql');
const cryptoJS = require('crypto-js');

exports.login = function(email, password, callback){
    mysql.query('SELECT * FROM admin.user WHERE admin.user.email = ? AND admin.user.password = ?', [email.toLowerCase(), cryptoJS.MD5(password).toString()], function(err, result){
        callback(err, result);
    });
};

exports.getUsers = function(callback){
    mysql.query('SELECT * FROM admin.user', function(err, result){
        callback(err, result);
    });
};

exports.getUserByEmail = function(email, callback){
    mysql.query('SELECT * FROM admin.user WHERE admin.user.email = ?', [email.toLowerCase()], function(err, result){
        callback(err, result);
    });
};

exports.getUserById = function(id, callback){
    mysql.query('SELECT * FROM admin.user WHERE admin.user.id = ?', [id], function(err, result){
        callback(err, result);
    });
};

exports.addUser = function(user, callback){
    if(!user || !user.email){
        callback(new Error('Argument is invalid.'));
    }else{
        user.email = user.email.toLowerCase();
        user.password = cryptoJS.MD5(user.password).toString();
        mysql.query('INSERT INTO admin.user SET ?', user, function(err, result){
            callback(err, result);
        });
    }
};

exports.removeUserById = function(id, callback){
    mysql.query('DELETE FROM admin.user WHERE admin.user.id = ?', [id], function(err, result){
        callback(err, result);
    });
};

exports.updateUser = function(user, callback){
    if(!user || !user.id){
        callback(new Error('Argument is invalid.'));
    }else{
        if(user.email){
            user.email = user.email.toLowerCase();
        }
        
        mysql.query('UPDATE admin.user SET ? WHERE admin.user.id = ?', [user, user.id], function(err, result){
            callback(err, result);
        });  
    }
};

exports.updateUser2 = function(id, oldPassword, newPassword, callback){
    mysql.query('UPDATE admin.user SET admin.user.password = ? WHERE admin.user.id = ? AND admin.user.password = ?', [cryptoJS.MD5(newPassword).toString(), id, cryptoJS.MD5(oldPassword).toString()], function(err, result){
        callback(err, result);
    });  
};