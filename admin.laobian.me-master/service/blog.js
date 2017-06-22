'use strict';

const mysql = require('../utility/mysql');

exports.getBlog = function(id, callback){
    mysql.query('SELECT * FROM blog.post WHERE blog.post.id = ?', [id], function(err, result){
        callback(err, result);
    });
};

exports.getBlogs = function(callback){
    mysql.query('SELECT * FROM blog.post ORDER BY blog.post.createAt DESC', function(err, result){
        callback(err, result);
    });
};

exports.addBlog = function(blog, callback){
    if(!blog || !blog.title || !blog.createAt || !blog.excerpt || !blog.url || !blog.content || !blog.contentHtml){
        callback(new Error('Argument is invalid.'));
    }else{
        blog.url = blog.url.toLowerCase();
        mysql.query('INSERT INTO blog.post SET ?', blog, function(err, result){
            callback(err, result);
        });
    }
};

exports.updateBlog = function(blog, callback){
    if(!blog || !blog.title || !blog.createAt || !blog.excerpt || !blog.url || !blog.content || !blog.contentHtml){
        callback(new Error('Argument is invalid.'));
    }else{
        blog.url = blog.url.toLowerCase();
        mysql.query('UPDATE blog.post SET ? WHERE blog.post.id = ?', [blog, blog.id], function(err, result){
            callback(err, result);
        });  
    }
};