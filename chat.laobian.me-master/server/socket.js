'use strict';

// import * as socketio from 'socket.io';
// import * as azurestorage from './azurestorage.js';
const socketio = require('socket.io');
const stream = require('stream');
const azurestorage = require('./azurestorage.js');
let users = [];

exports.init = function (app) {
    let io = socketio(app);
    io.on('connection', function (socket) {
        let sessionId = socket.handshake.query.sessionId;
        users.push({
            sessionId: sessionId,
            userName: socket.handshake.query.userName,
            ip: socket.request.connection.remoteAddress,
            userAgent: socket.request.headers['user-agent'],
            joinTime: new Date()
        });
        
        publishUserList(io);
        pushRecentlyMessages(socket);

        socket.on('disconnect', function () {
            users = users.filter(function(item){
                return item.sessionId !== sessionId;
            });
            publishUserList(io);
        });

        socket.on('chat', function (msg) {
            addNewMessage(msg, function (err, result) {
                msg.postTime = msg.postTime.toString();
                io.emit('chat', msg);
            });
        });

        socket.on('chat image', function (msg) {
            addNewImage(msg, function (err, result) {
                if (!err) {
                    addNewMessage(result, function (err, res) {
                        io.emit('chat', result);
                    });
                }
            })
        });

        socket.on('typing', function (msg) {
            socket.broadcast.emit('typing', msg);
        });

        socket.on('stop typing', function (msg) {
            socket.broadcast.emit('stop typing', msg);
        });
    });
}

Date.prototype.yyyymmdd = function () {
    var yyyy = this.getFullYear().toString();
    var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
    var dd = this.getDate().toString();
    return yyyy + (mm[1] ? mm : "0" + mm[0]) + (dd[1] ? dd : "0" + dd[0]); // padding
};

Object.defineProperty(Date.prototype, 'YYYYMMDDHHMMSS', {
    value: function () {
        function pad2(n) {  // always returns a string
            return (n < 10 ? '0' : '') + n;
        }

        return this.getFullYear() +
            pad2(this.getMonth() + 1) +
            pad2(this.getDate()) +
            pad2(this.getHours()) +
            pad2(this.getMinutes()) +
            pad2(this.getSeconds());
    }
});

function publishUserList(io){
    io.emit('publish user list', users);
}

function pushRecentlyMessages(socket) {
    azurestorage.getEntries('chat', (3155378975999999999 -(new Date().getTime() * 10000) - 621355968000000000) + "", 20, 'ge', function (err, result) {
        if (!err) {
            let messages = [];
            for (let i = 0; i < result.entries.length; i++) {
                let entry = result.entries[i];
                messages.push({
                    body: entry.body["_"],
                    postTime: entry.postTime["_"],
                    userName: entry.userName["_"]
                });

                socket.emit('chat', messages);
            }
        }
    });
}

function addNewMessage(message, callback) {
    var now = new Date();
    message.postTime = now;
    var entry = {
        PartitionKey: azurestorage.entGen.String('family'),
        RowKey: azurestorage.entGen.String((3155378975999999999 -(now.getTime() * 10000) - 621355968000000000) + ""),
        userName: azurestorage.entGen.String(message.userName),
        body: azurestorage.entGen.String(message.body),
        postTime: azurestorage.entGen.DateTime(now)
    };

    azurestorage.insertEntry('chat', entry, function (err, result) {
        callback(err, result);
    });
}

function addNewImage(msg, callback) {
    let img = msg.image;
    let indexOfBase64 = img.indexOf('base64');
    if (indexOfBase64 <= 0) {
        callback(new Error('not valid data url.'), null);
    }
    else {
        let ext = img.substring(img.indexOf('image/') + 6, img.indexOf(';base64'));
        let data = img.substr(indexOfBase64 + 7);
        let buffer = new Buffer(data, 'base64');
        let fileName = msg.userName + '-' + new Date().YYYYMMDDHHMMSS() + '.' + ext;
        fileName = fileName.replace(' ', '');
        azurestorage.insertFile("chatfiles", fileName, buffer, "image/" + ext, function (err, result) {
            let fullPath = `http://file.laobian.me/chatfiles/${fileName}`;
            callback(err, {userName: msg.userName, body: `<img src='${fullPath}' class='img-responsive img-thumbnail'/>`});
        });

    }
}