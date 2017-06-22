'use strict';

const express = require('express');
const user = require('../service/user');
const session = require('../service/session');
const config = require('config');
const router = express.Router();

router.get('/', function(req, res, next){
    res.render('index', { title: "Home" });
});

router.get('/login', function(req, res, next){
    res.clearCookie('session');
    res.render('login', { callback: req.query.callback, title: 'Login' });
});

router.post('/login', function(req, res, next){
    let response = { err: null, url: '/'};
    if(req.body.callback){
        response.url = req.body.callback;
    }
    
    if(!req.body.email || !req.body.password){
        response.err = 'Email or Password invalid.';
        res.send(response);
    }else{
        user.login(req.body.email, req.body.password, function(err, result, field){
            if(!err && result.length > 0){
                let user = result[0];
                session.add(user.id, user.email, user.userName, user.allowed, function(err, sessionId){
                    if(!err && sessionId){
                        res.cookie('session', sessionId, { maxAge: config.get('session.clientTTL'), httpOnly: true, domain: config.get('domain.session') });
                        res.send(response);
                    }else{
                        response.err = err.toString();
                        res.send(response);
                    }
                });
            }else{
                response.err = err || 'Email or Password invalid.';
                res.send(response);
            }
        });
    }
});

module.exports = router;