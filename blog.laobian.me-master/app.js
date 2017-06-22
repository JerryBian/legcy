'use strict';

const express = require('express');
const path = require('path');
const routes = require('./routes/index');
const post = require('./service/post');
const app = express();
const cookieParser = require('cookie-parser')
const bodyParser = require('body-parser');
const moment = require('moment');
const twitter = require('./service/twitter');

app.disable('x-powered-by');

app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'pug');

app.use(bodyParser.json({limit: '50mb'}));
app.use(bodyParser.urlencoded({ limit: '50mb', extended: true }));
app.use(cookieParser());
app.use('/assets', express.static(__dirname + '/assets'));

app.use(function(req, res, next){
    if(req._parsedUrl.pathname.toLowerCase() === '/error' || req._parsedUrl.pathname.toLowerCase() === '/rss'){
        next();
        return;
    }
    
    post.latest(function(err, result){
        let latest = [];
        req.latest = latest;
        
        if(!err && result){
            result.forEach(function(item){
                let createAt = moment(item.createAt);
                latest.push({
                    title: item.title,
                    url: `/${createAt.format('YYYY')}/${createAt.format('MM')}/${item.url}.html`,
                    createdAt: createAt.format('MMM DD, YYYY'),
                    visits: item.visits
                });
            });
            
            twitter.timeline('cnBian', function(err, tweets, res){
                req.tweets = undefined;
                
                if(!err){
                    req.tweets = tweets;
                }
                
                next();
            });
        }
    });
});

app.locals.pretty = false;
app.use('/', routes);

app.use(function(req, res, next){
    let err = new Error('Not found.');
    err.status = 404;
    next(err);
});

if(app.get('env') === 'development'){
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

app.listen(9002, function(){
    console.log('Example app listening on port 9002！');
});

module.exports = app;