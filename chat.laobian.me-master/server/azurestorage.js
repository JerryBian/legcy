'use strict';

// import * as azure from 'azure-storage';
const connectionString = "storageconnectionstring";
const azure = require('azure-storage');
const tableSvc = azure.createTableService(connectionString);
var blobSvc = azure.createBlobService(connectionString);
exports.entGen = azure.TableUtilities.entityGenerator;

exports.getEntries = function (tableName, rowKey, top, verb, callback) {
    var query = new azure.TableQuery().top(top).where(`RowKey ${verb} ?`, rowKey);
    tableSvc.queryEntities(tableName, query, null, function (err, result, response) {
        callback(err, result);
    });
};

exports.insertEntry = function (tableName, entry, callback) {
    tableSvc.insertEntity(tableName, entry, function (error, result, response) {
        callback(error, result);
    })
};

exports.insertFile = function(containerName, blobName, buffer, contentType, callback){
    blobSvc.createBlockBlobFromText(containerName, blobName, buffer, { contentType: contentType }, function(err, result, response){
        callback(err, null);
    })
}