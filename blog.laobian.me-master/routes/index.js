'use strict';

const express = require('express');
const post = require('../service/post');
const router = express.Router();
const moment = require('moment');
const rss = require('rss');

router.get('/', function(req, res, next){
    post.all(function(err, result){
        let grouped = [];
        if(!err && result){
            result.forEach(function(item){
                let createAt = moment(item.createAt);
                let year = createAt.format('YYYY');
                
                getByYear(grouped, year).push({
                    createAt: createAt.format('MMM DD, YYYY'),
                    title: item.title,
                    url: `/${createAt.format('YYYY')}/${createAt.format('MM')}/${item.url}.html`
                })
            });
        }
        
        res.render('index', { title: "Jerry Bian's blog", latest: req.latest, all: grouped, tweets: req.tweets });
    });
});

router.get('/:year/:month/:url.html', function(req, res, next){
    try{
        let year = parseInt(req.params.year);
        let month = parseInt(req.params.month);
        
        if((year < 2005 || year > 2999) || (month < 0 || month > 12)){
            res.status(404);
        }else{
            post.get(req.params.url.toLowerCase(), function(err, result){
                if(!err && result[0]){
                    let createAt = moment(result[0].createAt);
                    result[0].createAt = createAt.format('MMM DD, YYYY');
                    result[0].url = `/${createAt.format('YYYY')}/${createAt.format('MM')}/${result[0].url}.html`;
                    res.render('post', { title: `${result[0].title} - Jerry Bian's blog`, post: result[0], latest: req.latest, tweets: req.tweets });
                }else{
                    res.status(404);
                }
            });
        }
    }catch(err){
        res.status(404);
    }
});

router.get('/rss', function(req, res, next){
    res.header("Content-Type", 'application/rss+xml');
    post.all(function(err, result){
        let feed = new rss({
            title: 'Jerry Bian\'s blog',
            description: 'Programming, Cooking & Living',
            feed_url: 'http://blog.laobian.me/rss',
            site_url: 'http://blog.laobian.me',
            image_url: 'http://ww1.sinaimg.cn/large/95fba9d2jw1e2cotgxvi3j.jpg',
            docs: 'http://blogs.law.harvard.edu/tech/rss',
            managingEditor: 'Jerry Bian(卞良忠)',
            webMaster: 'Jerry Bian(卞良忠)',
            copyright: `2008 - ${moment().utc().format('YYYY')} 卞良忠`,
            language: 'zh',
            categories: ['Programming','Cooking','Living'],
            pubDate: 'June 1, 2008 00:00:00 GMT',
            ttl: '60'
        });
        
        result.forEach(function(item){
            let createAt = moment(item.createAt);
            item.link = `/${createAt.format('YYYY')}/${createAt.format('MM')}/${item.url}.html`;
            item.pubDate = createAt.format('ddd, DD MMM YYYY HH:mm:ss ZZ');
            
            feed.item({
                title:  item.title,
                description: item.contentHtml,
                url: item.link, // link to the item
                guid: item.link, // optional - defaults to url
                author: '卞良忠', // optional - defaults to feed author property
                date: item.pubData, // any format that js Date can parse.
            });
        });
       
        res.status(200).end(feed.xml());
    });
});

function getByYear(arr, year){
    let result;
    arr.forEach(function(item){
        if(item.year === year){
            result = item.posts;
            return;
        }
    });
    
    if(!result){
        let obj = {};
        obj.year = year;
        obj.posts = [];
        arr.push(obj);
        result = obj.posts;
    }
        
    return result;
    
}

module.exports = router;