'use strict';

const express = require('express');
const blog = require('../service/blog');
const config = require('config');
const router = express.Router();
const moment = require('moment');

router.get('/', function(req, res, next){
    res.redirect('/blog/manage');
});

router.get('/new', function(req, res, next){
    res.render('blog_new', { title:  'Create new blog'});
});

router.post('/new', function(req, res, next){
    let response = { error: null };
    if(!req.body.createAt){
        req.body.createAt = moment.utc().format();
    }
    
    if(!req.body.visits){
        req.body.visits = 100;
    }
     
    req.body.lastUpdate =  moment.utc().format();
    req.body.publish = parseInt(req.body.publish);  
    if(!req.body.title || !req.body.content || !req.body.contentHtml || !req.body.url){
        response.error = 'Invalid arguments';
        res.send(response);
    }else{
        console.log(req.body);
        blog.addBlog(req.body, function(err, result){
            response.error = err;
            res.send(response);
        });
    }
});

router.get('/edit', function(req, res, next){
    let id = req.query.id;
    if(!id){
        res.status(404);
    }else{
        blog.getBlog(id, function(err, result){
            if(err || !result){
                res.status(500);
            }else{
                res.render('blog_edit', { blog: result[0] })
            }
        });
    }
});

router.post('/edit', function(req, res, next){
    let response = { error: null };
    if(!req.body.createAt){
        req.body.createAt = moment.utc().format();
    }
    
    if(!req.body.visits){
        req.body.visits = 100;
    }
        
    req.body.lastUpdate =  moment.utc().format();
    req.body.publish = parseInt(req.body.publish);
    if(!req.body.title || !req.body.content || !req.body.contentHtml || !req.body.url){
        response.error = 'Invalid arguments';
        res.send(response);
    }else{
        console.log(req.body);
        blog.updateBlog(req.body, function(err, result){
            response.error = err;
            res.send(response);
        });
    }
});

router.get('/manage', function(req, res, next){
    blog.getBlogs(function(err, result){
        if(err){
            res.send(err);
        }else{
            res.render('blog_manage', { title:  'Manage blogs', blogs: result});
        }
    });
});

module.exports = router;