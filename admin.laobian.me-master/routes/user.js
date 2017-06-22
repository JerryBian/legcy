'use strict';

const express = require('express');
const config = require('config');
const router = express.Router();
const moment = require('moment');
const session = require('../service/session');
const user = require('../service/user');

router.get('/', function(req, res, next){
    res.render('user', { title:  'Manage your information', user: req.user});
});

router.get('/new', function(req, res, next){
    res.render('user_new', { title:  'Create new user'});
});

router.post('/username', function(req, res, next){
    let response = { error: null };
    if(!req.body.id){
        response.error = 'Invalid arguments';
        res.send(response);
    }else{
        console.log(req.body);
        user.updateUser({
            id: req.body.id,
            userName: req.body.userName,
            email: req.body.email
        }, function(err, result){
            if(!err){
                session.remove(req.cookies.session, function(err, result){
                    response.error = err;
                    res.send(response);
                    return;
                });
            }else{
                response.error = err;
                res.send(response);
            }
        });
    }
});

router.post('/password', function(req, res, next){
    let response = { error: null };
    if(!req.body.id){
        response.error = 'Invalid arguments';
        res.send(response);
    }else{
        console.log(req.body);
        user.updateUser2(parseInt(req.body.id), req.body.oldPassword, req.body.newPassword, function(err, result){
            response.error = err;
            
            if(!err && result && result.affectedRows === 0){
                response.error = "Current password is incorrect!";
            }
            
            res.send(response);
        });
    }
});

router.post('/new', function(req, res, next){
    let response = { error: null };
    if(!req.body.email || !req.body.password || !req.body.userName || !req.body.allowed){
        response.error = 'Invalid arguments';
        res.send(response);
    }else{
        console.log(req.body);
        user.addUser({
            email: req.body.email,
            password: req.body.password,
            userName: req.body.userName,
            createAt: moment.utc().format(),
            allowed: req.body.allowed
        }, function(err, result){
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
        user.getUserById(id, function(err, result){
            if(err || !result){
                res.status(500);
            }else{
                res.render('user_edit', { user: result[0] })
            }
        });
    }
});

router.post('/edit', function(req, res, next){
    let response = { error: null };
    if(!req.body.allowed){
        response.error = 'Invalid arguments';
        res.send(response);
    }else{
        console.log(req.body);
        user.updateUser({
            id: req.body.id,
            allowed: req.body.allowed
        }, function(err, result){
            response.error = err;
            res.send(response);
        });
    }
});

router.delete('/', function(req, res, next){
    let response = { error: null };
    console.log(req.body);
    user.removeUserById(parseInt(req.body.id), function(err, result){
        response.error = err;
        res.send(response);
    });
});

router.get('/manage', function(req, res, next){
    user.getUsers(function(err, result){
        if(err){
            res.send(err);
        }else{
            res.render('user_manage', { title:  'Manage users', users: result});
        }
    });
});

module.exports = router;