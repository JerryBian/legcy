'user strict';

const config = require('config');
const mysql = require('mysql');
const pool = mysql.createPool({
    host: config.get('mysql.host'),
    user: config.get('mysql.user'),
    password: config.get('mysql.password')
});

pool.on('error', function(err){
    console.log(`MySQL error: ${err}`);
});

exports.query = function(query, param, callback){
    pool.query(query, param, function(err, result){
        callback(err, result);
    });
};