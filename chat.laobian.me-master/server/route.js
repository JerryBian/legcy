'use strict';

// import * as azurestorage from './azurestorage.js';
// import * as qs from 'querystring';
// import * as fs from 'fs';
const azurestorage = require('./azurestorage.js');
const qs = require('querystring');
const fs = require('fs');
const path = require("path");

exports.distribute = function (req, res) {
    if (isUrlEquals(req.url, '/')) {
        handleNormal('index.html', res);
        return;
    }

    if (isUrlEquals(req.url, '/style.css')) {
        handleWithMimeType('style.css', 'text/css', res);
        return;
    }

    if (isUrlEquals(req.url, '/script.js')) {
        handleWithMimeType('script.js', 'text/javascript', res);
        return;
    }

    if (isUrlEquals(req.url, '/login')) {
        if (isMethodEquals(req.method, 'GET')) {
            handleNormal('login.html', res);
            return;
        }

        if (isMethodEquals(req.method, 'POST')) {
            handleLogin(req, res);
            return;
        }
    }
    
    if(isUrlEquals(req.url, '/favicon.ico')){
        handleWithMimeType('favicon.ico', 'image/x-icon', res)
        return;
    }
    
    handleUnknown(res);
};

function handleUnknown(res) {
    res.writeHead(404);
    res.end();
}

function handleLogin(req, res) {
    let body = '';
    req.on('data', function (data) {
        body += data;
        if (body.length > 1e6) {
            req.connection.destroy();
        }
    });

    req.on('end', function () {
        let user = qs.parse(body);
        azurestorage.getEntries('user', user.email, 1, 'eq', function (err, result) {
            if (err || result.entries.length !== 1 || result.entries[0].password['_'] !== user.password) {
                res.writeHead(500);
                res.end();
            } else {
                res.writeHead(200);
                res.end(result.entries[0].userName["_"]);
            }
        });
    });
}

function handleNormal(filePath, res) {
    findFile(filePath, function (err, data) {
        if (err) {
            res.writeHead(404);
            return res.end('Error loading ' + filePath + ', the error is ' + err);
        }

        res.writeHead(200, { "content-type": "text/html" });
        res.end(data);
    });
}

function handleWithMimeType(filePath, mimeType, res) {
    findFile(filePath, function (err, data) {
        if (err) {
            res.writeHead(404);
            return res.end('Error loading ' + filePath);
        }

        res.writeHead(200, { 'content-type': mimeType });
        res.end(data);
    });
}

function findFile(filePath, callback) {
    let fullpath = path.join(__dirname, '..', 'clients', filePath);
    fs.readFile(fullpath, function (err, data) {
        callback(err, data);
    });
}

function isUrlEquals(requestUrl, targetUrl) {
    return requestUrl.toUpperCase() === targetUrl.toUpperCase();
}

function isMethodEquals(requestMethod, targetMethod) {
    return requestMethod.toUpperCase() === targetMethod.toUpperCase();
}