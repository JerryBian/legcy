'use strict';

const express = require('express');
const path = require('path');
const routes = require('./routes/index');
const route_blog = require('./routes/blog');
const route_user = require('./routes/user');
const user = require('./service/user');
const session = require('./service/session');
const app = express();
const cookieParser = require('cookie-parser')
const bodyParser = require('body-parser');

app.disable('x-powered-by');

app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'pug');

app.use(bodyParser.json({limit: '50mb'}));
app.use(bodyParser.urlencoded({ limit: '50mb', extended: true }));
app.use(cookieParser());
app.use('/assets', express.static(__dirname + '/assets'));
app.use(function(req, res, next){  
    let loginUrl = '/login';
      
    if(req._parsedUrl.pathname.toLowerCase() === loginUrl){
        next();
        return;
    }
    
    loginUrl = loginUrl + '?callback=' + req.originalUrl;
    if(req.cookies.session){
        session.get(req.cookies.session, function(err, result){
            if(!err && result){
                result = JSON.parse(result);
                req.user = result;
                
                if(result.allowed === '{}'){
                   next(); 
                }else{
                    let allowed = JSON.parse(result.allowed);
                    let found = false;
                    allowed.forEach(function(item){
                        if(item.host.toLowerCase() === req.headers['host'].toLowerCase() &&
                           req._parsedUrl.pathname.toLowerCase().startsWith(item.pathname.toLowerCase())){
                           next();
                           found = true;
                           return;
                        }
                    });

                    if(!found){
                        res.redirect(loginUrl);
                    }
                } 
            }else{
                res.redirect(loginUrl);
            }
        });
    }else{
        res.redirect(loginUrl);
    }
});

app.locals.pretty = true;
app.use('/', routes);
app.use('/blog', route_blog);
app.use('/user', route_user);

app.use(function(req, res, next){
    let err = new Error('Not found.');
    err.status = 404;
    next(err);
});

if(app.get('env') !== 'development'){
    app.use(function(err, req, res, next){
        res.status(err.status || 500);
        res.render('error', {
            message: err.message,
            error: err
        });
    });
}

app.use(function(err, req, res, next){
    res.status(err.status || 500);
    res.render('error', {
        message: err.message,
        error: {}
    });
});

app.listen(9001, function(){
    console.log('Example app listening on port 9001!');
});

module.exports = app;