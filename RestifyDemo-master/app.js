/**
 * Created by User on 12/18/2015.
 */
var restify = require('restify');
var MongoClient = require('mongodb').MongoClient;
var assert = require('assert');
var url = 'mongodb://162.243.129.209:27017/restifydemo';

var findId = function(db, id, callback){
    var cursor = db.collection('ids').find({"id": id});
    cursor.toArray(function(err, docs){
        assert.equal(err, null);
        callback(docs);
    });
};

var insertId = function(db, id, callback){
    var collection = db.collection('ids');
    collection.insertOne({'id': id}, function(err, result){
        assert.equal(err, null);
        callback(result);
    });
};

var findIds = function(db, callback) {
    var cursor = db.collection('ids').find({});
    cursor.toArray(function (err, docs) {
        assert.equal(err, null);
        callback(docs);
    });
};

function respond(req, res, next){

    MongoClient.connect(url, function(err, db){
        assert.equal(err, null);

        findId(db, req.params.id, function(docs){
            if(!docs || docs.length !== 1){
                insertId(db, req.params.id, function(result){
                    findIds(db, function(docs){
                        res.send(docs);
                        next();
                    });
                });
            }else{
                findIds(db, function(docs){
                    res.send(docs);
                    next();
                });
            }
        });
    });

}

var server = restify.createServer({
    name: 'Blog engine',
    version: '1.0.0'
});

server.use(restify.authorizationParser());
server.use(function(req, res, next){
    var users = [{
        userName : "test",
        password: "testpwd"
    }];

    var user = users.filter(function(obj){
        if(!req.authorization.basic){
            return false;
        }

        return obj.userName == req.authorization.basic.username;
    });

    if(!req.authorization.basic || aureq.authorization.basic.username == 'anonymous' || !user[0] || user[0].password != req.authorization.basic.password){
        next(new restify.NotAuthorizedError());
    }else{
        next();
    }
});

server.get('/hello/:id', respond);

server.listen(8080, function(){
    console.log('%s listening at %s', server.name, server.url);
});