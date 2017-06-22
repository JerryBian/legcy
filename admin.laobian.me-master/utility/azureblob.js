'use strict';

const config = require('config');
const azure = require('azure-storage');
const blobSvc = azure.createBlobService(config.get('azureblob.connectionstring'));

exports.addFile = function(container, blob, stream, size, callback){
    blobSvc.createBlockBlobFromStream(container, blob, stream, size, function(err){
        callback(err);
    });
};