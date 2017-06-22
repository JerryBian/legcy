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

exports.all = function(callback){
    pool.query('SELECT * FROM blog.post ORDER BY blog.post.createAt DESC', callback);  
};

exports.latest = function(callback){
    pool.query('SELECT * FROM blog.post ORDER BY blog.post.createAt DESC LIMIT 0, 10', callback);
};

exports.get = function(url, callback){
    pool.query('UPDATE blog.post SET blog.post.visits = blog.post.visits + 1 WHERE blog.post.url = ?', [url], function(err, result){
        if(!err && result.affectedRows === 1){
            pool.query('SELECT * FROM blog.post WHERE blog.post.url = ?', [url], callback);
        }else{
            callback(new Error(), null);
        }
    });
};